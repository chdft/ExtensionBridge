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

			//relative path are relative to the application directory, not the current directory
			DirectoryAssemlbySource directorySource = new DirectoryAssemlbySource("Extensions", "*.mycustomextensionformat");
			//you have to call directorySource.LoadAssemblies() *before* you add directorySource to the repository (this actually loads the assemblies from the found files)
			//while it is recommended to check the returned collection of FileLoadResults for any unsuccessful items (and show that information to the user/log it somewhere), you can safely ignore the return value of directorySource.LoadAssemblies()
			//note that the call succeeds, even when the specified directory (in this case Extensions) can not be found
			foreach (var result in directorySource.LoadAssemblies())
			{
				//when the files contents were successfully loaded as assembly, result.IsSuccess is true
				//note that result.IsSuccess==true does not implicate that there are any extensions in the assembly
				//note that when a file contains an assembly that was already loaded, result.IsSuccess is true
				//  that means that it is safe to call directorySource.LoadAssemblies() more than once (even after it was added to an repository)
				//  doing so results in loading all newly added assemblies, however removed ones are not unloaded
				if (result.IsSuccess)
				{
					Console.WriteLine("{0}: loaded successfully", result.FilePath);
				}
				else
				{
					//result.Exception contains the encountered exception
					Console.WriteLine("{0}: exception: {1}", result.FilePath, result.Exception.ToString());
				}
			}


			repository.Sources.Add(directorySource);
			//LocalAssemblySource always refers to the assembly that was started (which is usually your host applications .exe)
			//if you want more control (i.e. because your host application consists of more than one assembly), use SingleAssemblySource instead
			repository.Sources.Add(new LocalAssemblySource());


			foreach (var extension in repository.GetExtensions<ITextProvider>())
			{
				//you can use extension.Source to determine which source contained the extension
				//you can use extension.ExtensionType to get the type object representing the extensions type, without actually creating an instance
				//  this is useful, when you want to use a dependency injection container or need additional information (i.e. the source assembly)
				//extension.CreateInstance() creates a new instance every time it is called using the public default constructor
				//  an ExtensionInstantiationException is thrown when such a constructor is unavailable
				//  if the extensions default constructor throws an exception, an ExtensionInstantiationException is thrown
				Console.WriteLine("{0} from {1} says {2}", extension.ExtensionType, extension.Source, extension.CreateInstance().Text);
			}
			Console.ReadLine();
		}
	}
}
