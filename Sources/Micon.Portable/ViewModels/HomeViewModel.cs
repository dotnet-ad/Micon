using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micon.Portable.Bitmaps;
using Micon.Portable.Generation;
using ReactiveUI;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;

namespace Micon.Portable
{
	public class HomeViewModel: ReactiveObject
	{
		public HomeViewModel(IBitmapLoader loader, IconGenerator generator)
		{
            this.LogoScale = 1.0;
            this.BackgroundColor = Color.FromRgb(0x45, 0x99, 0xd9);

            //Depdencies
            this.loader = loader;
            this.generator = generator;

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
                (logo, scale, color,endColor, shape,gradient) => new PreviewItemViewModel(this.generator, logo, scale, color,endColor, gradient, shape))
              //.Throttle(TimeSpan.FromMilliseconds(200))
              .Subscribe((o) => this.Preview = o);// .ToProperty(this,(x) => x.Preview);
            this.WhenAnyValue((x) => x.Logo).Subscribe((i) => this.RaisePropertyChanged(nameof(LogoPath)));
        }

        #region Fields

        readonly IBitmapLoader loader;

		readonly IconGenerator generator;

        #endregion

        #region Bound properties
        
        public string LogoPath
        {
            get { return this.Logo?.Path; }
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
        public PreviewItemViewModel Preview { get; set; }

        [Reactive]
        public GradientMode GradientMode { get; set; }

        [Reactive]
        public ScreenBackground ScreenBackground { get; set; }

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

