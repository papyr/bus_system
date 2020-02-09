using System.Collections.ObjectModel;

namespace TrackAChild.Interfaces
{
    internal interface IRouteService
    {
        ObservableCollection<RouteModel> Routes { get; set; }
        void AddRoute(RouteModel route);
        void RemoveRoute(RouteModel route);
    }
}
