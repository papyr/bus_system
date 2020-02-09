using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Services;
using TrackAChild.Views;
using System.Collections.ObjectModel;

namespace TrackAChild.ViewModels
{
    public class RoutesViewModel
    {
        IRouteService routeService;

        public ICommand NewRouteCommand { private set; get; }

        public ICommand EditRouteCommand { private set; get; }
        public ICommand RemoveRouteCommand { private set; get; }

        // Retrieve routes from route service
        public ObservableCollection<RouteModel> Routes
        {
            get { return routeService.Routes; }
        }

        public RoutesViewModel()
        {
            routeService = (App.Current as App).Container.GetService<IRouteService>();

            // Go to new route page
            NewRouteCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(RouteNewPage));
            });

            EditRouteCommand = new RelayCommand(() =>
            {
                // TODO
            });

            RemoveRouteCommand = new RelayCommand(() =>
            {
                // TODO
            });
        }
    }
}
