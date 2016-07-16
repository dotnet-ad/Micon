using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Micon.Windows.Converters
{
    public class NativeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var portable = value as Micon.Portable.Generation.Color;
            if (portable == null)
                return null;

            return portable.ToNative();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var native =(Color) value;
            return native.FromNative();
        }
    }
}
