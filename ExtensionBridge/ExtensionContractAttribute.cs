using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// Declares the decorated interface as base for an extension
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
	public sealed class ExtensionContractAttribute : Attribute
	{
		/// <summary>
		/// Create a new ExtensionContractAttribute instance.
		/// </summary>
		public ExtensionContractAttribute() { }
	}
}
