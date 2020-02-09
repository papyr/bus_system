using System;
using TrackAChild.Models;
using Windows.UI.Xaml.Data;

namespace TrackAChild.Converters
{
    class PassengerModelToTypeString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value is StudentModel)
                return "Student";
            else if (value is ChaperoneModel)
                return "Chaperone";
            else
                return "Other";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return "";
        }
    }
}
