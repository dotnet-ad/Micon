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
    public enum SystemMode
    {
        All,
        Android,
        iOS,
    }

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

        public static readonly DependencyProperty SystemModeProperty = DependencyProperty.Register("SystemMode", typeof(SystemMode), typeof(SystemSelector), new FrameworkPropertyMetadata(SystemMode.All, OnSystemModePropertyChanged));

        public SystemMode SystemMode
        {
            get { return (SystemMode)GetValue(SystemModeProperty); }
            set { SetValue(SystemModeProperty, value); }
        }

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(SystemSelector), new FrameworkPropertyMetadata(0, OnSelectedIndexPropertyChanged));

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        private static void OnSystemModePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as SystemSelector;
            var mode = (SystemMode)e.NewValue;

            control.ios.Visibility = mode != SystemMode.Android ? Visibility.Visible : Visibility.Collapsed;
            control.android.Visibility = mode != SystemMode.iOS ? Visibility.Visible : Visibility.Collapsed;

            if(mode == SystemMode.Android)
            {
                control.SelectedIndex = 1;
                control.android.Margin = new Thickness(0);
            }
            else if (mode == SystemMode.iOS)
            {
                control.SelectedIndex = 0;
                control.android.Margin = new Thickness(0);
            }
            else
            {
                control.android.Margin = new Thickness(10,0,0,0);
            }
        }

        private static void OnSelectedIndexPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as SystemSelector;
            var index = (int)e.NewValue;

            var isIos = index == 0;
            control.ios.Fill = isIos ? control.selectedBrush : control.unselectedBrush;
            control.android.Fill = !isIos ? control.selectedBrush : control.unselectedBrush;
            control.label.Text = isIos ? "iOS" : "Android";

            if (control.SelectedItemChanged != null)
            {
                control.SelectedItemChanged(control, index);
            }
        }

        public event EventHandler<int> SelectedItemChanged;

        private readonly Brush selectedBrush, unselectedBrush;

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selected = sender as Path;
            this.SelectedIndex = (this.ios == selected) ? 0 : 1;
        }
    }
}
