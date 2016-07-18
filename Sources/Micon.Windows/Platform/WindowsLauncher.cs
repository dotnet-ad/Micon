namespace Micon.Windows.Platform
{
    using Portable.Platform;
    using System.Diagnostics;
    using System.Windows;

    public class WindowsLauncher : ILauncher
    {
        public void Open(string url)
        {
            Process.Start(url);
        }

        public void Quit()
        {
            Application.Current.Shutdown();
        }
    }
}
