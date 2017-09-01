using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public class Repository
	{
		public Repository()
		{
			_Sources = new LinkedList<IAssemblySource>();
		}

		public Repository(IEnumerable<IAssemblySource> sources)
		{
			_Sources = new LinkedList<IAssemblySource>(sources);
		}

		/// <summary>
		/// collection of sources used for getting the extension implementations
		/// </summary>
		/// <remarks>
		/// This reference is guaranteed to not change during the lifetime of this object.
		/// </remarks>
		/// <seealso cref="IAssemblySource"/>
		public ICollection<IAssemblySource> Sources
		{
			get { return _Sources; }
		}
		private ICollection<IAssemblySource> _Sources;

		/// <summary>
		/// extensions for the contract <typeparamref name="TContract"/>
		/// </summary>
		/// <typeparam name="TContract">contract the extension must implement</typeparam>
		/// <returns>collection of matching extension</returns>
		/// <exception cref="ArgumentException"><typeparamref name="TContract"/> is not a contract (decorated with <see cref="ExtensionContractAttribute"/>)</exception>
		public IEnumerable<Extension<TContract>> GetExtensions<TContract>() where TContract : class
		{
			if (!typeof(TContract).IsContract())
			{
				throw new ArgumentException("TContract must be marked as contract. (use ContractAttribute)");
			}

			foreach (var source in Sources)
			{
				IEnumerable<Assembly> assemblies = null;
				try
				{
					assemblies = source.GetAssemblies();
				}
				catch (SourceLoadingException)
				{
					//we can't really do anything about this => re-throw
					throw;
				}
				if (assemblies != null)
				{
					foreach (var assembly in assemblies)
					{
						foreach (Type type in assembly.GetTypes())
						{
							//check first, if the contract is implemented at all; otherwise the search for an extension-attribute isn't necessary
							//check ContainsGenericParameters, because generic types with unspecified type parameters cannot be instantiated...
							//doing it in this order is slightly faster, because checking whether TContract is implemented is faster than searching for a matching attribute
							if (type.ImplementsContract<TContract>() && type.DeclaresContract<TContract>() && !type.ContainsGenericParameters)
							{
								yield return new Extension<TContract>(type, source);
								break; //return each type only once
							}
						}
					}
				}
			}
		}
	}
}
