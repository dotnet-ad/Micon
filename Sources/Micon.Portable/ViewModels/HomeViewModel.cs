using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micon.Portable.Bitmaps;
using Micon.Portable.Generation;
using ReactiveUI;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using System.Linq;
using Micon.Portable.Data;
using System.IO;

namespace Micon.Portable
{
	public class HomeViewModel: ReactiveObject
	{
		public HomeViewModel(IBitmapLoader loader, IconGenerator generator)
		{
            this.LogoScale = 1.0;
            this.BackgroundColor = Color.FromRgb(0x45, 0x99, 0xd9);
            this.BackgroundHasBorder = true;

            //Icons
            var icons = new List<Icon>();
            icons.AddRange(Icons.Load(Icons.iOS));
            icons.AddRange(Icons.Load(Icons.Android));
            icons.AddRange(Icons.Load(Icons.Windows));
            this.icons = icons;

            //Depdencies
            this.loader = loader;
            this.generator = generator;

            // Reactive
            this.ExportCommand = ReactiveCommand.CreateAsyncTask(ExecuteExportCommand);
            this.LoadLogoCommand = ReactiveCommand.CreateAsyncTask(ExecuteLoadImageCommand);
			this.LoadLogoCommand.Subscribe((img) => this.Logo = img);
            this.WhenAnyValue(
                (x) => x.Logo,
                (x) => x.LogoScale, 
                (x) => x.BackgroundColor, 
                (x) => x.BackgroundEndColor, 
                (x) => x.BackgroundShape,
                (x) => x.BackgroundHasBorder,
                (x) => x.GradientMode,
                this.CreatePreview)
              //.Throttle(TimeSpan.FromMilliseconds(200))
              .Subscribe((o) => this.Preview = o);// .ToProperty(this,(x) => x.Preview);
            this.WhenAnyValue((x) => x.Logo).Subscribe((i) => this.RaisePropertyChanged(nameof(LogoPath)));
        }

        #region Fields

        private string path;

        readonly IBitmapLoader loader;

		readonly IconGenerator generator;

        private IEnumerable<Icon> icons;

        #endregion

        #region Bound properties
        
        public string LogoPath
        {
            get { return this.path; }
            set { this.LoadLogoCommand.Execute(value); }
        }

        [Reactive]
        public IBitmap Logo { get; set; }

        [Reactive]
        public double LogoScale { get; set; }

        [Reactive]
        public Color BackgroundColor { get; set; }

        [Reactive]
        public Color BackgroundEndColor { get; set; }

        [Reactive]
        public Shape BackgroundShape { get; set; }

        [Reactive]
        public bool BackgroundHasBorder { get; set; }

        [Reactive]
        public PreviewItemViewModel Preview { get; set; }

        [Reactive]
        public GradientMode GradientMode { get; set; }

        [Reactive]
        public ScreenBackground ScreenBackground { get; set; }

        #endregion

        #region Bound commands

        public ReactiveCommand<IBitmap> LoadLogoCommand { get; private set; }

		public ReactiveCommand<PreviewItemViewModel> PreviewCommand { get; private set; }

		public ReactiveCommand<IEnumerable<IBitmap>> ExportCommand { get; private set; }

		#endregion

		#region Command execution

		private async Task<IBitmap> ExecuteLoadImageCommand(object parameter)
		{
			var path = parameter as string;
			var r = await this.loader.LoadAsync(path);
            this.path = path;
            return r;
        }

        private async Task<IEnumerable<IBitmap>> ExecuteExportCommand(object parameter)
        {
            var output = parameter as string;

            var bitmaps = this.icons.Select((x) => new { Icon = x, Bitmap = Generate(x) });

            var saves = bitmaps.Select((x) => x.Bitmap.Save(Path.Combine(output, $"{x.Icon.Name}.png")));

            await Task.WhenAll(saves);

            return bitmaps.Select((x) => x.Bitmap);
        }

        #endregion

        #region Preview

        #endregion

        #region Icons

        private IBitmap Generate(Icon icon)
        {
            return this.Generate(icon, this.Logo, this.LogoScale, this.BackgroundColor, this.BackgroundEndColor, this.BackgroundShape, this.BackgroundHasBorder , this.GradientMode);
        }

        private IBitmap Generate(Icon icon, IBitmap logo, double scale, Color color, Color endColor, Shape shape, bool hasBorder, GradientMode gradient)
        {
            var result = icon.Copy();
            result.HasBorder &= hasBorder;
            result.BackgroundColor = color;
            result.BackgroundEndColor = GetEndColor(color,endColor,gradient);
            result.Scale *= scale;

            if (icon.BackgroundShape != Shape.Unavailable)
            {
                result.BackgroundShape = shape;
                result.HasBorder = hasBorder;
            }

            return this.generator.GenerateIcon(logo, result);
        }

        const string IconPreviewApple = "AppIcon.appiconset/Icon-76x76@2x";
        const string IconPreviewAndroid = "xhdpi/ic_launcher";
        const string IconPreviewWindows = "Square150x150Logo";
        const string IconPreviewWindowsSmall = "Square71x71Logo";
        const string IconPreviewWindowsWide = "Wide310x150Logo";

        private PreviewItemViewModel CreatePreview(IBitmap logo, double scale, Color color, Color endColor, Shape shape, bool hasBorder, GradientMode gradient)
        {
            return new PreviewItemViewModel()
            {
                Apple = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"iOS/{IconPreviewApple}"), logo, scale, color, endColor, shape, hasBorder, gradient),
                Android = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"Android/{IconPreviewAndroid}"), logo, scale, color, endColor, shape, hasBorder, gradient),
                Windows = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"Windows/{IconPreviewWindows}"), logo, scale, color, endColor, shape, hasBorder, gradient),
                WindowsSmall = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"Windows/{IconPreviewWindowsSmall}"), logo, scale, color, endColor, shape, hasBorder, gradient),
                WindowsWide = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"Windows/{IconPreviewWindowsWide}"), logo, scale, color, endColor, shape, hasBorder, gradient),
            };
        }

        private Color GetEndColor(Color color, Color endColor, GradientMode gradient)
        {
            if (gradient == GradientMode.Auto)
            {
                endColor = color?.Lerp(color.CreateOpposite(), color.Lightness * 0.75);
            }
            else if (this.GradientMode == GradientMode.None)
            {
                endColor = null;
            }

            return endColor;
        }

        #endregion
    }
}

