using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micon.Portable.Bitmaps;
using Micon.Portable.Generation;
using ReactiveUI;

namespace Micon.Portable
{
	public class HomeViewModel: ReactiveObject
	{
		public HomeViewModel(IBitmapLoader loader = null, IconGenerator generator = null)
		{
			// Dependencies
			var locator = Splat.Locator.Current;
			this.loader = loader ?? locator.GetService<IBitmapLoader>();
			this.generator = generator ?? locator.GetService<IconGenerator>();

			// Reactive
			this.LoadLogoCommand = ReactiveCommand.CreateAsyncTask(ExecuteLoadImageCommand);
			this.LoadBackgroundCommand = ReactiveCommand.CreateAsyncTask(ExecuteLoadImageCommand);
			this.LoadLogoCommand.Subscribe((img) => this.Logo = img);
			this.LoadBackgroundCommand.Subscribe((img) => this.Background = img);
			this.WhenAny((x) => x.Logo, (x) => x.Background, (logo, bg) => new PreviewItemViewModel(this.generator,logo.Value,bg.Value)).ToProperty(this,(x) => x.Preview);

		}

		#region Fields

		readonly IBitmapLoader loader;

		readonly IconGenerator generator;

		private IBitmap logo;

		private IBitmap background;

		private PreviewItemViewModel preview;

		#endregion

		#region Bound properties

		public IBitmap Logo
		{
			get { return logo; }
			set { this.RaiseAndSetIfChanged(ref logo, value); }
		}

		public IBitmap Background
		{
			get { return background; }
			set { this.RaiseAndSetIfChanged(ref background, value); }
		}

		public PreviewItemViewModel Preview
		{
			get { return preview; }
			set { this.RaiseAndSetIfChanged(ref preview, value); }
		}

		#endregion

		#region Bound commands

		public ReactiveCommand<IBitmap> LoadLogoCommand { get; private set; }

		public ReactiveCommand<IBitmap> LoadBackgroundCommand { get; private set; }

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

