namespace Micon.Portable.ViewModels.Items
{
    using Graphics;
    using NGraphics;
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;

    public class PreviewItemViewModel : ReactiveObject
	{
		public PreviewItemViewModel()
        {

		}

        [Reactive]
        public IImage Apple { get; set; }

        [Reactive]
        public IImage Android { get; set; }

        [Reactive]
        public IImage Windows { get; set; }

        [Reactive]
        public IImage WindowsWide { get; set; }

        [Reactive]
        public IImage WindowsSmall { get; set; }
    }
}

