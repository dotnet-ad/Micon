namespace Micon.Portable.ViewModels.Items
{
    using Graphics;
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;

    public class PreviewItemViewModel : ReactiveObject
	{
		public PreviewItemViewModel()
        {

		}

        [Reactive]
        public IBitmap Apple { get; set; }

        [Reactive]
        public IBitmap Android { get; set; }

        [Reactive]
        public IBitmap Windows { get; set; }

        [Reactive]
        public IBitmap WindowsWide { get; set; }

        [Reactive]
        public IBitmap WindowsSmall { get; set; }
    }
}

