using System;
using TrackAChild.Models;
using Windows.UI.Xaml.Data;

namespace TrackAChild.Converters
{
    public class AssignedValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return "Unassigned";
            }

            // Get driver name
            DriverModel driverModel = value as DriverModel;
            if (driverModel != null)
            {
                return driverModel.DriverName;
            }

            // If we are here, we have a BusModel object, so let's convert
            BusModel busModel = value as BusModel;
            if (busModel != null)
            {
                return busModel.BusTag;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
