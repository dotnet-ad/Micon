namespace Micon.Windows.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    
    /// <summary>
    /// Interaction logic for BackgroundPicker.xaml
    /// </summary>
    public partial class BackgroundPicker : UserControl
    {
        public BackgroundPicker()
        {
            InitializeComponent();
            this.shapes.SelectedIndex = 0;
            this.gradients.SelectedIndex = 0;
        }
        
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(BackgroundPicker), new PropertyMetadata(null));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(string), typeof(BackgroundPicker), new PropertyMetadata(null));
        
        public string AndroidDescription
        {
            get { return (string)GetValue(AndroidDescriptionProperty); }
            set { SetValue(AndroidDescriptionProperty, value); }
        }

        public static readonly DependencyProperty AndroidDescriptionProperty = DependencyProperty.Register(nameof(AndroidDescription), typeof(string), typeof(BackgroundPicker), new PropertyMetadata(null));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(nameof(Path), typeof(string), typeof(BackgroundPicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(BackgroundPicker), new FrameworkPropertyMetadata(Color.FromArgb(0,0,0,0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBackgroundColorChanged));

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public static readonly DependencyProperty EndColorProperty = DependencyProperty.Register(nameof(EndColor), typeof(Color), typeof(BackgroundPicker), new FrameworkPropertyMetadata(Color.FromArgb(0, 0, 0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBackgroundEndColorChanged));

        public Portable.Graphics.Shape Shape
        {
            get { return (Portable.Graphics.Shape)GetValue(ShapeProperty); }
            set { SetValue(ShapeProperty, value); }
        }

        public static readonly DependencyProperty ShapeProperty = DependencyProperty.Register(nameof(Shape), typeof(Portable.Graphics.Shape), typeof(BackgroundPicker), new FrameworkPropertyMetadata(Portable.Graphics.Shape.Rectangle, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,OnShapeChanged));

        public Portable.Graphics.GradientMode GradientMode
        {
            get { return (Portable.Graphics.GradientMode)GetValue(GradientModeProperty); }
            set { SetValue(GradientModeProperty, value); }
        }

        public static readonly DependencyProperty GradientModeProperty = DependencyProperty.Register(nameof(GradientMode), typeof(Portable.Graphics.GradientMode), typeof(BackgroundPicker), new FrameworkPropertyMetadata(Portable.Graphics.GradientMode.None, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,OnGradientChanged));

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public static readonly DependencyProperty HasBorderProperty = DependencyProperty.Register(nameof(HasBorder), typeof(bool), typeof(BackgroundPicker), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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
                this.Shape = Portable.Graphics.Shape.Rectangle;
            }
            else if (this.shapes.SelectedIndex == 1)
            {
                this.Shape = Portable.Graphics.Shape.RoundedRectangle;
            }
            else if (this.shapes.SelectedIndex == 2)
            {
                this.Shape = Portable.Graphics.Shape.Circle;
            }
        }

        private void gradients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gradients.SelectedIndex == 0)
            {
                this.GradientMode = Portable.Graphics.GradientMode.None;
                this.endColorPicker.Visibility = Visibility.Collapsed;
            }
            else if (this.gradients.SelectedIndex == 1)
            {
                this.GradientMode = Portable.Graphics.GradientMode.Auto;
                this.endColorPicker.Visibility = Visibility.Collapsed;
            }
            else if (this.gradients.SelectedIndex == 2)
            {
                this.GradientMode = Portable.Graphics.GradientMode.Select;
                this.endColorPicker.Visibility = Visibility.Visible;
            }
        }

        private static void OnBackgroundColorChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var picker = source as BackgroundPicker;
            var color = (Color)e.NewValue;
            picker.colorPicker.SelectedColor = color;
        }

        private static void OnBackgroundEndColorChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var picker = source as BackgroundPicker;
            var color = (Color)e.NewValue;
            picker.endColorPicker.SelectedColor = color;
        }

        private static void OnShapeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var picker = source as BackgroundPicker;
            var shape = (Portable.Graphics.Shape)e.NewValue;

            switch (shape)
            {
                case Portable.Graphics.Shape.RoundedRectangle:
                    picker.shapes.SelectedIndex = 1;
                    break;
                case Portable.Graphics.Shape.Circle:
                    picker.shapes.SelectedIndex = 2;
                    break;
                default:
                    picker.shapes.SelectedIndex = 0;
                    break;
            }
        }
        private static void OnGradientChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var picker = source as BackgroundPicker;
            var gradient = (Portable.Graphics.GradientMode)e.NewValue;

            switch (gradient)
            {
                case Portable.Graphics.GradientMode.Auto:
                    picker.gradients.SelectedIndex = 1;
                    break;
                case Portable.Graphics.GradientMode.Select:
                    picker.gradients.SelectedIndex = 2;
                    break;
                default:
                    picker.gradients.SelectedIndex = 0;
                    break;
            }
        }
    }
}
