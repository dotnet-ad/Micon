namespace Micon.Portable.Bitmaps
{
	using System;
	using System.Threading.Tasks;
	using Micon.Portable.Generation;

	public interface IBitmap
	{
		string Path { get; }

		int Width { get; }

		int Height { get; }

		Rectangle Bounds { get; }

		void Draw(IBitmap other, Rectangle area);

        void DrawCircle(Color color, Color endColor, bool border);

        void DrawRectangle(Color color, Color endColor, bool border, double cornerRadius);

        Task Save();
	}
}

