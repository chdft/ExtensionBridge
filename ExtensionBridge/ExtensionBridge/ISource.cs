using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public interface ISource<TExtension>
	{
		IEnumerable<TExtension> GetAll();
	}
}
