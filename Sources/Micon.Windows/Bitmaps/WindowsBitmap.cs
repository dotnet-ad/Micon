using Micon.Portable;
using Micon.Portable.Bitmaps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Micon.Windows.Bitmaps
{
    public class WindowsBitmap : IBitmap
    {
        public WindowsBitmap(string path, int width, int height)
        {
            this.Path = path;
            this.Image = new RenderTargetBitmap(width, height,96d,96d,PixelFormats.Default);
        }

        public WindowsBitmap(string path)
        {
            this.Path = path;
            var bi = new BitmapImage(new Uri(path));
            this.Image = new RenderTargetBitmap((int)bi.Width, (int)bi.Height, 96d, 96d, PixelFormats.Default);
            var img = new Image() { Source = bi, Stretch = Stretch.Fill };
            img.Measure(new System.Windows.Size(bi.Width,bi.Height));
            img.Arrange(new Rect(0, 0, bi.Width, bi.Height));
            this.Image.Render(img);    
        }

        public RenderTargetBitmap Image { get; private set; }

        public string Path { get; private set; }

        public int Width
        {
            get { return (int)this.Image.Width; }
        }

        public int Height
        {
            get { return (int)this.Image.Height; }
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
            var otherNsImage = ((WindowsBitmap)other).Image;
            
            var img = new Image() { Source = otherNsImage, Stretch = Stretch.Fill };
            img.Measure(new System.Windows.Size(area.Width,area.Height));
            img.Arrange(new Rect(area.X, area.Y, area.Width, area.Height));
            this.Image.Render(img);
        }

        public Task Save()
        {
            var png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(this.Image));
            CreateIfNotExists(this.Path);
            using (var stream = File.Create(this.Path))
            {
                png.Save(stream);
            }
            return Task.FromResult(true);
        }
    }
}
