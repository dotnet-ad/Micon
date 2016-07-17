namespace Micon.Portable.Bitmaps
{
	using System;
	using System.Threading.Tasks;

	public interface IBitmapLoader
	{
		IBitmap Create(int width, int height);

		Task<IBitmap> LoadAsync(string path);
	}
}

