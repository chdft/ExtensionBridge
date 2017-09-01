using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	/// <summary>
	/// Wraps the result of an attempt to load a file as assembly.
	/// </summary>
	public class FileLoadResult
	{
		/// <summary>
		/// Create a new instance for a loaded file.
		/// </summary>
		/// <param name="path">Path to the file/assembly.</param>
		/// <param name="e">exception thrown while loading the assembly / null if no exception occurred</param>
		public FileLoadResult(string path, Exception e)
		{
			Exception = e;
			FilePath = path;
		}

		/// <summary>
		/// Create a new instance for a successfully loaded file.
		/// </summary>
		/// <param name="path">Path to the file/assembly.</param>
		public FileLoadResult(string path) : this(path, null) { }

		/// <summary>
		/// True if the file was loaded successfully, false otherwise.
		/// </summary>
		public bool IsSuccess
		{
			get
			{
				return Exception == null;
			}
		}

		/// <summary>
		/// Path to the file.
		/// </summary>
		/// <remarks>
		/// This path may be local or remote and may not even exist. 
		/// </remarks>
		public string FilePath { get; private set; }

		/// <summary>
		/// Gets the exception that was thrown while loading the file as assembly. Null iif no exception was thrown.
		/// </summary>
		public Exception Exception { get; private set; }
	}
}
