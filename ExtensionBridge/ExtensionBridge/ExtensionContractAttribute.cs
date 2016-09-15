using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// marks the decorated class or interface as base for an extension
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface|AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ExtensionContractAttribute : Attribute
	{
		public ExtensionContractAttribute() { }
	}
}
