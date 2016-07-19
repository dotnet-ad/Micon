namespace Micon
{
    using Autofac;
    using Windows.Platform;
    using Portable.Platform;
    using Windows.Graphics;
    using Portable.Graphics;

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

