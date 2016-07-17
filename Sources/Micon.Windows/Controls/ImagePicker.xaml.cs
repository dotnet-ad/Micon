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
        
        public static readonly DependencyProperty TitleProperty =  DependencyProperty.Register("Title", typeof(string), typeof(ImagePicker), new PropertyMetadata(null));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(ImagePicker), new PropertyMetadata(null));

        public string SizeDescription
        {
            get { return (string)GetValue(SizeDescriptionProperty); }
            set { SetValue(SizeDescriptionProperty, value); }
        }

        public static readonly DependencyProperty SizeDescriptionProperty = DependencyProperty.Register("SizeDescription", typeof(string), typeof(ImagePicker), new PropertyMetadata(null));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        
        public static readonly DependencyProperty PathProperty =  DependencyProperty.Register("Path", typeof(string), typeof(ImagePicker), new FrameworkPropertyMetadata(null,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPathChanged));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(ImagePicker), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|JPEG Files (*.jpeg)|*.jpeg|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            
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
            picker.preview.Source = new BitmapImage(new Uri(e.NewValue as string));

        }
    }
}
