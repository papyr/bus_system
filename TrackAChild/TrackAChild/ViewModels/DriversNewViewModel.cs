using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Popups;
using System;
using TrackAChild.Services;
using TrackAChild.Views;

namespace TrackAChild.ViewModels
{
    public class DriversNewViewModel : Observable
    {
        IDriverService driverService;

        public ICommand PublishAddDriverCommand { private set; get; }

        private string driverFirstName;
        public string DriverFirstName
        {
            get { return driverFirstName; }
            set { driverFirstName = value; OnPropertyChanged(nameof(DriverFirstName)); }
        }

        private string driverLastName;
        public string DriverLastName
        {
            get { return driverLastName; }
            set { driverLastName = value; OnPropertyChanged(nameof(DriverLastName)); }
        }

        private string driverNumber;
        public string DriverNumber
        {
            get { return driverNumber; }
            set { driverNumber = value; OnPropertyChanged(nameof(DriverNumber)); }
        }

        public DriversNewViewModel()
        {
            driverService = (App.Current as App).Container.GetService<IDriverService>();

            PublishAddDriverCommand = new RelayCommand(
            async () =>
            {
                var messageDialog = new MessageDialog("Are you sure you want to add a new driver?", "Add New Driver");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    driverService.AddDriver(DriverFirstName, DriverLastName, DriverNumber);
                }

                DriverFirstName = "";
                DriverLastName = "";

                NavigationService.Navigate(typeof(DriversPage));
            });
        }
    }
}
