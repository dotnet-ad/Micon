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

namespace Micon.Windows.Controls
{
    /// <summary>
    /// Interaction logic for SmallPreviewPanel.xaml
    /// </summary>
    public partial class SmallPreviewPanel : UserControl
    {
        public SmallPreviewPanel()
        {
            InitializeComponent();
        }

        private void SystemSelector_SelectedItemChanged(object sender, int e)
        {
            this.previewIos.IsAnimatedVisible = e == 0;
            this.previewAndroid.IsAnimatedVisible = e == 1;
            this.previewWindows.IsAnimatedVisible = e == 2;
        }
    }
}
