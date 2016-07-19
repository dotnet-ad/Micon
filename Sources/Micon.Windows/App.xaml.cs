namespace Micon.Windows
{
    using Autofac;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        public App()
        {
            var builder = new ContainerBuilder();
            builder.RegisterShared();
            builder.RegisterPlatform();
            App.Container = builder.Build();
        }
    }
}
