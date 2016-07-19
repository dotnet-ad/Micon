namespace Micon.Windows.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Portable.Graphics;
    using NGraphics;
    public class Preview : UserControl
    {
        public string ScreenBackground
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public System.Drawing.Brush ScreenForeground
        {
            get { return (System.Drawing.Brush)GetValue(ScreenForegroundProperty); }
            set { SetValue(ScreenForegroundProperty, value); }
        }

        public IImage Icon
        {
            get { return (IImage)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(IImage), typeof(Preview), new PropertyMetadata(null));

        public static readonly DependencyProperty ScreenForegroundProperty = DependencyProperty.Register(nameof(ScreenForeground), typeof(System.Windows.Media.Brush), typeof(Preview), new PropertyMetadata((System.Windows.Media.Brush)new SolidColorBrush(System.Windows.Media.Colors.White)));

        public static readonly DependencyProperty ScreenBackgroundProperty = DependencyProperty.Register(nameof(ScreenBackground), typeof(string), typeof(Preview), new PropertyMetadata(null));
        
        public static readonly DependencyProperty IsAnimatedVisibleProperty = DependencyProperty.Register(nameof(IsAnimatedVisible), typeof(bool), typeof(Preview), new FrameworkPropertyMetadata(false, OnVisiblePropertyChanged));

        public bool IsAnimatedVisible
        {
            get { return (bool)GetValue(IsAnimatedVisibleProperty); }
            set { SetValue(IsAnimatedVisibleProperty, value); }
        }

        private static void OnVisiblePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as Preview;
            var visible = (bool)e.NewValue;

            if (visible)
            {
                control.Show();
            }
            else
            {
                control.Hide();
            }
        }

        public void Show()
        {
            var sb = (Storyboard)this.Resources["ShowStoryboard"];
            sb.Begin();
        }

        public void Hide()
        {
            var sb = (Storyboard)this.Resources["HideStoryboard"];
            sb.Begin();
        }
    }
}
