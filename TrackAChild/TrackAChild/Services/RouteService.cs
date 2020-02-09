using System.Collections.ObjectModel;
using TrackAChild.Interfaces;

namespace TrackAChild.Services
{
    public class RouteService : IRouteService
    {
        public ObservableCollection<RouteModel> Routes { get; set; }

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
    }
}
