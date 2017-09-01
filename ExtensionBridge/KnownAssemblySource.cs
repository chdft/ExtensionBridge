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
	public class KnownAssemblySource : IAssemblySource
	{
		/// <summary>
		/// Create a new KnownAssemblySource instance using a known assembly.
		/// </summary>
		/// <param name="assembly">Assembly which will be provided as source for extension-lookups</param>
		public KnownAssemblySource(Assembly assembly)
		{
			if (assembly.ReflectionOnly) { throw new ArgumentException("assembly is loaded into reflection-only context and not execution-context."); }

			Assembly = assembly;
		}

		/// <summary>
		/// Assembly provided as source
		/// </summary>
		public Assembly Assembly { get; private set; }

		/// <inheritdoc/>
		public IEnumerable<Assembly> GetAssemblies()
		{
			return new Assembly[] { Assembly };
		}
	}
}
