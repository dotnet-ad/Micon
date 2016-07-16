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
    /// Interaction logic for BackgroundPicker.xaml
    /// </summary>
    public partial class BackgroundPicker : UserControl
    {
        public BackgroundPicker()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(BackgroundPicker), new PropertyMetadata(null));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(BackgroundPicker), new PropertyMetadata(null));
        
        public string AndroidDescription
        {
            get { return (string)GetValue(AndroidDescriptionProperty); }
            set { SetValue(AndroidDescriptionProperty, value); }
        }

        public static readonly DependencyProperty AndroidDescriptionProperty = DependencyProperty.Register("AndroidDescription", typeof(string), typeof(BackgroundPicker), new PropertyMetadata(null));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(BackgroundPicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(BackgroundPicker), new FrameworkPropertyMetadata(Color.FromArgb(0,0,0,0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public static readonly DependencyProperty EndColorProperty = DependencyProperty.Register("EndColor", typeof(Color), typeof(BackgroundPicker), new FrameworkPropertyMetadata(Color.FromArgb(0, 0, 0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Portable.Generation.Shape Shape
        {
            get { return (Portable.Generation.Shape)GetValue(ShapeProperty); }
            set { SetValue(ShapeProperty, value); }
        }

        public static readonly DependencyProperty ShapeProperty = DependencyProperty.Register("Shape", typeof(Portable.Generation.Shape), typeof(BackgroundPicker), new FrameworkPropertyMetadata(Portable.Generation.Shape.Rectangle, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Portable.Generation.GradientMode GradientMode
        {
            get { return (Portable.Generation.GradientMode)GetValue(GradientModeProperty); }
            set { SetValue(GradientModeProperty, value); }
        }

        public static readonly DependencyProperty GradientModeProperty = DependencyProperty.Register("GradientMode", typeof(Portable.Generation.GradientMode), typeof(BackgroundPicker), new FrameworkPropertyMetadata(Portable.Generation.GradientMode.None, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if(e.NewValue != null)
                this.Color = e.NewValue.Value;
        }

        private void endColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue != null)
                this.EndColor = e.NewValue.Value;
        }



        private void shapes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.shapes.SelectedIndex == 0)
            {
                this.Shape = Portable.Generation.Shape.Rectangle;
            }
            else if (this.shapes.SelectedIndex == 1)
            {
                this.Shape = Portable.Generation.Shape.RoundedRectangle;
            }
            else if (this.shapes.SelectedIndex == 2)
            {
                this.Shape = Portable.Generation.Shape.Circle;
            }
        }

        private void gradients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gradients.SelectedIndex == 0)
            {
                this.GradientMode = Portable.Generation.GradientMode.None;
                this.endColorPicker.Visibility = Visibility.Collapsed;
            }
            else if (this.gradients.SelectedIndex == 1)
            {
                this.GradientMode = Portable.Generation.GradientMode.Auto;
                this.endColorPicker.Visibility = Visibility.Collapsed;
            }
            else if (this.gradients.SelectedIndex == 2)
            {
                this.GradientMode = Portable.Generation.GradientMode.Select;
                this.endColorPicker.Visibility = Visibility.Visible;
            }
        }
    }
}
