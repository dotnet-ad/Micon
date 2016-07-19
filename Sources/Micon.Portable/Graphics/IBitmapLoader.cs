namespace Micon.Portable.Graphics
{
	using System.Threading.Tasks;

    /// <summary>
    /// A loader for bitmaps.
    /// </summary>
	public interface IBitmapLoader
	{
        /// <summary>
        /// Creates a new bitmap with specified size.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
		IBitmap Create(int width, int height);

        /// <summary>
        /// Loads an existing image from the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
		Task<IBitmap> LoadAsync(string path);
	}
}

