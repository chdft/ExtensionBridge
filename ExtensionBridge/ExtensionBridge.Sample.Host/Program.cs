using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge.Sample.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			Repository repository = new Repository();
			DirectoryAssemlbySource directorySource = new DirectoryAssemlbySource("Extensions", "*.mycustomextensionformat"); //pass string.Empty to scan the 
			foreach (var result in directorySource.LoadAssemblies())
			{
				if (result.IsSuccess)
				{
					Console.WriteLine("{0}: loaded successfully", result.FilePath);
				}
				else
				{
					Console.WriteLine("{0}: exception: {1}", result.FilePath, result.Exception.ToString());
				}
			}
			repository.Sources.Add(directorySource);
			repository.Sources.Add(new LocalAssemblySource());

			foreach (var extension in repository.GetExtensions<ITextProvider>())
			{
				Console.WriteLine("{0} from {1} says {2}", extension.ExtensionType, extension.Source, extension.CreateInstance().Text);
			}
			Console.ReadLine();
		}
	}
}
