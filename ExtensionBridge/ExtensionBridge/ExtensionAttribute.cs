using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// marks the decorated class as an extension
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class ExtensionAttribute : Attribute
	{
		public ExtensionAttribute(Type contract)
		{
			if (contract == null) { throw new ArgumentNullException("contract"); }
			Contract = contract;
		}

		/// <summary>
		/// defines the contract that this extension fullfills
		/// if null, all implemented contracts are assumed
		/// </summary>
		public Type Contract { get; private set; }
	}
}
