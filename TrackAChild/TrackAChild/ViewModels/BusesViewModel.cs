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
using Windows.UI.Xaml;

namespace TrackAChild.ViewModels
{
    public class BusesViewModel : Observable
    {
        IBusService busService;
        public ICommand EditBusCommand { private set; get; }
        public ICommand RemoveBusCommand { private set; get; }
        public ICommand AssignDriverCommand { private set; get; }
        public ICommand NewBusCommand { private set; get; }

        private Visibility isAssignDriverButtonVisible = Visibility.Collapsed;
        public Visibility IsAssignDriverButtonVisible
        {
            get { return isAssignDriverButtonVisible; }
            set { isAssignDriverButtonVisible = value; OnPropertyChanged(nameof(IsAssignDriverButtonVisible)); }
        }

        private BusModel selectedBus;
        public BusModel SelectedBus
        {
            get { return selectedBus; }
            set
            {
                selectedBus = value;
                OnPropertyChanged(nameof(SelectedBus));

                if (SelectedBus != null)
                {
                    IsAssignDriverButtonVisible = Visibility.Visible;
                }
                else
                {
                    IsAssignDriverButtonVisible = Visibility.Collapsed;
                }
            }
        }

        // Retrieve buses from bus service
        public ObservableCollection<BusModel> Buses
        {
            get { return busService.Buses; }
        }

        public BusesViewModel()
        {
            // Retrieve driver service
            busService = (App.Current as App).Container.GetService<IBusService>();

            // FIX: For some reason, I can't bind to the datacontext of the grid to get the BusModel,
            // so I have to bind to the grid and get the datacontext of that through here...
            // Set command for editing buses so new page appears
            EditBusCommand = new RelayCommand<object>((param) =>
            {
                object busModel = (param as Grid).DataContext as object;
                busService.SetBusToEdit(busModel as BusModel);
                NavigationService.Navigate(typeof(BusEditPage));
            });

            // Go to new bus page
            NewBusCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(BusNewPage));
            });

            // Set command for bus driver
            RemoveBusCommand = new RelayCommand<object>(async (param) =>
            {
                object busModel = (param as Grid).DataContext as object;

                var messageDialog = new MessageDialog("Are you sure you want to remove this bus?", "Remove Bus");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    busService.RemoveBus(busModel);
                }
            });

            AssignDriverCommand = new RelayCommand(async () =>
            {
                busService.SetBusToEdit(SelectedBus);
                await new AssignDriverContentDialog().ShowAsync();
            });
        }
    }
}
