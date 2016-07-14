using System;
using Micon.Windows.Bitmaps;
using Micon.Portable;
using Micon.Portable.Bitmaps;

namespace Micon.Windows
{
	public class WindowsDependencies : Dependencies
	{
		public static void Init()
		{
			var dep = new WindowsDependencies();
			dep.Register();
		}

		private WindowsDependencies() { }

		protected override void RegisterPlatform(Splat.IMutableDependencyResolver locator)
		{
			locator.Register<IBitmapLoader>(() => new WindowsBitmapLoader());
		}
	}
}

