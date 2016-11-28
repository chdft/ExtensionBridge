using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public class FileLoadResult
	{
		public FileLoadResult(string path, Exception e)
		{
			Exception = e;
			FilePath = path;
		}

		public FileLoadResult(string path) : this(path, null) { }

		public bool IsSuccess
		{
			get
			{
				return Exception == null;
			}
		}

		public string FilePath { get; private set; }

		public Exception Exception { get; private set; }
	}
}
