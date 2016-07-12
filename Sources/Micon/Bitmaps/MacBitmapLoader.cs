using System;
using System.Threading.Tasks;
using Micon.Portable.Bitmaps;

namespace Micon.Mac.Bitmaps
{
	public class MacBitmapLoader : IBitmapLoader
	{
		public MacBitmapLoader()
		{
		}

		public Task<IBitmap> Create(string path, int width, int height)
		{
			return Task.FromResult<IBitmap>(new MacBitmap(path,width,height));
		}

		public Task<IBitmap> LoadAsync(string path)
		{
			return Task.FromResult<IBitmap>(new MacBitmap(path));
		}
	}
}

