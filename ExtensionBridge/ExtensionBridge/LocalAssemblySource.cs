using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public class LocalAssemblySource<TExtension> : ISource<TExtension>
	{
		public LocalAssemblySource() { }

		public IEnumerable<TExtension> GetAll()
		{
			throw new NotImplementedException();
		}
	}
}
