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
        Windows,
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

            control.ios.Visibility = mode == SystemMode.iOS || mode == SystemMode.All ? Visibility.Visible : Visibility.Collapsed;
            control.android.Visibility = mode == SystemMode.Android || mode == SystemMode.All ? Visibility.Visible : Visibility.Collapsed;
            control.windows.Visibility = mode == SystemMode.Windows || mode == SystemMode.All ? Visibility.Visible : Visibility.Collapsed;

            if(mode == SystemMode.All)
            {
                control.android.Margin = new Thickness(20, 0, 0, 0);
                control.windows.Margin = new Thickness(20, 0, 0, 0);
            }
            else
            {
                control.android.Margin = new Thickness(0);
                control.windows.Margin = new Thickness(0);

                if (mode == SystemMode.Android)
                {
                    control.SelectedIndex = 1;
                }
                else if (mode == SystemMode.iOS)
                {
                    control.SelectedIndex = 0;
                }
                else if (mode == SystemMode.Windows)
                {
                    control.SelectedIndex = 2;
                }
            }
        }

        private static string[] labels = new[] { "iOS", "Android", "Windows" };

        private static void OnSelectedIndexPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as SystemSelector;
            var index = (int)e.NewValue;

            var isIos = index == 0;
            control.ios.Fill = index == 0 ? control.selectedBrush : control.unselectedBrush;
            control.android.Fill = index == 1 ? control.selectedBrush : control.unselectedBrush;
            control.windows.Fill = index == 2 ? control.selectedBrush : control.unselectedBrush;
            control.label.Text = labels[index];

            control.SelectedItemChanged?.Invoke(control, index);
        }

        public event EventHandler<int> SelectedItemChanged;

        private readonly Brush selectedBrush, unselectedBrush;

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selected = (sender as Border).Child as Path;
            this.SelectedIndex = (this.ios == selected) ? 0 : ((this.android == selected) ? 1 : 2);
        }
    }
}
