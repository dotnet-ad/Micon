using System;
using Micon.Portable.Bitmaps;
using Micon.Portable.Generation;
using ReactiveUI;

namespace Micon.Portable
{
	public class PreviewItemViewModel : ReactiveObject
	{
		public PreviewItemViewModel(IconGenerator generator, IBitmap logo, double scale, Color color, Color endColor, GradientMode gradient, Shape shape)
        {
            this.Load(generator, logo, scale, color, endColor, gradient,shape);
		}

        private async void Load(IconGenerator generator, IBitmap logo, double scale, Color color, Color endColor, GradientMode gradient, Shape shape)
        {
            if(gradient == GradientMode.Auto)
            {
                endColor = color?.Lerp(color.CreateOpposite(), color.Lightness * 0.75);
            }
            else if(gradient == GradientMode.None)
            {
                endColor = null;
            }

            // iOS
            var iosIcon = new Icon("ios", 100) { BackgroundColor = color, BackgroundEndColor = endColor };
            iosIcon.ImageArea.Width =(int)(iosIcon.ImageArea.Width *scale);
            iosIcon.ImageArea.Height = (int)(iosIcon.ImageArea.Height * scale);
            iosIcon.ImageArea.X = (iosIcon.Width / 2) - (iosIcon.ImageArea.Width  / 2);
            iosIcon.ImageArea.Y = (iosIcon.Height / 2) - (iosIcon.ImageArea.Height / 2);
            var t1 = generator.GenerateIcon("ios", logo, iosIcon);

            // Android
            var androidIcon = new Icon("android", 100) { BackgroundColor = color, BackgroundShape = shape, BackgroundEndColor = endColor, HasBorder = true };
            androidIcon.ImageArea.Width = (int)(androidIcon.ImageArea.Width * scale);
            androidIcon.ImageArea.Height = (int)(androidIcon.ImageArea.Height * scale);
            androidIcon.ImageArea.X = (androidIcon.Width / 2) - (androidIcon.ImageArea.Width / 2);
            androidIcon.ImageArea.Y = (androidIcon.Height / 2) - (androidIcon.ImageArea.Height / 2);
            var t2 = generator.GenerateIcon("android", logo, androidIcon);

            Apple = await t1;
            Android = await t2;
        }

        private IBitmap apple,android;

        public IBitmap Apple
        {
            get { return apple; }
            set {
                this.RaiseAndSetIfChanged(ref apple, value); }
        }

        public IBitmap Android
        {
            get { return android; }
            set { this.RaiseAndSetIfChanged(ref android, value); }
        }
    }
}

