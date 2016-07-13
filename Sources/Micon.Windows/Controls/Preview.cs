using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Micon.Windows.Controls
{
    public class Preview : UserControl
    {

        public static readonly DependencyProperty IsAnimatedVisibleProperty = DependencyProperty.Register("IsAnimatedVisible", typeof(bool), typeof(Preview), new FrameworkPropertyMetadata(false, OnVisiblePropertyChanged));

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
                var sb = (Storyboard)control.Resources["ShowStoryboard"];
                sb.Begin();
            }
            else
            {
                var sb = (Storyboard)control.Resources["HideStoryboard"];
                sb.Begin();

            }
        }
    }
}
