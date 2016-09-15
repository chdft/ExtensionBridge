using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public class AssemblySource<TExtensionContract> : ISource<TExtensionContract> where TExtensionContract : class
	{
		public AssemblySource(Assembly sourceAssembly)
		{
			SourceAssembly = sourceAssembly;
		}

		public Assembly SourceAssembly { get; private set; }

		public IEnumerable<TExtensionContract> GetAllInstances()
		{
			foreach (var extensionType in GetAllTypes())
			{
				yield return Activator.CreateInstance(extensionType) as TExtensionContract;
			}
		}

		protected IEnumerable<Type> GetAllTypes()
		{
			foreach (Type type in SourceAssembly.GetTypes())
			{
				//check first, if the contract is implemented at all; otherwise the search for an extension-attribute isn't nessecary
				if (ImplementsContract(type))
				{
					foreach (var extensionAttribute in type.GetCustomAttributes(typeof(ExtensionAttribute)).Cast<ExtensionAttribute>())
					{
						if (extensionAttribute.Contract == typeof(TExtensionContract))
						{
							//matching contract
							yield return type;
							break; //return each type only once
						}
					}
				}
			}
		}

		private bool ImplementsContract(Type potentialExtensionType)
		{
			return potentialExtensionType.GetInterfaces().Contains(typeof(TExtensionContract));
		}
	}
}
