using Micon.Portable.Generation;
using Micon.Windows.Bitmaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Micon.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test();

            this.DataContext = new Portable.HomeViewModel();

            this.SizeChanged += MainWindow_SizeChanged;
            // Do any additional setup after loading the view.
            this.UpdatePanel(this.RenderSize);
        }
        const int min = 1200;

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(e.PreviousSize.Width <=  min && e.NewSize.Width > min || e.PreviousSize.Width >= min && e.NewSize.Width < min)
            {
                UpdatePanel(e.NewSize);
            }
        }

        private void UpdatePanel(Size size)
        {
            if (size.Width > min)
            {
                this.widePanel.Visibility = Visibility.Visible;
                this.smallPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                this.widePanel.Visibility = Visibility.Hidden;
                this.smallPanel.Visibility = Visibility.Visible;
            }
        }

        private async void Test()
        {
            try
            {
                var loader = new WindowsBitmapLoader();
                var gen = new IconGenerator(loader);
                await gen.Generate(@"C:\test\profile.jpg", @"C:\test\profile.jpg", new Portable.Generation.SystemIcons()
                {
                    Name = "iOS",
                    Icons = new System.Collections.Generic.List<Icon>
                    {
                        new Icon("test16.png",16),
                        new Icon("test150.png",150,100),
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
