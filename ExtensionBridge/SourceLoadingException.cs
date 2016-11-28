using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// The exception that is thrown when a source fails to load/find/locate its assemblies.
	/// </summary>
	[Serializable]
	public class SourceLoadingException : Exception
	{
		public SourceLoadingException() { }
		public SourceLoadingException(string message) : base(message) { }
		public SourceLoadingException(string message, Exception inner) : base(message, inner) { }
		protected SourceLoadingException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
