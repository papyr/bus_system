using TrackAChild.Interfaces;
using System;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TrackAChild.Services
{
    public class MapService : IMapService
    {
        public MapService()
        {
        }

        public async Task<MapRouteView> CalculateRoute(List<BasicGeoposition> basicGeopositions)
        {
            // Get Driving Route from first point to end point going through all
            // points in between
            var path = new List<EnhancedWaypoint>();

            for (int waypointIndex = 0; waypointIndex < basicGeopositions.Count; ++waypointIndex)
            {
                if (waypointIndex == 0 || waypointIndex == basicGeopositions.Count - 1)
                {
                    path.Add(new EnhancedWaypoint(new Geopoint(basicGeopositions[waypointIndex]), WaypointKind.Stop));
                }
                else
                {
                    path.Add(new EnhancedWaypoint(new Geopoint(basicGeopositions[waypointIndex]), WaypointKind.Via));
                }
            }

            // Get the route between the points.
            MapRouteFinderResult routeResult =
                  await MapRouteFinder.GetDrivingRouteFromEnhancedWaypointsAsync(path,
                  new MapRouteDrivingOptions()
                  { RouteOptimization = MapRouteOptimization.TimeWithTraffic, RouteRestrictions = MapRouteRestrictions.None });

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                return new MapRouteView(routeResult.Route);
            }
            else
            {
                return null;
            }
        }
    }
}
