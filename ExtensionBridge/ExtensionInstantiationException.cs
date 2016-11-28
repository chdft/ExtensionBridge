using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// The exception that is thrown when an extension cannot be instantiated.
	/// </summary>
	[Serializable]
	public class ExtensionInstantiationException : Exception
	{
		public ExtensionInstantiationException() { }
		public ExtensionInstantiationException(string message) : base(message) { }
		public ExtensionInstantiationException(string message, Exception inner) : base(message, inner) { }
		protected ExtensionInstantiationException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
