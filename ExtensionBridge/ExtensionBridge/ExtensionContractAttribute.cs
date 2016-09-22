using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// marks the decorated interface as base for an extension
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
	public sealed class ExtensionContractAttribute : Attribute
	{
		public ExtensionContractAttribute() { }
	}
}
