namespace Micon
{
    using Autofac;
    using Windows.Platform;
    using Portable.Platform;
    using Portable.Graphics;
    using NGraphics;
    public static class WindowsDependencies
	{
		public static void RegisterPlatform(this ContainerBuilder container)
        {
            container.Register((x) => Platforms.Current).As<IPlatform>();
            container.RegisterType<WindowsStorage>().As<IStorage>();
            container.RegisterType<WindowsLauncher>().As<ILauncher>();
            container.RegisterType<WindowsInfo>().As<IInfo>();
        }
	}
}

