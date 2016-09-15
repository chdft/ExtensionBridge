using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public interface ISource<TExtensionContract> where TExtensionContract : class
	{
		IEnumerable<TExtensionContract> GetAllInstances();
	}
}
