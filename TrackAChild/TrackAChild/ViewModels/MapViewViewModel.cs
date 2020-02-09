using TrackAChild.Interfaces;
using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using System.IO;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls.Maps;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI;
using Windows.Devices.Geolocation;
using TrackAChild.Services;
using TrackAChild.Helpers;

namespace TrackAChild.ViewModels
{
    public class MapViewViewModel : Observable
    {
        public MapControl Map { get; private set; }
        IRouteService routeService;

        private const double DefaultZoomLevel = 17;

        private readonly BasicGeoposition defaultPosition = new BasicGeoposition()
        {
            Latitude = 47.609425,
            Longitude = -122.3417
        };

        private double zoomLevel;

        public double ZoomLevel
        {
            get { return zoomLevel; }
            set { Set(ref zoomLevel, value); }
        }

        private Geopoint _center;

        public Geopoint Center
        {
            get { return _center; }
            set { Set(ref _center, value); }
        }

        public MapViewViewModel()
        {
            Center = new Geopoint(defaultPosition);
            ZoomLevel = DefaultZoomLevel;

            routeService = (App.Current as App).Container.GetService<IRouteService>();

            // load some routing data and build a routing network.
            var routerDb = new RouterDb();

            var s = (App.Current as App).Container.GetService<IOSPathDependencies>().GetStoragePath();
            var fullpath = "test.pbf";

            using (var stream = new FileInfo(fullpath).OpenRead())
            {
                // create the network for cars only.
                routerDb.LoadOsmData(stream, Vehicle.Car);
            }

            // create a router.
            var router = new Router(routerDb);

            // get a profile.
            var profile = Vehicle.Car.Fastest(); // the default OSM car profile.

            // create a routerpoint from a location.
            // snaps the given location to the nearest routable edge.
            var start = router.Resolve(profile, 51.443570f, -0.344270f);
            var end = router.Resolve(profile, 51.425330f, -0.370930f);

            // calculate a route.
            var route = router.Calculate(profile, start, end);

            // Create map
            Map = new MapControl();

            // Add polylines
            List<BasicGeoposition> positionValues = new List<BasicGeoposition>();
            foreach (var stop in route.Shape)
            {
                positionValues.Add(new BasicGeoposition { Latitude = stop.Latitude, Longitude = stop.Longitude });
            }

            MapPolyline polyline = new MapPolyline
            {
                StrokeColor = Colors.Blue,
                StrokeThickness = 12
            };

            polyline.Path = new Geopath(positionValues);

            Map.MapElements.Add(polyline);
        }
    }
}
