namespace Micon.Windows.Controls
{
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
        public IBitmap WideIcon
        {
            get { return (IBitmap)GetValue(WideIconProperty); }
            set { SetValue(WideIconProperty, value); }
        }

        public static readonly DependencyProperty WideIconProperty = DependencyProperty.Register(nameof(WideIcon), typeof(IBitmap), typeof(WindowsPreview), new PropertyMetadata(null));

        public IBitmap SmallIcon
        {
            get { return (IBitmap)GetValue(SmallIconProperty); }
            set { SetValue(SmallIconProperty, value); }
        }

        public static readonly DependencyProperty SmallIconProperty = DependencyProperty.Register(nameof(SmallIcon), typeof(IBitmap), typeof(WindowsPreview), new PropertyMetadata(null));


    }
}
