using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrackAChild.Interfaces;
using TrackAChild.Models;

namespace TrackAChild.Services
{
    public class RouteService : IRouteService
    {
        public ObservableCollection<RouteModel> Routes { get; set; }

        private RouteModel routeToEdit = null;

        public RouteService()
        {
            Routes = new ObservableCollection<RouteModel>();
        }

        public void AddRoute(RouteModel route)
        {
            Routes.Add(route);
        }

        public void RemoveRoute(RouteModel route)
        {
            Routes.Remove(route);
        }

        public void SetRouteToEdit(RouteModel route)
        {
            routeToEdit = route;
        }

        public RouteModel GetRouteToEdit()
        {
            return routeToEdit;
        }

        public void AssignBusToRoute(BusModel busModel)
        {
            routeToEdit.AssignedBus = busModel;
        }

        public void AssignPassengersToRoute(List<PassengerModel> passengers)
        {
            routeToEdit.AssignedPassengers = passengers;
        }
    }
}
