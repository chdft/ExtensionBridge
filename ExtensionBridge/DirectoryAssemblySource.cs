﻿using System;
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
	public class DirectoryAssemblySource : IAssemblySource
	{
		/// <summary>
		/// Search pattern matching only DLLs with .dll extension
		/// </summary>
		public const string DllSearchPattern = "*.dll";

		/// <summary>
		/// Search pattern matching all files
		/// </summary>
		public const string AllFilesSearchPattern = "*";

		/// <param name="directory">relative or absolute path to a directory, see <see cref="Directory"/> for detailed information</param>
		/// <param name="searchPattern">search pattern used to filter files; filters for DLL-files only by default; see <see cref="SearchPattern"/> for detailed information</param>
		/// <remarks>
		/// <note type="warning">
		/// You must call <see cref="LoadAssemblies"/> before calling <see cref="GetAssemblies"/> (or passing it to a Repository). Otherwise the call will fail with a <see cref="SourceLoadingException"/> and extensions from this source will not be available.
		/// </note>
		/// </remarks>
		/// <seealso cref="DllSearchPattern"/>
		public DirectoryAssemblySource(string directory, string searchPattern = DllSearchPattern)
		{
			Directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory);
			SearchPattern = searchPattern;
		}

		/// <summary>
		/// Relative or absolute path to the directory that is searched for assemblies
		/// Note that paths are relative to the application directory and not the current directory for security reasons.
		/// </summary>
		public string Directory { get; private set; }

		/// <summary>
		/// Search pattern used to filter files
		/// </summary>
		/// <remarks>
		/// <note type="note">
		/// Consult https://msdn.microsoft.com/library/wz42302f(v=vs.110).aspx for the syntax of this string.
		/// </note>
		/// Changing this value is useful, when you want to use a custom file extension for the files containing the assemblies.
		/// </remarks>
		public string SearchPattern { get; private set; }

		/// <summary>
		/// Currently known set of assemblies.
		/// </summary>
		/// <remarks>
		/// This value is updated when <see cref="LoadAssemblies"/> is called.
		/// </remarks>
		protected IEnumerable<Assembly> Assemblies { get; private set; }

		/// <summary>
		/// Loads all assemblies matching from the specified Directory
		/// </summary>
		/// <returns>Collection of <see cref="FileLoadResult"/> containing information about the status of each found file (most notably whether it could be loaded successfully)</returns>
		/// <seealso cref="Directory"/>
		/// <seealso cref="SearchPattern"/>
		public IEnumerable<FileLoadResult> LoadAssemblies()
		{
			List<FileLoadResult> results = new List<FileLoadResult>();
			List<Assembly> assemblies = new List<Assembly>();
			if (System.IO.Directory.Exists(Directory))
			{
				foreach (var file in System.IO.Directory.GetFiles(Directory, SearchPattern, SearchOption.TopDirectoryOnly))
				{
					Assembly assembly = null;
					try
					{
						assembly = Assembly.LoadFrom(file);
					}
					catch (System.IO.FileNotFoundException e)
					{
						//race condition: the file was deleted during file enumeration => can be safely ignored
						results.Add(new FileLoadResult(file, e));
					}
					catch (BadImageFormatException e)
					{
						//incompatible assembly format (or not an assembly at all)
						results.Add(new FileLoadResult(file, e));
					}
					catch (System.Security.SecurityException e)
					{
						//permission denied (this has nothing to do with file system access control, but AppDomain restrictions)
						//this exception will occur for *all* assemblies since all assemblies would be loaded from the same directory (which is either local or remote, but not both)
						results.Add(new FileLoadResult(file, e));
					}
					catch (System.IO.PathTooLongException e)
					{
						//path is to long; we can't do anything about it
						results.Add(new FileLoadResult(file, e));
					}
					if (assembly != null)
					{
						assemblies.Add(assembly);
						results.Add(new FileLoadResult(file));
					}
				}
			}
			Assemblies = assemblies;
			return results;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// <note type="warning">
		/// You must call <see cref="LoadAssemblies"/> before calling <see cref="GetAssemblies"/> (or passing it to a Repository). Otherwise the call will fail with a <see cref="SourceLoadingException"/> and extensions from this source will not be available.
		/// </note>
		/// </remarks>
		public IEnumerable<Assembly> GetAssemblies()
		{
			if (Assemblies == null)
			{
				throw new SourceLoadingException("Assemblies were not loaded, call LoadAssemblies()");
			}
			return Assemblies;
		}
	}
}
