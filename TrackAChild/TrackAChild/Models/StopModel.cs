using System;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
namespace TrackAChild
{
    public class StopModel : Observable, ICloneable
    {
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }

        private decimal latitude;
        public decimal Latitude { get { return latitude; } set { latitude = value; OnPropertyChanged(nameof(Latitude)); } }

        private decimal longitude;
        public decimal Longitude { get { return longitude; } set { longitude = value; OnPropertyChanged(nameof(Longitude)); } }

        private string address;
        public string Address { get { return address; } set { address = value; OnPropertyChanged(nameof(Address)); } }

        private TimeSpan arrivalTime;
        public TimeSpan ArrivalTime { get { return arrivalTime; } set { arrivalTime = value; OnPropertyChanged(nameof(ArrivalTime)); } }

        private string status = StatusEnum.Waiting.ToString();
        public string Status { get { return status; } set { status = value; OnPropertyChanged(nameof(Status)); } }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
