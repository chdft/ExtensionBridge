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
		public Repository() { }

		/// <summary>
		/// collection of sources used for getting the extension implementations
		/// </summary>
		/// <remarks>
		/// This reference is guaranteed to not change during the lifetime of this object.
		/// </remarks>
		public IList<ISource> Sources
		{
			get { return _Sources; }
		}
		private IList<ISource> _Sources = new List<ISource>();

		/// <summary>
		/// creates instances of all found extensions for the contract <typeparamref name="TContract"/>
		/// </summary>
		/// <typeparam name="TContract">contract the extension must implement</typeparam>
		/// <returns>collection of instances of every matching extension</returns>
		public IEnumerable<TContract> GetAllInstances<TContract>() where TContract : class
		{
			foreach (var extensionType in GetAllTypes<TContract>())
			{
				yield return Activator.CreateInstance(extensionType) as TContract;
			}
		}

		/// <summary>
		/// gets type definitions of all found extensions for the contract <typeparamref name="TContract"/>
		/// </summary>
		/// <typeparam name="TContract">contract the extension must implement</typeparam>
		/// <returns>collection of instances of every matching extension</returns>
		public IEnumerable<Type> GetAllTypes<TContract>() where TContract : class
		{
			foreach (var assembly in GetAllAssemblies())
			{
				foreach (Type type in assembly.GetTypes())
				{
					//check first, if the contract is implemented at all; otherwise the search for an extension-attribute isn't nessecary
					if (ImplementsContract<TContract>(type) && DeclaresContract<TContract>(type))
					{
						yield return type;
						break; //return each type only once
					}
				}
			}
		}

		/// <summary>
		/// get all assemblies found in all sources
		/// </summary>
		/// <returns>all found assemblies</returns>
		protected IEnumerable<Assembly> GetAllAssemblies()
		{
			foreach (var source in Sources)
			{
				foreach (var assembly in source.GetAssemblies())
				{
					yield return assembly;
				}
			}
		}

		/// <summary>
		/// checks if a type implements a contract
		/// </summary>
		/// <typeparam name="TContract">contract interface for which to check</typeparam>
		/// <param name="potentialExtensionType">type which will be checked</param>
		/// <returns>true, if the type implements the contract; false otherwise</returns>
		private bool ImplementsContract<TContract>(Type potentialExtensionType) where TContract : class
		{
			return potentialExtensionType.GetInterfaces().Contains(typeof(TContract));
		}

		/// <summary>
		/// checks if a type declares a contract
		/// </summary>
		/// <typeparam name="TContract">contract interface for which to check</typeparam>
		/// <param name="potentialExtensionType">type which will be checked</param>
		/// <returns>true, if the type declares the contract; false otherwise</returns>
		private bool DeclaresContract<TContract>(Type potentialExtensionType) where TContract : class
		{
			foreach (var extensionAttribute in potentialExtensionType.GetCustomAttributes(typeof(ExtensionAttribute)).Cast<ExtensionAttribute>())
			{
				if (extensionAttribute.Contract == typeof(TContract))
				{
					//matching contract
					return true;
				}
			}
			return false;
		}
	}
}
