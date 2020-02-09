using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using TrackAChild.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Popups;
using TrackAChild.Services;
using TrackAChild.Views;

namespace TrackAChild.ViewModels
{
    public class PassengerChaperoneEditViewModel : Observable
    {
        public ICommand PublishEditChaperonePassengerCommand { get; set; }

        IPassengerService passengerService;

        private ChaperoneModel chaperoneModelToEdit;

        public string PassengerNameToDisplay { get; }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        private string emergencyContactNumber;
        public string EmergencyContactNumber { get { return emergencyContactNumber; } set { Set(ref emergencyContactNumber, value); } }

        public PassengerChaperoneEditViewModel()
        {
            passengerService = (App.Current as App).Container.GetService<IPassengerService>();

            // Cast object to ChaperoneModel and assign
            chaperoneModelToEdit = passengerService.GetPassengerToEdit() as ChaperoneModel;

            // Set passenger name for label
            PassengerNameToDisplay = chaperoneModelToEdit.FirstName + " " + chaperoneModelToEdit.LastName;

            PublishEditChaperonePassengerCommand = new RelayCommand(
            async () =>
            {
                var messageDialog = new MessageDialog("Are you sure you want to commit these changes?", "Accept Chaperone Changes");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    chaperoneModelToEdit.FirstName = FirstName;
                    chaperoneModelToEdit.LastName = LastName;
                    chaperoneModelToEdit.EmergencyContactNumber = EmergencyContactNumber;
                }

                NavigationService.Navigate(typeof(PassengersPage));
            });
        }
    }
}
