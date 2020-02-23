using TrackAChild.Interfaces;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls.Maps;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI;
using Windows.Devices.Geolocation;
using TrackAChild.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using Windows.Services.Maps;
using System;
using System.Threading.Tasks;

namespace TrackAChild.ViewModels
{
    public class MapViewViewModel : Observable
    {
        IRouteService routeService;
        IMapService mapService;

        public ICommand MapLoaded { get; set; }

        public MapControl Map { get; private set; }

        public ObservableCollection<RouteModel> Routes
        {
            get { return routeService.Routes; }
        }

        private RouteModel routeSelected;
        public RouteModel RouteSelected
        {
            get { return routeSelected; }
            set
            {
                if (routeSelected != value && value != null)
                {
                    Set(ref routeSelected, value);
                    Stops = routeSelected.Stops;
                    CalculateRouteToDisplay();
                }
            }
        }

        private ObservableCollection<StopModel> stops;
        public ObservableCollection<StopModel> Stops
        {
            get { return stops; }
            set { Set(ref stops, value); }
        }

        private StopModel selectedStop;
        public StopModel SelectedStop
        {
            get { return selectedStop; }
            set { Set(ref selectedStop, value); }
        }

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

            MapLoaded = new RelayCommand<object>((param) =>
            {
                MapControl newMap = param as MapControl;
                Map = newMap;
            });
        }

        private async void CalculateRouteToDisplay()
        {
            // Clear map routes
            Map.Routes.Clear();

            // Get list of all longitude and latitude values from our Stops
            List<BasicGeoposition> allLongitudesAndLatitudes = new List<BasicGeoposition>();
            foreach (var stop in Stops)
            {
                allLongitudesAndLatitudes.Add(new BasicGeoposition()
                {
                    Latitude = decimal.ToDouble(stop.Latitude),
                    Longitude = decimal.ToDouble(stop.Longitude)
                });
            }

            if (allLongitudesAndLatitudes.Count >= 2)
            {
                var route = await mapService.CalculateRoute(allLongitudesAndLatitudes);
                if (route != null)
                {
                    route.RouteColor = Colors.Yellow;
                    route.OutlineColor = Colors.Black;
                    Map.Routes.Add(route);
                }

                SetBounds(route);
            }
        }

        private async void SetBounds(MapRouteView mapRouteView)
        {
            // Fit the MapControl to the route.
            await Map.TrySetViewBoundsAsync(
                  mapRouteView.Route.BoundingBox,
                  new Windows.UI.Xaml.Thickness(20),
                  MapAnimationKind.Bow);
        }
    }
}
