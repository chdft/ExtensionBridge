using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge.Sample.Extension1
{
	[Extension(typeof(ExtensionBridge.Sample.Host.ITextProvider))]
	public class Extension1 : ExtensionBridge.Sample.Host.ITextProvider
	{
		public string Text
		{
			get { return "Hello World from another assembly!"; }
		}
	}
}
