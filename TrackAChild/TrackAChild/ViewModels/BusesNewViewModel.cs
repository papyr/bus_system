using System.Windows.Input;
using TrackAChild.Helpers;
using Microsoft.Extensions.DependencyInjection;
using TrackAChild.Interfaces;
using Windows.UI.Popups;
using TrackAChild.Services;
using TrackAChild.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackAChild.ViewModels
{
    public class BusesNewViewModel : Observable
    {
        IBusService busService;

        public ICommand PublishAddBusCommand { private set; get; }

        private string busTag;
        public string BusTag
        {
            get { return busTag; }
            set { busTag = value; OnPropertyChanged(nameof(BusTag)); }
        }

        private string vrn;
        public string VRN
        {
            get { return vrn; }
            set { vrn = value; OnPropertyChanged(nameof(VRN)); }
        }

        private Specs specs;
        public Specs Specification
        {
            get { return specs; }
            set { specs = value; OnPropertyChanged(nameof(Specification)); }
        }

        private Maintenance busMaintenance;
        public Maintenance BusMaintenance
        {
            get { return busMaintenance; }
            set { busMaintenance = value; OnPropertyChanged(nameof(BusMaintenance)); }
        }

        public BusesNewViewModel()
        {
            // Initialise bus data
            Specification = new Specs();
            BusMaintenance = new Maintenance();

            busService = (App.Current as App).Container.GetService<IBusService>();

            PublishAddBusCommand = new RelayCommand(
            async () =>
            {
                var messageDialog = new MessageDialog("Are you sure you want to add a new bus?", "Add New Bus");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    busService.AddBus(BusTag, VRN, Specification, BusMaintenance);
                }

                ResetBusData();

                NavigationService.Navigate(typeof(BusesPage));
            });
        }

        private void ResetBusData()
        {
            Specification = new Specs();
            BusMaintenance = new Maintenance();

            BusTag = "";
            VRN = "";
        }
    }
}
