﻿namespace Micon.Windows.Converters
{
    using Portable.Generation;
    using Portable.ViewModels.Items;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ScreenBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bg = (ScreenBackground)value;
            
            if(targetType == typeof(string))
            {
                switch (bg)
                {
                    case ScreenBackground.Light: return "/Assets/device_background_002.png";
                    default: return "/Assets/device_background_001.png";
                }
            }
            
            switch (bg)
            {
                case ScreenBackground.Light: return new SolidColorBrush(Colors.Black) ;
                default: return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
