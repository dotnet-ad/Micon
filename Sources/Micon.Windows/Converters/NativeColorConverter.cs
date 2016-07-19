namespace Micon.Windows.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class NativeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var portable = value as Portable.Graphics.Color;
            if (portable == null)
                return null;

            return portable.ToNative();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var native = (Color) value;
            return native.FromNative();
        }
    }
}
