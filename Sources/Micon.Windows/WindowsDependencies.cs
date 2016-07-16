using System;
using Micon.Windows.Bitmaps;
using Micon.Portable;
using Micon.Portable.Bitmaps;
using Autofac;

namespace Micon
{
	public static class WindowsDependencies
	{
		public static void RegisterPlatform(this ContainerBuilder container)
        {
            container.RegisterType<WindowsBitmapLoader>().As<IBitmapLoader>();
        }
	}
}

