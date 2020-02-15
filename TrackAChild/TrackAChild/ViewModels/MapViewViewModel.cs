using TrackAChild.Interfaces;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls.Maps;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI;
using Windows.Devices.Geolocation;
using TrackAChild.Helpers;
using System.Collections.ObjectModel;

namespace TrackAChild.ViewModels
{
    public class MapViewViewModel : Observable
    {
        IRouteService routeService;
        IMapService mapService;

        private const double DefaultZoomLevel = 17;

        private readonly BasicGeoposition defaultPosition = new BasicGeoposition()
        {
            Latitude = 47.609425,
            Longitude = -122.3417
        };

        public ObservableCollection<MapLayer> MapLayers
            { get; } = new ObservableCollection<MapLayer>();

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
            mapService = (App.Current as App).Container.GetService<IMapService>();

            // create a routerpoint from a location.
            // snaps the given location to the nearest routable edge.
            var start = mapService.RetrieveRouterPoint(51.443570f, -0.344270f);
            var end = mapService.RetrieveRouterPoint(51.425330f, -0.370930f);

            // calculate a route.
            var route = mapService.CalculateRoute(start, end);

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

            // Proper way is to create a map element layer
            var elements = new List<MapElement>() { polyline };
            var layer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = elements
            };

            MapLayers.Add(layer);
        }
    }
}
