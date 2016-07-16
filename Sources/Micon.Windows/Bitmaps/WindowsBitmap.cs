using Micon.Portable;
using Micon.Portable.Bitmaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            
            var img = new Image() { Source = otherNsImage, Stretch = Stretch.Uniform };
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
        
        private Brush CreateBrush(Micon.Portable.Generation.Color color, Micon.Portable.Generation.Color endColor)
        {
            var baseColor = color.ToNative();

            if(endColor == null)
                return new SolidColorBrush(baseColor);
            
            return new LinearGradientBrush(baseColor, endColor.ToNative(), 95);
        }

        private Brush CreateBorderBrush(Micon.Portable.Generation.Color color, Micon.Portable.Generation.Color endColor)
        {
            var l = color.Lightness;

            var darkColor = color.Lerp(Micon.Portable.Generation.Color.FromRgb(0, 0, 0), l * 0.20).ToNative();

            if (endColor == null)
                return new SolidColorBrush(darkColor);

            var endDarkColor = endColor.Lerp(Micon.Portable.Generation.Color.FromRgb(0, 0, 0), l * 0.40).ToNative();

            return new LinearGradientBrush(darkColor, endDarkColor, 95);
        }

        public void DrawCircle(Micon.Portable.Generation.Color color, Micon.Portable.Generation.Color endColor, bool border)
        {
            var circle = new System.Windows.Shapes.Ellipse() {
                Fill = CreateBrush(color, endColor),
                Stretch = Stretch.Fill,
                Width = this.Width,
                Height = this.Height,
            };

            if(border)
            {
                circle.Stroke = this.CreateBorderBrush(color, endColor);
                circle.StrokeThickness = 2.0;
            }

            circle.Measure(new System.Windows.Size(this.Width, this.Height));
            circle.Arrange(new Rect(0, 0, this.Width, this.Height));
            this.Image.Render(circle);
        }

        public void DrawRectangle(Micon.Portable.Generation.Color color, Micon.Portable.Generation.Color endColor, bool border, double cornerRadius)
        {
            var rect = new Border()
            {
                Background = CreateBrush(color, endColor),
                CornerRadius = new CornerRadius(cornerRadius),
                Width = this.Width,
                Height = this.Height,
            };

            if (border)
            {
                rect.BorderBrush = this.CreateBorderBrush(color,endColor);
                rect.BorderThickness = new Thickness(2.0);
            }

            rect.Measure(new System.Windows.Size(this.Width, this.Height));
            rect.Arrange(new Rect(0, 0, this.Width, this.Height));
            this.Image.Render(rect);
        }
    }
}
