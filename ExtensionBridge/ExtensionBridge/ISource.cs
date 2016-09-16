using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public interface ISource
	{
		/// <summary>
		/// get a collection of assemblies which may contain extensions
		/// </summary>
		/// <returns>collection of assemblies which may contain extensions</returns>
		/// <exception cref="SourceLoadingException">one or more assemblies could not be loaded</exception>
		IEnumerable<Assembly> GetAssemblies();
	}
}
