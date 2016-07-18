namespace Micon.Windows.Bitmaps
{
    using System;
    using Micon.Portable.Platform;
    using System.Deployment.Application;
    public class WindowsInfo : IInfo
    {
        public string GetApplicationVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (InvalidDeploymentException)
            {
                return "NA";
            }
        }
    }
}
