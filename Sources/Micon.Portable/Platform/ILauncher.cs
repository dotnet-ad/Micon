namespace Micon.Portable.Platform
{
    public interface ILauncher
    {
        void Open(string url);

        void Quit();
    }
}
