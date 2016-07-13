using Micon.Portable.Bitmaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micon.Windows.Bitmaps
{
    public class WindowsBitmapLoader : IBitmapLoader
    {
        public WindowsBitmapLoader()
        {
        }

        public Task<IBitmap> Create(string path, int width, int height)
        {
            return Task.FromResult<IBitmap>(new WindowsBitmap(path, width, height));
        }

        public Task<IBitmap> LoadAsync(string path)
        {
            return Task.FromResult<IBitmap>(new WindowsBitmap(path));
        }
    }
}
