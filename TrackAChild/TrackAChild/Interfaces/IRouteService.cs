using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrackAChild.Models;

namespace TrackAChild.Interfaces
{
    internal interface IRouteService
    {
        ObservableCollection<RouteModel> Routes { get; set; }
        void AddRoute(RouteModel route);
        void RemoveRoute(RouteModel route);
        void SetRouteToEdit(RouteModel route);
        void AssignBusToRoute(BusModel busModel);
        void AssignPassengersToRoute(List<PassengerModel> passengers);
        RouteModel GetRouteToEdit();
    }
}
