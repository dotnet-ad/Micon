namespace Micon.Windows.Converters
{
    using Graphics;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bitmap = value as WindowsBitmap;
            return bitmap?.Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
