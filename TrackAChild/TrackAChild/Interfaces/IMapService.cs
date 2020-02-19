using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace TrackAChild.Interfaces
{
    internal interface IMapService
    {
        Task<MapRouteView> CalculateRoute(List<BasicGeoposition> basicGeopositions);
    }
}
