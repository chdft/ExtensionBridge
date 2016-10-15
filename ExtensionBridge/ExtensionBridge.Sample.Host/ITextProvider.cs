using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge.Sample.Host
{
	//you may want to extract your ExtensionContract into a small class library so extension developers don't have a dependency on your host application
	[ExtensionContract]
	public interface ITextProvider
	{
		string Text { get; }
	}
}
