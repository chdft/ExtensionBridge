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

		public List<ISource> Sources
		{
			get { return _Sources; }
		}
		private List<ISource> _Sources = new List<ISource>();

		public IEnumerable<TContract> GetAllInstances<TContract>() where TContract : class
		{
			foreach (var extensionType in GetAllTypes<TContract>())
			{
				yield return Activator.CreateInstance(extensionType) as TContract;
			}
		}

		public IEnumerable<Type> GetAllTypes<TContract>() where TContract : class
		{
			foreach (var assembly in GetAllAssemblies())
			{
				foreach (Type type in assembly.GetTypes())
				{
					//check first, if the contract is implemented at all; otherwise the search for an extension-attribute isn't nessecary
					if (ImplementsContract<TContract>(type))
					{
						foreach (var extensionAttribute in type.GetCustomAttributes(typeof(ExtensionAttribute)).Cast<ExtensionAttribute>())
						{
							if (extensionAttribute.Contract == typeof(TContract))
							{
								//matching contract
								yield return type;
								break; //return each type only once
							}
						}
					}
				}
			}
		}

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

		private bool ImplementsContract<TContract>(Type potentialExtensionType) where TContract : class
		{
			return potentialExtensionType.GetInterfaces().Contains(typeof(TContract));
		}

	}
}
