namespace SiteManager.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(bool), typeof(TextWrapping))]
    public class BoolToWordWrapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b ? TextWrapping.Wrap : TextWrapping.NoWrap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TextWrapping wrapping && wrapping == TextWrapping.Wrap;
        }
    }

    [ValueConversion(typeof(bool), typeof(TextWrapping))]
    public class InverseBoolToWordWrapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b ? TextWrapping.NoWrap : TextWrapping.Wrap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TextWrapping wrapping && wrapping == TextWrapping.NoWrap;
        }
    }
}
