using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TrackAChild.Services;
using TrackAChild.Views;
using Windows.UI.Popups;
using System;
using Windows.UI.Xaml.Controls;
using TrackAChild.Models;
using System.Collections.ObjectModel;

namespace TrackAChild.ViewModels
{
    public class PassengersNewViewModel : Observable
    {
        IPassengerService passengerService;

        public ICommand PublishAddPassengerCommand { private set; get; }

        // This will be used for the passenger service
        private PassengerModel passengerModelChosen = null;

        private StudentModel studentModel = new StudentModel();
        public StudentModel StudentModel
        {
            get { return studentModel; }
            set { studentModel = value; OnPropertyChanged(nameof(StudentModel)); }
        }

        private ChaperoneModel chaperoneModel = new ChaperoneModel();
        public ChaperoneModel ChaperoneModel
        {
            get { return chaperoneModel; }
            set { chaperoneModel = value; OnPropertyChanged(nameof(ChaperoneModel)); }
        }

        private object selectedPivotItem;
        public object SelectedPivotItem
        {
            get { return selectedPivotItem; }
            set { selectedPivotItem = value; OnPropertyChanged(nameof(SelectedPivotItem)); }
        }

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

        public PassengersNewViewModel()
        {
            passengerService = (App.Current as App).Container.GetService<IPassengerService>();

            PublishAddPassengerCommand = new RelayCommand(
            async () =>
            {
                var messageDialog = new MessageDialog("Are you sure you want to add a new passenger?", "Add New Passenger");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    // Let's get what passenger we want to add
                    PivotItem pivotItemSelected = SelectedPivotItem as PivotItem;
                    if (pivotItemSelected.Header.ToString() == "Student")
                    {
                        passengerModelChosen = StudentModel;
                    }
                    else if (pivotItemSelected.Header.ToString() == "Chaperone")
                    {
                        passengerModelChosen = ChaperoneModel;
                    }

                    passengerModelChosen.FirstName = FirstName;
                    passengerModelChosen.LastName = LastName;

                    passengerService.AddPassenger(passengerModelChosen);
                }

                ResetPassengerData();

                NavigationService.Navigate(typeof(PassengersPage));
            });
        }

        private void ResetPassengerData()
        {
            FirstName = "";
            LastName = "";
        }
    }
}
