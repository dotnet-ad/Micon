namespace Micon.Mac.Bitmaps
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using AppKit;
	using CoreGraphics;
	using Foundation;
	using Portable;
	using Micon.Portable.Generation;
	using Portable.Bitmaps;

	public class MacBitmap : IBitmap
	{
		public MacBitmap(string path, int width, int height)
		{
			this.Path = path;
			this.Image = new NSImage(new CGSize(width,height));
		}

		public MacBitmap(string path)
		{
			this.Path = path;
			this.Image = new NSImage(path);
		}

		public NSImage Image { get; private set; }

		public string Path { get; private set; }

		public int Width
		{
			get { return (int)this.Image.Size.Width; }
		}

		public int Height
		{
			get { return (int)this.Image.Size.Height; }
		}

		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(0, 0, this.Width, this.Height);
			}
		}

		private static void CreateIfNotExists(string path)
		{
			var dir = System.IO.Directory.GetParent(path);
			if (!dir.Exists)
			{
				dir.Create();
			}

			var file = new System.IO.FileInfo(path);
			if (!file.Exists)
			{
				file.Create();
			}
		}

		public void Draw(IBitmap other, Rectangle area)
		{
			var otherNsImage = ((MacBitmap)other).Image;

			this.Image.LockFocus();
			otherNsImage.ResizingMode = NSImageResizingMode.Stretch;
			otherNsImage.Size = new CGSize(area.Width, area.Height);
			NSGraphicsContext.CurrentContext.ImageInterpolation = NSImageInterpolation.High;
			otherNsImage.Draw( new CGPoint(area.X, area.Y), new CGRect(0, 0, area.Width, area.Height), NSCompositingOperation.Copy, 1.0f);
			this.Image.UnlockFocus();
		}

		public Task Save()
		{
			var rep = new NSBitmapImageRep(this.Image.CGImage);
			var png = rep.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png);
			CreateIfNotExists(this.Path);
			return Task.FromResult(png.Save(this.Path, true));
		}
	}
}

