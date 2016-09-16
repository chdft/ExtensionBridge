using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// Uses all assemblies found in a directory as a source for extensions.
	/// </summary>
	public class DirectorySource : ISource
	{
		/// <param name="directory">relative or absolute path to a directory</param>
		/// <param name="searchPattern">search pattern used to filter files; filters for DLL-files by default; see https://msdn.microsoft.com/library/wz42302f(v=vs.110).aspx for information about the allowed characters</param>
		public DirectorySource(string directory, string searchPattern = "*.dll")
		{
			Directory = directory;
			SearchPattern = searchPattern;
		}

		public string Directory { get; private set; }

		public string SearchPattern { get; private set; }

		private IEnumerable<Assembly> Assemblies;

		public IEnumerable<Assembly> GetAssemblies()
		{
			if (Assemblies == null)
			{
				List<Assembly> assemblies = new List<Assembly>();
				foreach (var file in System.IO.Directory.GetFiles(Directory, SearchPattern, SearchOption.TopDirectoryOnly))
				{
					assemblies.Add(Assembly.LoadFrom(file));
				}
				Assemblies = assemblies;
			}
			return Assemblies;
		}
	}
}
