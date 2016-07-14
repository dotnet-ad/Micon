using System;

using AppKit;
using Foundation;
using Micon.Mac.Bitmaps;
using Micon.Portable;
using Micon.Portable.Generation;
using ReactiveUI;
using Splat;

namespace Micon
{
	public partial class ViewController : ReactiveViewController, IViewFor<HomeViewModel>
	{
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.ViewModel = new HomeViewModel();

			Test();
			// Do any additional setup after loading the view.
		}

		private async void Test()
		{
			try
			{
				var loader = new MacBitmapLoader();
				var gen = new IconGenerator(loader);
				await gen.Generate("/Users/alois/Documents/profile_pic.jpeg","/Users/alois/Documents/profile_pic.jpeg", "/Users/alois/Documents/icons", new Portable.Generation.SystemIcons()
				{
					Name = "iOS",
					Icons = new System.Collections.Generic.List<Icon>
					{
						new Icon("test16.png",16),
						new Icon("test150.png",150,100) { HasBackground = true },
					}
				});
			}
			catch (Exception ex)
			{

			}

		}

		public override NSObject RepresentedObject
		{
			get { return base.RepresentedObject; }
			set { base.RepresentedObject = value; }
		}

		#region Reactive implementation

		private HomeViewModel viewModel;

		public HomeViewModel ViewModel
		{
			get { return this.viewModel;}
			set { this.RaiseAndSetIfChanged(ref viewModel, value); }
		}

		object IViewFor.ViewModel
		{
			get { return this.ViewModel; }
			set { this.ViewModel = (HomeViewModel)value; }
		}

		#endregion
	}
}
