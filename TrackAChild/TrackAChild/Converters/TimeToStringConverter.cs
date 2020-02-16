using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace TrackAChild.Converters
{
    public class TimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            TimeSpan timeSpan = (TimeSpan)value;
            DateTime dateTime = DateTime.MinValue + timeSpan;

            DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            string shortTimePattern
                = dateTimeFormat.LongTimePattern.Replace(":ss", string.Empty).Replace(":s", string.Empty);

            return dateTime.ToString(shortTimePattern);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return value;
        }
    }
}
