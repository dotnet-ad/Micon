using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micon.Portable.Bitmaps;
using Micon.Portable.Generation;
using ReactiveUI;
using System.Reactive.Linq;

namespace Micon.Portable
{
	public class HomeViewModel: ReactiveObject
	{
		public HomeViewModel(IBitmapLoader loader = null, IconGenerator generator = null)
		{
            this.LogoScale = 1.0;
            this.BackgroundColor = Color.FromRgb(0x45, 0x99, 0xd9);
            
			// Dependencies
			var locator = Splat.Locator.Current;
			this.loader = loader ?? locator.GetService<IBitmapLoader>();
			this.generator = generator ?? locator.GetService<IconGenerator>();

			// Reactive
			this.LoadLogoCommand = ReactiveCommand.CreateAsyncTask(ExecuteLoadImageCommand);
			this.LoadLogoCommand.Subscribe((img) => this.Logo = img);
            this.WhenAnyValue(
                (x) => x.Logo, 
                (x) => x.LogoScale, 
                (x) => x.BackgroundColor, 
                (x) => x.BackgroundEndColor, 
                (x) => x.BackgroundShape, 
                (x) => x.GradientMode,
                (logo, scale, color,endColor, shape,gradient) => new PreviewItemViewModel(this.generator, logo, scale, color,endColor, gradientMode, shape))
              //.Throttle(TimeSpan.FromMilliseconds(200))
              .Subscribe((o) => this.Preview = o);// .ToProperty(this,(x) => x.Preview);
            this.WhenAnyValue((x) => x.Logo).Subscribe((i) => this.RaisePropertyChanged(nameof(LogoPath)));
        }

        #region Fields

        readonly IBitmapLoader loader;

		readonly IconGenerator generator;

		private IBitmap logo;

        private double logoScale;

        private Color backgroundColor;

        private Color backgroundEndColor;

        private Shape backgroundShape;

        private GradientMode gradientMode;

        private PreviewItemViewModel preview;

        private ScreenBackground screenBackground;

        #endregion

        #region Bound properties

        public string LogoPath
        {
            get { return this.Logo?.Path; }
            set { this.LoadLogoCommand.Execute(value); }
        }

        public IBitmap Logo
		{
			get { return logo; }
			set { this.RaiseAndSetIfChanged(ref logo, value); }
		}

        public double LogoScale
        {
            get { return logoScale; }
            set { this.RaiseAndSetIfChanged(ref logoScale, value); }
        }

        public Color BackgroundColor
		{
			get { return backgroundColor; }
			set { this.RaiseAndSetIfChanged(ref backgroundColor, value); }
        }

        public Color BackgroundEndColor
        {
            get { return backgroundEndColor; }
            set { this.RaiseAndSetIfChanged(ref backgroundEndColor, value); }
        }

        public Shape BackgroundShape
        {
            get { return backgroundShape; }
            set { this.RaiseAndSetIfChanged(ref backgroundShape, value); }
        }

        public PreviewItemViewModel Preview
		{
			get { return preview; }
			set { this.RaiseAndSetIfChanged(ref preview, value); }
        }

        public GradientMode GradientMode
        {
            get { return gradientMode; }
            set { this.RaiseAndSetIfChanged(ref gradientMode, value); }
        }

        public ScreenBackground ScreenBackground
        {
            get { return screenBackground; }
            set { this.RaiseAndSetIfChanged(ref screenBackground, value); }
        }

        #endregion

        #region Bound commands

        public ReactiveCommand<IBitmap> LoadLogoCommand { get; private set; }

		public ReactiveCommand<PreviewItemViewModel> PreviewCommand { get; private set; }

		public ReactiveCommand<IEnumerable<IBitmap>> GenerateCommand { get; private set; }

		#endregion

		#region Command execution

		private Task<IBitmap> ExecuteLoadImageCommand(object parameter)
		{
			var path = parameter as string;
			return this.loader.LoadAsync(path);
		}

		#endregion
	}
}

