using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// Represents an extension implementing <typeparamref name="TContract"/>.
	/// </summary>
	/// <typeparam name="TContract">Contract interface</typeparam>
	public class Extension<TContract> where TContract : class
	{
		/// <summary>
		/// Create a new Extension instance based on the represented extension type and providing source.
		/// </summary>
		/// <param name="extensionType">Type of the extension.</param>
		/// <param name="source"><see cref="IAssemblySource"/> that provided the assembly containing this extension.</param>
		public Extension(Type extensionType, IAssemblySource source)
		{
			if (!typeof(TContract).IsAssignableFrom(extensionType))
			{
				throw new ArgumentException("The extension type does not implement the contract (TContract is not assignable from extensionType)");
			}
			if (extensionType.ContainsGenericParameters)
			{
				throw new ArgumentException("The extension type has open type parameters. Such a type cannot be instantiated.");
			}

			ExtensionType = extensionType;
			Source = source;
		}

		/// <summary>
		/// Gets the Type of the represented extension.
		/// </summary>
		/// <remarks>
		/// This attribute is intended for advanced use-cases, like extensions with non-default constructors.
		/// Use <see cref="CreateInstance"/> to create a new instance of the represented extension using the default constructor.
		/// </remarks>
		public Type ExtensionType { get; private set; }

		/// <summary>
		/// Gets the <see cref="IAssemblySource"/> that provided this extension.
		/// </summary>
		public IAssemblySource Source { get; private set; }

		/// <summary>
		/// Creates a new Instance of the extension.
		/// </summary>
		/// <exception cref="ExtensionInstantiationException">the extension could not be instantiated</exception>
		/// <returns>instance of the extension</returns>
		public TContract CreateInstance()
		{
			TContract instance = null;

			try
			{
				instance = Activator.CreateInstance(ExtensionType) as TContract;
			}
			catch (ArgumentException e)
			{
				//this should only happen when ExtensionType is not a RuntimeType since the constructor checks for open type parameters
				throw new ExtensionInstantiationException("The extension type cannot be instantiated.", e);
			}
			catch (NotSupportedException e)
			{
				throw new ExtensionInstantiationException("The extension type cannot be instantiated.", e);
			}
			catch (TargetInvocationException e)
			{
				throw new ExtensionInstantiationException("The extension threw an exception in the constructor.", e);
			}
			catch (MethodAccessException e)
			{
				throw new ExtensionInstantiationException("The extensions default constructor is not accessible (check that the constructor is public).", e);
			}
			catch (MissingMethodException e)
			{
				throw new ExtensionInstantiationException("The extension does not have a default constructor.", e);
			}
			catch (TypeLoadException e)
			{
				throw new ExtensionInstantiationException("ExtensionType is invalid.", e);
			}
			catch (InvalidComObjectException e)
			{
				throw new ExtensionInstantiationException("ExtensionType is invalid.", e);
			}
			catch (COMException e)
			{
				throw new ExtensionInstantiationException("ExtensionType is invalid.", e);
			}

			if (instance == null)
			{
				//type not matching (this happens when somebody passes the wrong type into the constructor)
				//this should have been caught by the constructor
				throw new ExtensionInstantiationException("the extension type does not implement the contract");
			}

			return instance;
		}
	}
}
