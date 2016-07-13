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
            // Do any additional setup after loading the view.
        }

        private async void Test()
        {
            try
            {
                var loader = new WindowsBitmapLoader();
                var gen = new IconGenerator(loader);
                await gen.Generate(@"C:\test\profile.jpg", @"C:\test\profile.jpg", @"C:\test\icons", new Portable.Generation.SystemIcons()
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

        private void SystemSelector_SelectedItemChanged(object sender, int e)
        {
            this.previewIos.IsAnimatedVisible = e == 0;
            this.previewAndroid.IsAnimatedVisible = e == 1;
        }
    }
}
