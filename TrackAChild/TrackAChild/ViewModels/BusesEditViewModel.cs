using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using TrackAChild.Models;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Popups;
using TrackAChild.Services;
using TrackAChild.Views;
using System;

namespace TrackAChild.ViewModels
{
    public class BusesEditViewModel : Observable
    {
        IBusService busService;

        public ICommand PublishEditBusCommand { private set; get; }

        private BusModel busModelToEdit;
        public string BusTag { get; }

        private string newBusTag;
        public string NewBusTag
        {
            get { return newBusTag; }
            set { Set(ref newBusTag, value); }
        }

        private string newVRN;
        public string NewVRN
        {
            get { return newVRN; }
            set { Set(ref newVRN, value); }
        }

        public BusesEditViewModel()
        {
            busService = (App.Current as App).Container.GetService<IBusService>();

            // Cast object to BusModel and assign
            busModelToEdit = busService.GetBusToEdit() as BusModel;

            // Set bus tag name for label
            BusTag = busModelToEdit.BusTag;

            PublishEditBusCommand = new RelayCommand(
            async () =>
            {
                var messageDialog = new MessageDialog("Are you sure you want to commit these changes?", "Accept Bus Changes");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    busService.EditBus(BusTag, NewBusTag, NewVRN);
                }

                NewBusTag = "";
                NewVRN = "";

                NavigationService.Navigate(typeof(BusesPage));
            });
        }
    }
}
