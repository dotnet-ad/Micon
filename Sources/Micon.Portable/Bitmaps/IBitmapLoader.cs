namespace Micon.Portable.Bitmaps
{
	using System;
	using System.Threading.Tasks;

	public interface IBitmapLoader
	{
		Task<IBitmap> Create(string path, int width, int height);

		Task<IBitmap> LoadAsync(string path);
	}
}

