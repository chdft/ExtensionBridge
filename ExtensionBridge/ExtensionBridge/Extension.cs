using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public class Extension<TContract> where TContract : class
	{
		public Extension(Type extensionType, ISource source)
		{
			ExtensionType = extensionType;
			Source = source;
		}

		public Type ExtensionType { get; private set; }

		public ISource Source { get; private set; }

		public TContract CreateInstance()
		{
			TContract instance = null;

			try
			{
				instance = Activator.CreateInstance(ExtensionType) as TContract;
			}
			catch (ArgumentException e)
			{
				throw new ExtensionInstantiationException("the extension type cannot be instantiated", e);
			}
			catch (NotSupportedException e)
			{
				throw new ExtensionInstantiationException("the extension type cannot be instantiated", e);
			}

			if (instance == null)
			{
				//type not matching (this happens when somebody passes the wrong type into the constructor)
				throw new ExtensionInstantiationException("the extension type does not implement the contract");
			}

			return instance;
		}
	}
}
