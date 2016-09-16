using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// Uses a provided assembly as source for extensions.
	/// </summary>
	public class SingleAssemblySource : ISource
	{
		public SingleAssemblySource(Assembly assembly)
		{
			Assembly = assembly;
		}

		public Assembly Assembly { get; private set; }

		public IEnumerable<Assembly> GetAssemblies()
		{
			return new Assembly[] { Assembly };
		}
	}
}
