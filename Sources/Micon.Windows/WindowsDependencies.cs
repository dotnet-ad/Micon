using System;
using Micon.Windows.Bitmaps;
using Micon.Portable.Bitmaps;
using Autofac;
using Micon.Windows.Platform;
using Micon.Portable.Platform;

namespace Micon
{
	public static class WindowsDependencies
	{
		public static void RegisterPlatform(this ContainerBuilder container)
        {
            container.RegisterType<WindowsBitmapLoader>().As<IBitmapLoader>();
            container.RegisterType<WindowsStorage>().As<IStorage>();
            container.RegisterType<WindowsLauncher>().As<ILauncher>();
            container.RegisterType<WindowsInfo>().As<IInfo>();
        }
	}
}

