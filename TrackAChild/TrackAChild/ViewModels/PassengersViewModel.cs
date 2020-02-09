using System.Collections.ObjectModel;
using System.Windows.Input;
using TrackAChild.Interfaces;
using TrackAChild.Models;
using Microsoft.Extensions.DependencyInjection;
using TrackAChild.Helpers;
using Windows.UI.Xaml.Controls;
using TrackAChild.Services;
using Windows.UI.Popups;
using System;
using TrackAChild.Views;

namespace TrackAChild.ViewModels
{
    public class PassengersViewModel
    {
        IPassengerService passengerService;

        public ICommand EditPassengerCommand { private set; get; }
        public ICommand RemovePassengerCommand { private set; get; }

        public ICommand NewPassengerCommand { private set; get; }

        // Retrieve passengers from passenger service
        public ObservableCollection<PassengerModel> Passengers
        {
            get { return passengerService.Passengers; }
        }

        public PassengersViewModel()
        {
            // Retrieve passenger service
            passengerService = (App.Current as App).Container.GetService<IPassengerService>();

            // FIX: For some reason, I can't bind to the datacontext of the grid to get the BusModel,
            // so I have to bind to the grid and get the datacontext of that through here...
            // Set command for editing passengers so new page appears
            EditPassengerCommand = new RelayCommand<object>((param) =>
            {
                object passengerModel = (param as Grid).DataContext as object;
                passengerService.SetPassengerToEdit(passengerModel as PassengerModel);

                if (passengerModel.GetType() == typeof(StudentModel))
                {
                    NavigationService.Navigate(typeof(PassengerStudentEditPage));
                }
                else
                {
                    NavigationService.Navigate(typeof(PassengerChaperoneEditPage));
                }
            });

            // Go to new passenger page
            NewPassengerCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(PassengerNewPage));
            });

            // Remove passenger from list of passengers
            RemovePassengerCommand = new RelayCommand<object>(async (param) =>
            {
                object passengerModel = (param as Grid).DataContext as object;

                var messageDialog = new MessageDialog("Are you sure you want to remove this passenger?", "Remove Passenger");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    passengerService.RemovePassenger(passengerModel);
                }
            });
        }
    }
}
