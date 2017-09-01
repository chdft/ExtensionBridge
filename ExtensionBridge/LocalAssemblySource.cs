using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// Uses the entry assembly as source for extensions.
	/// </summary>
	public class LocalAssemblySource : IAssemblySource
	{
		/// <summary>
		/// Create a new LocalAssemblySource instance.
		/// </summary>
		public LocalAssemblySource() { }

		/// <inheritdoc/>
		public IEnumerable<Assembly> GetAssemblies()
		{
			return new Assembly[] { Assembly.GetEntryAssembly() };
		}
	}
}
