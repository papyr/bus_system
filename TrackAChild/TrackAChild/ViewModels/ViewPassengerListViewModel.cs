using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TrackAChild.Models;
using System.Collections.Generic;
using System.Windows.Input;
using TrackAChild.Helpers;
using Windows.UI.Xaml.Controls;

namespace TrackAChild.ViewModels
{
    public class ViewPassengerListViewModel
    {
        IRouteService routeService;

        public ICommand CloseDialog { get; set; }

        // Retrieve passengers of route
        public List<PassengerModel> Passengers
        {
            get { return routeService.GetRouteToEdit().AssignedPassengers; }
        }

        public ViewPassengerListViewModel()
        {
            // Retrieve route service
            routeService = (App.Current as App).Container.GetService<IRouteService>();

            CloseDialog = new RelayCommand<object>((param) =>
            {
                ContentDialog cd = param as ContentDialog;
                cd.Hide();
            });
        }
    }
}
