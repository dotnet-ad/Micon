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

		Task Save();
	}
}

