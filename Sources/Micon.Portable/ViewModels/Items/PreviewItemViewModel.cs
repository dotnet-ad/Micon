using System;
using Micon.Portable.Bitmaps;
using Micon.Portable.Generation;
using ReactiveUI;

namespace Micon.Portable
{
	public class PreviewItemViewModel : ReactiveObject
	{
		public PreviewItemViewModel(IconGenerator generator, IBitmap logo, IBitmap background)
		{
            this.Load(generator, logo, background);
		}

        private async void Load(IconGenerator generator, IBitmap logo, IBitmap background)
        {
            var t1 = generator.GenerateIcon("ios", logo, background, new Icon("ios", 64));
            var t2 = generator.GenerateIcon("android", logo, background, new Icon("android", 64));

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

