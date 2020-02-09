using System.Collections.ObjectModel;
using System.Windows.Input;
using TrackAChild.Interfaces;
using TrackAChild.Models;
using Windows.UI.Popups;
using Microsoft.Extensions.DependencyInjection;
using TrackAChild.Helpers;
using System;
using TrackAChild.Views;
using TrackAChild.Services;
using Windows.UI.Xaml.Controls;

namespace TrackAChild.ViewModels
{
    public class DriversViewModel
    {
        IDriverService driverService;
        public ICommand EditDriverCommand { private set; get; }
        public ICommand RemoveDriverCommand { private set; get; }

        public ICommand NewDriverCommand { private set; get; }

        // Retrieve drivers from driver service
        public ObservableCollection<DriverModel> Drivers
        {
            get { return driverService.Drivers; }
        }

        public DriversViewModel()
        {
            // Retrieve driver service
            driverService = (App.Current as App).Container.GetService<IDriverService>();

            // FIX: For some reason, I can't bind to the datacontext of the grid to get the DriverModel,
            // so I have to bind to the grid and get the datacontext of that through here...
            // Set command for editing drivers so new page appears
            EditDriverCommand = new RelayCommand<object>((param) =>
            {
                object driverModel = (param as Grid).DataContext as object;
                driverService.SetDriverToEdit(driverModel as DriverModel);
                NavigationService.Navigate(typeof(DriverEditPage));
            });

            // Go to new driver page
            NewDriverCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(DriverNewPage));
            });

            // Set command for removing driver
            RemoveDriverCommand = new RelayCommand<object>(async (param) =>
            {
                object driverModel = (param as Grid).DataContext as object;

                var messageDialog = new MessageDialog("Are you sure you want to remove this driver?", "Remove Driver");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    driverService.RemoveDriver(driverModel);
                }
            });
        }
    }
}
