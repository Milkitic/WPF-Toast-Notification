using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace notify
{
    public class EmptyToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                return string.IsNullOrEmpty(s) ? Visibility.Collapsed : Visibility.Visible;
            }

            if (value is null)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}