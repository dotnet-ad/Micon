using AppKit;
using Micon.Mac;

namespace Micon
{
	static class MainClass
	{
		static void Main(string[] args)
		{
			MacDependencies.Init();
			NSApplication.Init();
			NSApplication.Main(args);
		}
	}
}
