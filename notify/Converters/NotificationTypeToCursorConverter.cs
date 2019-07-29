using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace Notification.Converters
{
    public class NotificationTypeToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is NotificationType actualType && actualType == NotificationType.Alert
                ? Cursors.Hand
                : Cursors.Arrow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}