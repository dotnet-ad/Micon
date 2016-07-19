namespace Micon.Windows.Controls
{
    using NGraphics;
    using Portable.Graphics;
    using System.Windows;

    /// <summary>
    /// Interaction logic for WindowsPreview.xaml
    /// </summary>
    public partial class WindowsPreview
    {
        public WindowsPreview()
        {
            InitializeComponent();
        }
        public IImage WideIcon
        {
            get { return (IImage)GetValue(WideIconProperty); }
            set { SetValue(WideIconProperty, value); }
        }

        public static readonly DependencyProperty WideIconProperty = DependencyProperty.Register(nameof(WideIcon), typeof(IImage), typeof(WindowsPreview), new PropertyMetadata(null));

        public IImage SmallIcon
        {
            get { return (IImage)GetValue(SmallIconProperty); }
            set { SetValue(SmallIconProperty, value); }
        }

        public static readonly DependencyProperty SmallIconProperty = DependencyProperty.Register(nameof(SmallIcon), typeof(IImage), typeof(WindowsPreview), new PropertyMetadata(null));


    }
}
