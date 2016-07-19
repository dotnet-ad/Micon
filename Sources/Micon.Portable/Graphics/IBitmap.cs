namespace Micon.Portable.Graphics
{
	using System.Threading.Tasks;

    /// <summary>
    /// Represents an drawable image.
    /// </summary>
	public interface IBitmap
	{
        /// <summary>
        /// The width of the image (in pixels).
        /// </summary>
		int Width { get; }

        /// <summary>
        /// The height of the image (in pixels).
        /// </summary>
        int Height { get; }

        /// <summary>
        /// The bounds of the image (0,0,Width,Height).
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Draws a bitmap.
        /// </summary>
        /// <param name="other">The other bitmap.</param>
        /// <param name="area">The area where the bitmap is drawn</param>
		void Draw(IBitmap other, Rectangle area);

        /// <summary>
        /// Draws a circle filled with a gradient.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="endColor"></param>
        /// <param name="border"></param>
        void DrawCircle(Color color, Color endColor, bool border);

        /// <summary>
        /// Draws a rectangle filled with a gradient.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="endColor"></param>
        /// <param name="border"></param>
        /// <param name="cornerRadius"></param>
        void DrawRectangle(Color color, Color endColor, bool border, double cornerRadius);

        /// <summary>
        /// Saves the image to the specified path.
        /// </summary>
        /// <param name="path">The file location.</param>
        /// <returns>The asynchronous task</returns>
        Task Save(string path);
	}
}

