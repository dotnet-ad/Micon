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
    /// Interaction logic for ImagePicker.xaml
    /// </summary>
    public partial class ImagePicker : UserControl
    {
        public ImagePicker()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        
        public static readonly DependencyProperty TitleProperty =  DependencyProperty.Register(nameof(Title), typeof(string), typeof(ImagePicker), new PropertyMetadata(null));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(string), typeof(ImagePicker), new PropertyMetadata(null));

        public string SizeDescription
        {
            get { return (string)GetValue(SizeDescriptionProperty); }
            set { SetValue(SizeDescriptionProperty, value); }
        }

        public static readonly DependencyProperty SizeDescriptionProperty = DependencyProperty.Register(nameof(SizeDescription), typeof(string), typeof(ImagePicker), new PropertyMetadata(null));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        
        public static readonly DependencyProperty PathProperty =  DependencyProperty.Register(nameof(Path), typeof(string), typeof(ImagePicker), new FrameworkPropertyMetadata(null,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPathChanged));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(ImagePicker), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScaleChanged));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG|*.png|jpg|*.jpg|jpeg|*.jpeg|gif|*.gif";
            
            var result = dlg.ShowDialog();
            
            if (result == true)
            {
                this.Path = dlg.FileName;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Scale = e.NewValue;
        }

        private static void OnPathChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var picker = source as ImagePicker;
            var path = e.NewValue as string;
            picker.preview.Source = (path != null) ? new BitmapImage(new Uri(path)) : null;
        }

        private static void OnScaleChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var picker = source as ImagePicker;
            picker.scaleSlider.Value = (double)e.NewValue;
        }
    }
}
