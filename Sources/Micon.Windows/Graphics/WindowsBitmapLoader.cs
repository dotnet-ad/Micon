namespace Micon.Windows.Graphics
{
    using System.Threading.Tasks;
    using Portable.Graphics;

    public class WindowsBitmapLoader : IBitmapLoader
    {
        public WindowsBitmapLoader()
        {
        }

        public IBitmap Create(int width, int height)
        {
            return new WindowsBitmap(width, height);
        }

        public Task<IBitmap> LoadAsync(string path)
        {
            return Task.FromResult<IBitmap>(new WindowsBitmap(path));
        }
    }
}
