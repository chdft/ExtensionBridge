using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public class DirectorySource : ISource
	{
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
				foreach (var file in System.IO.Directory.GetFiles(Directory))
				{
					assemblies.Add(Assembly.LoadFrom(file));
				}
				Assemblies = assemblies;
			}
			return Assemblies;
		}
	}
}
