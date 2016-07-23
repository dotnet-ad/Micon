namespace Micon.Windows.Platform
{
    using Portable.Platform; 
    using System.Reflection;

    public class WindowsInfo : IInfo
    {
        public string GetApplicationVersion()
        {
            var version = Assembly.GetEntryAssembly().GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
