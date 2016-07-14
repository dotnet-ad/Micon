using System;
using Micon.Mac.Bitmaps;
using Micon.Portable;
using Micon.Portable.Bitmaps;

namespace Micon.Mac
{
	public class MacDependencies : Dependencies
	{
		public static void Init()
		{
			var dep = new MacDependencies();
			dep.Register();
		}

		private MacDependencies() { }

		protected override void RegisterPlatform(Splat.IMutableDependencyResolver locator)
		{
			locator.Register<IBitmapLoader>(() => new MacBitmapLoader());
		}
	}
}

