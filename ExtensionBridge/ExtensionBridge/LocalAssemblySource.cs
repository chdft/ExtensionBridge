using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// Uses the currently executing assembly as source for extensions.
	/// </summary>
	public class LocalAssemblySource : ISource
	{
		public LocalAssemblySource() { }

		public IEnumerable<Assembly> GetAssemblies()
		{
			return new Assembly[] { Assembly.GetExecutingAssembly() };
		}
	}
}
