using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using TrackAChild.Models;

namespace TrackAChild
{
    public class RouteModel : Observable, IRoute
    {
        private string routeName;
        public string RouteName { get { return routeName; } set { routeName = value; OnPropertyChanged(nameof(RouteName)); } }

        private ObservableCollection<StopModel> stops;
        public ObservableCollection<StopModel> Stops 
        { get { return stops; } set { stops = value; OnPropertyChanged(nameof(Stops)); } }

        public ObservableCollection<StopModel> GetStops() 
        {
            return Stops;
        }

        private List<DateTime> datesForCurrentRoute;
        public List<DateTime> DatesForCurrentRoute {
            get { return datesForCurrentRoute; }
            set { datesForCurrentRoute = value; OnPropertyChanged(nameof(DatesForCurrentRoute)); }
        }

        private BusModel assignedBus = null;
        public BusModel AssignedBus
        {
            get { return assignedBus; }
            set { assignedBus = value; OnPropertyChanged(nameof(AssignedBus)); }
        }

        private void AddStop(StopModel stop) { stops.Add(stop); }
        private void RemoveStop(StopModel stop) { stops.Remove(stop); }
    }
}
