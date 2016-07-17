using Micon.Portable.Bitmaps;
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
