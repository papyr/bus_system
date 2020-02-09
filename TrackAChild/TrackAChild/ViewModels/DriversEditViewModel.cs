using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using TrackAChild.Models;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Popups;
using System;
using TrackAChild.Services;
using TrackAChild.Views;

namespace TrackAChild.ViewModels
{
    public class DriversEditViewModel : Observable
    {
        IDriverService driverService;

        public ICommand PublishEditDriverCommand { private set; get; }

        private DriverModel driverModelToEdit;
        public string DriverName { get; }

        private string driverFirstName;
        public string DriverFirstName
        {
            get { return driverFirstName; }
            set { Set(ref driverFirstName, value); }
        }

        private string driverLastName;
        public string DriverLastName
        {
            get { return driverLastName; }
            set { Set(ref driverLastName, value); }
        }

        private string driverNumber;
        public string DriverNumber
        {
            get { return driverNumber; }
            set { Set(ref driverNumber, value); }
        }

        public DriversEditViewModel()
        {
            driverService = (App.Current as App).Container.GetService<IDriverService>();

            // Cast object to DriverModel and assign
            driverModelToEdit = driverService.GetDriverToEdit() as DriverModel;

            // Set driver name for label
            DriverName = driverModelToEdit.DriverName;

            PublishEditDriverCommand = new RelayCommand(
            async () =>
            {
                var messageDialog = new MessageDialog("Are you sure you want to commit these changes?", "Accept Driver Changes");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    driverService.EditDriver(DriverName, DriverFirstName, DriverLastName, DriverNumber);
                }

                DriverFirstName = "";
                DriverLastName = "";

                NavigationService.Navigate(typeof(DriversPage));
            });
        }
    }
}
