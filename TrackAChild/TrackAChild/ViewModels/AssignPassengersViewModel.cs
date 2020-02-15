using System.Collections.ObjectModel;
using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using TrackAChild.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Linq;

namespace TrackAChild.ViewModels
{
    public class AssignPassengersViewModel : Observable
    {
        IRouteService routeService;
        IPassengerService passengerService;

        public ICommand AssignPassengersCommand { private set; get; }

        // Retrieve passengers from passenger service
        public ObservableCollection<PassengerModel> Passengers
        {
            get { return passengerService.Passengers; }
        }

        private ObservableCollection<PassengerModel> filteredPassengers;
        public ObservableCollection<PassengerModel> FilteredPassengers
        {
            get { return filteredPassengers; }
            set { filteredPassengers = value; OnPropertyChanged(nameof(FilteredPassengers)); }
        }

        private string typeSelected;
        public string TypeSelected
        {
            get { return typeSelected; }
            set
            {
                if (typeSelected != value)
                {
                    typeSelected = value;
                    OnPropertyChanged(nameof(TypeSelected));
                    FilterPassengersToDisplay();
                }
            }
        }

        // A list to store the current selected passengers
        private List<PassengerModel> selectedPassengers = new List<PassengerModel>();

        // Selected passenger just to set the assign button to enabled or not
        private PassengerModel selectedPassenger;
        public PassengerModel SelectedPassenger
        {
            get { return selectedPassenger; }
            set
            {
                Set(ref selectedPassenger, value);
                if (SelectedPassenger != null) { IsAssignButtonEnabled = true; } else { IsAssignButtonEnabled = false; }
            }
        }

        private bool isAssignButtonEnabled = false;
        public bool IsAssignButtonEnabled
        {
            get { return isAssignButtonEnabled; }
            set { isAssignButtonEnabled = value; OnPropertyChanged(nameof(IsAssignButtonEnabled)); }
        }

        public AssignPassengersViewModel()
        {
            // Retrieve passenger and route service
            routeService = (App.Current as App).Container.GetService<IRouteService>();
            passengerService = (App.Current as App).Container.GetService<IPassengerService>();

            // Set type
            TypeSelected = "All";

            AssignPassengersCommand = new RelayCommand<object>((param) =>
            {
                routeService.AssignPassengersToRoute(selectedPassengers);
            });
        }

        // Not the nicest MVVM but hey hum... selected items is a read-only property
        // on list view... :\
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (PassengerModel selectedItem in e.AddedItems)
            {
                selectedPassengers.Add(selectedItem);
            }
            foreach (PassengerModel unSelectedItem in e.RemovedItems)
            {
                selectedPassengers.Remove(unSelectedItem);
            }
        }

        private void FilterPassengersToDisplay()
        {
            if (TypeSelected == "Student")
            {
                FilteredPassengers = new ObservableCollection<PassengerModel>(
                    Passengers.Where(x => x.GetType() == typeof(StudentModel)).ToList());
            }
            else if (TypeSelected == "Chaperone")
            {
                FilteredPassengers = new ObservableCollection<PassengerModel>(
                    Passengers.Where(x => x.GetType() == typeof(ChaperoneModel)).ToList());
            }
            else
            {
                FilteredPassengers = new ObservableCollection<PassengerModel>(Passengers);
            }
        }
    }
}
