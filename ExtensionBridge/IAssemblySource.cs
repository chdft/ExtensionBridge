﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// Provides assemblies for extension-lookup.
	/// </summary>
	/// <seealso cref="KnownAssemblySource"/>
	/// <seealso cref="DirectoryAssemblySource"/>
	/// <seealso cref="LocalAssemblySource"/>
	public interface IAssemblySource
	{
		/// <summary>
		/// get a collection of assemblies which may contain extensions
		/// </summary>
		/// <returns>collection of assemblies which may contain extensions</returns>
		/// <exception cref="SourceLoadingException">one or more assemblies could not be loaded</exception>
		IEnumerable<Assembly> GetAssemblies();
	}
}
