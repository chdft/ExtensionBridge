using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge.Sample.Host
{
	[Extension(typeof(ITextProvider))]
	class LocalExtension : ITextProvider
	{
		public string Text
		{
			get { return "Hello World from the executing assembly!"; }
		}
	}
}
