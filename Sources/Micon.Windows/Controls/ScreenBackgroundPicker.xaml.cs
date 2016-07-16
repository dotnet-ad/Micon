using Micon.Portable.Generation;
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
    /// Interaction logic for ScreenBackgroundPicker.xaml
    /// </summary>
    public partial class ScreenBackgroundPicker : UserControl
    {
        public ScreenBackgroundPicker()
        {
            InitializeComponent();
            this.selectedBrush = App.Current.Resources["MiconAccentBrush"] as Brush;
            this.unselectedBrush = new SolidColorBrush(System.Windows.Media.Colors.Transparent);
        }

        public static readonly DependencyProperty ScreenBackgroundProperty = DependencyProperty.Register(nameof(ScreenBackground), typeof(ScreenBackground), typeof(ScreenBackgroundPicker), new FrameworkPropertyMetadata(ScreenBackground.Dark, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScreenBackgroundPropertyChanged));

        private readonly Brush selectedBrush;

        private readonly Brush unselectedBrush;

        public ScreenBackground ScreenBackground
        {
            get { return (ScreenBackground)GetValue(ScreenBackgroundProperty); }
            set { SetValue(ScreenBackgroundProperty, value); }
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var isLight = (sender == light);
            this.ScreenBackground = isLight ? ScreenBackground.Light : ScreenBackground.Dark;
        }


        private static void OnScreenBackgroundPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as ScreenBackgroundPicker;
            var value = (ScreenBackground)e.NewValue;
            
            var isLight = (value == ScreenBackground.Light);
            control.ScreenBackground = isLight ? ScreenBackground.Light : ScreenBackground.Dark;
            control.light.BorderBrush = isLight ? control.selectedBrush : control.unselectedBrush;
            control.dark.BorderBrush = !isLight ? control.selectedBrush : control.unselectedBrush;


        }
    }
}
