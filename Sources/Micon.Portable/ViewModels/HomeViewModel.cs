namespace Micon.Portable
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ReactiveUI;
    using System.Reactive.Linq;
    using ReactiveUI.Fody.Helpers;
    using System.Linq;
    using System.IO;
    using Data;
    using Graphics;
    using Generation;
    using Platform;
    using Files;
    using ViewModels.Items;
    using NGraphics;
    using Extensions;
    public class HomeViewModel: ReactiveObject
	{
		public HomeViewModel(IPlatform platform, IStorage storage, ILauncher launcher, IInfo info, IIconGenerator generator)
        {
            this.platform = platform;

            //Icons
            var sets = new[] { Icons.Load(Icons.iOS), Icons.Load(Icons.Android), Icons.Load(Icons.Windows) };
            
            this.defaultIcons = sets.SelectMany((s) => s.Icons);
            this.defaultManifests = sets.SelectMany((s) => s.Manifests);
            this.icons = this.defaultIcons;
            this.manifests = this.defaultManifests;
            this.iconsPreview = defaultIconPreview;
            
            //Depdencies
            this.info = info;
            this.generator = generator;
            this.storage = storage;
            this.launcher = launcher;

            // Reactive
            this.ExportCommand = ReactiveCommand.CreateAsyncTask(ExecuteExportCommand);
            this.LoadLogoCommand = ReactiveCommand.CreateAsyncTask(ExecuteLoadImageCommand);
            this.NewCommand = ReactiveCommand.Create();
            this.NewCommand.Subscribe(this.ExecuteNewCommand);
            this.AboutCommand = ReactiveCommand.Create();
            this.AboutCommand.Subscribe(this.ExecuteAboutCommand);
            this.QuitCommand = ReactiveCommand.Create();
            this.QuitCommand.Subscribe(this.ExecuteQuitCommand);
            this.OpenCommand = ReactiveCommand.CreateAsyncTask(ExecuteLoadCommand);
            this.SaveCommand = ReactiveCommand.CreateAsyncTask(ExecuteSaveCommand);
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
            
            this.WhenAnyValue(
                (x) => x.LogoPath,
                (x) => x.GradientMode,
                (x) => x.BackgroundColor,
                (x) => x.BackgroundEndColor,
                (x) => x.BackgroundShape,
                (x) => x.BackgroundHasBorder,
                (x) => x.GradientMode,
                (a, b, c, d, e, f, g) => a
                ).Subscribe((a) => this.saved = false);

            this.NewCommand.Subscribe((x) =>  this.saved = true);
            this.OpenCommand.Subscribe((x) => this.saved = true);
            this.SaveCommand.Subscribe((x) => this.saved = true);

            // Init
            this.ExecuteNewCommand(null);
        }

        #region Constants
        
        const string IconPreviewApple = "AppIcon.appiconset/Icon-76x76@2x";

        const string IconPreviewAndroid = "drawable-xhdpi/ic_launcher";

        const string IconPreviewWindows = "Square150x150Logo";

        const string IconPreviewWindowsSmall = "Square71x71Logo";

        const string IconPreviewWindowsWide = "Wide310x150Logo";

        #endregion

        #region Fields

        private bool saved;

        private readonly IStorage storage;

        private string path;
        
		readonly IIconGenerator generator;

        private IEnumerable<Icon> icons;

        private IEnumerable<MiconManifest> manifests;

        private MiconPreview iconsPreview;

        private readonly static MiconPreview defaultIconPreview = new MiconPreview()
        {
            Android = IconPreviewAndroid,
            Apple = IconPreviewApple,
            Windows = IconPreviewWindows,
            WindowsSmall = IconPreviewWindowsSmall,
            WindowsWide = IconPreviewWindowsWide,
        };

        private readonly ILauncher launcher;

        private readonly IInfo info;

        private readonly IEnumerable<Icon> defaultIcons;
        private readonly IEnumerable<MiconManifest> defaultManifests;
        private readonly IPlatform platform;

        #endregion

        #region Bound properties

        public string LogoPath
        {
            get { return this.path; }
            set { this.LoadLogoCommand.Execute(value); }
        }

        public string Version { get { return this.info.GetApplicationVersion(); } }

        [Reactive]
        public IImage Logo { get; set; }

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
        public GradientMode GradientMode { get; set; }

        [Reactive]
        public PreviewItemViewModel Preview { get; set; }

        [Reactive]
        public ScreenBackground ScreenBackground { get; set; }

        #endregion

        #region Bound commands

        public ReactiveCommand<IImage> LoadLogoCommand { get; private set; }

		public ReactiveCommand<PreviewItemViewModel> PreviewCommand { get; private set; }

		public ReactiveCommand<string> ExportCommand { get; private set; }

        public ReactiveCommand<MiconFile> OpenCommand { get; private set; }

        public ReactiveCommand<MiconFile> SaveCommand { get; private set; }

        public ReactiveCommand<object> NewCommand { get; private set; }

        public ReactiveCommand<object> AboutCommand { get; private set; }

        public ReactiveCommand<object> QuitCommand { get; private set; }

        #endregion

        #region Command execution

        private async Task<MiconFile> ExecuteLoadCommand(object parameter)
        {
            var path = parameter as string;
            var f = await this.storage.Load<MiconFile>(path);

            this.icons = f.Icons.Icons;
            this.LogoPath = f.LogoPath;
            this.BackgroundColor = f.BackgroundColor;
            this.BackgroundEndColor = f.BackgroundEndColor;
            this.BackgroundHasBorder = f.BackgroundHasBorder;
            this.BackgroundShape = f.BackgroundShape;
            this.LogoScale = f.Scale;
            this.GradientMode = f.GradientMode;
            
            return f;
        }

        private async Task<MiconFile> ExecuteSaveCommand(object parameter)
        {
            var path = parameter as string;

            var f = new MiconFile()
            {
                Icons = new MiconIconSet() { Icons = icons },
                Preview = iconsPreview,
                LogoPath = this.LogoPath,
                GradientMode = this.GradientMode,
                BackgroundHasBorder = this.BackgroundHasBorder,
                BackgroundColor = this.BackgroundColor,
                BackgroundEndColor = this.BackgroundEndColor,
                BackgroundShape = this.BackgroundShape,
                Scale = this.LogoScale,
            };

            await this.storage.Save(path, f);
            
            return f;
        }

        private async Task<IImage> ExecuteLoadImageCommand(object parameter)
		{
			var path = parameter as string;

            if(!string.IsNullOrEmpty(path))
            {
                var r = this.platform.LoadImage(path);
                this.path = path;
                return r;
            }

            return null;
        }

        private void ExecuteNewCommand(object parameter)
        {
            this.icons = this.defaultIcons;
            this.iconsPreview = defaultIconPreview;
            this.LogoPath = null;
            this.LogoScale = 1.0;
            this.BackgroundColor = Color.FromRGB(0x4599d9);
            this.BackgroundEndColor = Color.FromRGB(0x4599d9);
            this.BackgroundHasBorder = true;
            this.BackgroundShape = Shape.Rectangle;
            this.GradientMode = GradientMode.None;
        }

        private void ExecuteAboutCommand(object parameter)
        {
            this.launcher.Open("https://github.com/aloisdeniel/Micon/");
        }

        private void ExecuteQuitCommand(object parameter)
        {
            this.launcher.Quit();
        }

        private async Task<string> ExecuteExportCommand(object parameter)
        {
            var output = parameter as string;

            foreach (var icon in this.icons)
            {
                var bitmap = Generate(icon);
                bitmap.GetImage().SaveAsPng(System.IO.Path.Combine(output, $"{icon.Name}.png"));
            }
            
            return output;
        }

        #endregion

        #region Preview

        #endregion

        #region Icons

        private IImageCanvas Generate(Icon icon)
        {
            return this.Generate(icon, this.Logo, this.LogoScale, this.BackgroundColor, this.BackgroundEndColor, this.BackgroundShape, this.BackgroundHasBorder , this.GradientMode);
        }

        private IImageCanvas Generate(Icon icon, IImage logo, double scale, Color color, Color endColor, Shape shape, bool hasBorder, GradientMode gradient)
        {
            var result = new Icon(icon);
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


        private PreviewItemViewModel CreatePreview(IImage logo, double scale, Color color, Color endColor, Shape shape, bool hasBorder, GradientMode gradient)
        {
            return new PreviewItemViewModel()
            {
                Apple = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"iOS/{this.iconsPreview.Apple}"), logo, scale, color, endColor, shape, hasBorder, gradient)?.GetImage(),
                Android = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"Android/{this.iconsPreview.Android}"), logo, scale, color, endColor, shape, hasBorder, gradient)?.GetImage(),
                Windows = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"Windows/{this.iconsPreview.Windows}"), logo, scale, color, endColor, shape, hasBorder, gradient)?.GetImage(),
                WindowsSmall = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"Windows/{this.iconsPreview.WindowsSmall}"), logo, scale, color, endColor, shape, hasBorder, gradient)?.GetImage(),
                WindowsWide = this.Generate(this.icons.FirstOrDefault((i) => i.Name == $"Windows/{this.iconsPreview.WindowsWide}"), logo, scale, color, endColor, shape, hasBorder, gradient)?.GetImage(),
            };
        }

        private Color GetEndColor(Color color, Color endColor, GradientMode gradient)
        {
            if (gradient == GradientMode.Auto)
            {
                endColor = color.Lerp(color.CreateGradientOpposite(), color.Brightness * 0.75);
            }
            else if (this.GradientMode == GradientMode.None)
            {
                endColor = color;
            }

            return endColor;
        }

        #endregion
    }
}

