using System;
using Windows.UI.Xaml.Data;

namespace TrackAChild.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return !(bool)value;
        }
    }
}
