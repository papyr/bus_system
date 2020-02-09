using TrackAChild.Helpers;
using TrackAChild.Interfaces;

namespace TrackAChild.Models
{
    public class DriverModel : Observable
    {
        private string driverName;
        public string DriverName { get { return driverName; } set { driverName = value; OnPropertyChanged(nameof(DriverName)); } }

        private string driverNumber;
        public string DriverNumber { get { return driverNumber; } set { driverNumber = value; OnPropertyChanged(nameof(DriverNumber)); } }
    }
}
