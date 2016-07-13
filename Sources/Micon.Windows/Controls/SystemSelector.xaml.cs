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
    /// Interaction logic for SystemSelector.xaml
    /// </summary>
    public partial class SystemSelector : UserControl
    {
        public SystemSelector()
        {
            InitializeComponent();

            this.selectedBrush = App.Current.Resources["MiconAccentBrush"] as Brush;
            this.unselectedBrush = App.Current.Resources["MiconSemiDarkBackgroundBrush"] as Brush;
            
        }

        public event EventHandler<int> SelectedItemChanged;

        private readonly Brush selectedBrush, unselectedBrush;

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selected = sender as Path;

            var index = (this.ios == selected) ? 0 : 1;
            var isIos = index == 0;

            this.ios.Fill = isIos ? selectedBrush : unselectedBrush;
            this.android.Fill = !isIos ? selectedBrush : unselectedBrush;
            this.label.Text = isIos ? "iOS" : "Android";

            if (this.SelectedItemChanged != null)
            {
                this.SelectedItemChanged(this, index);
            }
        }
    }
}
