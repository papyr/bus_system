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
    public class PassengerStudentEditViewModel : Observable
    {
        public ICommand PublishEditStudentPassengerCommand { get; set; }

        IPassengerService passengerService;

        private StudentModel studentModelToEdit;

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

        private string className;
        public string ClassName { get { return className; } set { Set(ref className, value); } }

        private string parentName;
        public string ParentName { get { return parentName; } set { Set(ref parentName, value); } }

        private string parentEmail;
        public string ParentEmail { get { return parentEmail; } set { Set(ref parentEmail, value); } }

        private AgeGroupCapabilities ageGroup;
        public AgeGroupCapabilities AgeGroup { get { return ageGroup; } set { Set(ref ageGroup, value); } }

        public PassengerStudentEditViewModel()
        {
            AgeGroup = new AgeGroupCapabilities();

            passengerService = (App.Current as App).Container.GetService<IPassengerService>();

            // Cast object to StudentModel and assign
            studentModelToEdit = passengerService.GetPassengerToEdit() as StudentModel;

            // Set passenger name for label
            PassengerNameToDisplay = studentModelToEdit.FirstName + " " + studentModelToEdit.LastName;

            PublishEditStudentPassengerCommand = new RelayCommand(
            async () =>
            {
                var messageDialog = new MessageDialog("Are you sure you want to commit these changes?", "Accept Student Changes");
                messageDialog.Commands.Add(new UICommand("Yes", null));
                messageDialog.Commands.Add(new UICommand("No", null));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                var cmd = await messageDialog.ShowAsync();

                if (cmd.Label == "Yes")
                {
                    studentModelToEdit.FirstName = FirstName;
                    studentModelToEdit.LastName = LastName;
                    studentModelToEdit.ClassName = ClassName;
                    studentModelToEdit.ParentName = ParentName;
                    studentModelToEdit.ParentEmail = ParentEmail;
                    studentModelToEdit.AgeGroup = AgeGroup;
                }

                NavigationService.Navigate(typeof(PassengersPage));
            });
        }
    }
}
