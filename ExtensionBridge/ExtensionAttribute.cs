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
		/// <summary>
		/// Create a new ExtensionAttribute based on a declared contract interface.
		/// </summary>
		/// <param name="contract">contract interface type / null to implicitly declare all implemented contracts</param>
		public ExtensionAttribute(Type contract)
		{
			Contract = contract;
		}

		/// <summary>
		/// defines the contract that this extension fullfills
		/// if null, all implemented contracts are assumed
		/// </summary>
		public Type Contract { get; private set; }
	}
}
