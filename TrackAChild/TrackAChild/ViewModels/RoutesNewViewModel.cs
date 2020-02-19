using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Views;
using System;
using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI;

namespace TrackAChild.ViewModels
{
    public class RoutesNewViewModel : Observable
    {
        IStopService stopService;
        IMapService mapService;

        private bool isSwitchCommandRunning = false;

        public MapControl Map { get; private set; }

        private StopModel selectedStop;
        public StopModel SelectedStop
        {
            get { return selectedStop; }
            set { Set(ref selectedStop, value); }
        }

        public ObservableCollection<StopModel> Stops
        {
            get { return stopService.Stops; }
        }

        private readonly BasicGeoposition defaultPosition = new BasicGeoposition()
        {
            Latitude = 51.443570f,
            Longitude = -0.344270f
        };

        public ObservableCollection<MapLayer> MapLayers
        { get; } = new ObservableCollection<MapLayer>();

        private List<MapElement> mapElements = new List<MapElement>();

        private double zoomLevel = 17;
        public double ZoomLevel
        {
            get { return zoomLevel; }
            set { Set(ref zoomLevel, value); }
        }

        private Geopoint center;
        public Geopoint Center
        {
            get { return center; }
            set { Set(ref center, value); }
        }

        public ICommand MapLoaded { get; set; }

        public ICommand NewStopCommand { get; set; }

        public ICommand RemoveStopCommand { get; set; }

        public ICommand SwitchStopUpCommand { get; set; }

        public ICommand SwitchStopDownCommand { get; set; }

        public ICommand SaveRouteCommand { get; set; }

        public RoutesNewViewModel()
        {
            stopService = (App.Current as App).Container.GetService<IStopService>();
            mapService = (App.Current as App).Container.GetService<IMapService>();

            Stops.CollectionChanged += Stops_CollectionChanged;

            Center = new Geopoint(defaultPosition);

            MapLoaded = new RelayCommand<object>((param) =>
            {
                MapControl newMap = param as MapControl;
                Map = newMap;
            });

            NewStopCommand = new RelayCommand(async () => { await new SearchAddressDialog().ShowAsync(); });
            RemoveStopCommand = new RelayCommand<object>((param) =>
            {
                // will need to find out what's wrong with this binding at some point
                object stopModel = (param as Grid).DataContext as object;
                stopService.RemoveStop(stopModel);
            });

            SaveRouteCommand = new RelayCommand(async () => { await new NewRouteConfirmation().ShowAsync(); });

            SwitchStopUpCommand = new RelayCommand(() =>
            {
                // We are running a switch command
                isSwitchCommandRunning = true;

                // Find index of selected item in stops
                var indexOfStop = stopService.RetrieveIndexOfStop(SelectedStop as object);

                // if index is 0, do nothing
                if (indexOfStop == 0)
                {
                    return;
                }

                // Temp model object
                var tempStopToReinsert = SelectedStop.Clone();

                // Remove item from collection and reinsert at desired index (-1)
                stopService.RemoveStop(SelectedStop);

                // SelectedStop becomes null at this point, so let's use the
                // copy we made to reinsert
                stopService.AddStopAtIndex(tempStopToReinsert, indexOfStop - 1);

                // We've reached the end of the switch command
                isSwitchCommandRunning = false;
            });

            SwitchStopDownCommand = new RelayCommand(() =>
            {
                // We are running a switch command
                isSwitchCommandRunning = true;

                // Find index of selected item in stops
                var indexOfStop = stopService.RetrieveIndexOfStop(SelectedStop as object);

                // if index is the last index, do nothing
                if (indexOfStop == stopService.Stops.Count - 1)
                {
                    return;
                }

                // Temp model object
                var tempStopToReinsert = SelectedStop.Clone();

                // Remove item from collection and reinsert at desired index (+1)
                stopService.RemoveStop(SelectedStop);

                // SelectedStop becomes null at this point, so let's use the
                // copy we made to reinsert
                stopService.AddStopAtIndex(tempStopToReinsert, indexOfStop + 1);

                // We've reached the end of the switch command
                isSwitchCommandRunning = false;
            });
        }

        private void Stops_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // If we are running a switch command, don't do anything
            if (isSwitchCommandRunning)
            {
                // Perform re-routing mechanism here since we swapped
                // the indexes of our stops
                PerformReroutingOfPoints();
                return;
            }

            // If we have no more stops, clear our map layers and reset our
            // center to our default position
            if (Stops.Count == 0)
            {
                // Remove any pushpins
                MapLayers.Clear();

                // Set Center to default position
                Center = new Geopoint(defaultPosition);
                return;
            }

            // For old items, remove the map elements, for new items, add them
            if (e.OldItems != null)
            {
                foreach (var old in e.OldItems)
                {
                    StopModel stop = old as StopModel;

                    // Remove our removed stop from the map elements of its corresponding
                    // pushpin
                    if (mapElements.Any(x => ((MapIcon)x).Title == stop.Name))
                    {
                        mapElements.Remove(mapElements.Where(x => ((MapIcon)x).Title == stop.Name).First());
                    }
                }
            }

            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    StopModel stop = newItem as StopModel;
                    var stopGeopoint = new Geopoint(
                        new BasicGeoposition()
                        {
                            Latitude = decimal.ToDouble(stop.Latitude),
                            Longitude = decimal.ToDouble(stop.Longitude)
                        });

                    var poi = new MapIcon
                    {
                        Location = stopGeopoint,
                        NormalizedAnchorPoint = new Point(0.5, 1.0),
                        Title = stop.Name,
                        ZIndex = 0
                    };

                    // Add newly created pushpin to our map elements
                    mapElements.Add(poi);
                }
            }

            // Recreate the layer
            var layer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = mapElements
            };

            MapLayers.Clear();
            MapLayers.Add(layer);

            // Perform re-routing mechanism
            PerformReroutingOfPoints();
        }

        /// <summary>
        /// This function performs re-routing of points
        /// </summary>
        private void PerformReroutingOfPoints()
        {
            // If we have less than 2 stops, we return here as we
            // have nothing to do - oh and to clear the routes of course
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

            // Call function to re-draw the route
            if (allLongitudesAndLatitudes.Count > 1)
            {
                ReDrawRoute(allLongitudesAndLatitudes);
            }
            else
            {
                // Try to compute the bounding box
                var boundingBox = GeoboundingBox.TryCompute(allLongitudesAndLatitudes);

                // Call async function to display all pushpins
                RetrieveBoundingBox(boundingBox);
            }
        }

        /// <summary>
        /// Function to try and set the view of the map with the bounds we've just
        /// passed in
        /// </summary>
        /// <param name="boundingBox"></param>
        private async void RetrieveBoundingBox(GeoboundingBox boundingBox)
        {
            await Map.TrySetViewBoundsAsync(boundingBox, new Windows.UI.Xaml.Thickness(30), MapAnimationKind.Bow);
        }

        /// <summary>
        /// This function takes in a list of geopositions, and tells the map
        /// service to create a route using this list. Existing routes on the
        /// map are cleared.
        /// </summary>
        /// <param name="stopLocations"></param>
        private async void ReDrawRoute(List<BasicGeoposition> stopLocations)
        {
            // Clear routes from Map
            Map.Routes.Clear();

            var route = await mapService.CalculateRoute(stopLocations);
            if (route != null)
            {
                route.RouteColor = Colors.Yellow;
                route.OutlineColor = Colors.Black;
                Map.Routes.Add(route);
            }

            // Set the view of the map to encompass all of the route
            RetrieveBoundingBox(route.Route.BoundingBox);
        }
    }
}
