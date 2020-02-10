using System.Collections.ObjectModel;
using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using TrackAChild.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TrackAChild.ViewModels
{
    public class AssignBusViewModel : Observable
    {
        IRouteService routeService;
        IBusService busService;

        public ICommand AssignBusCommand { private set; get; }

        // Retrieve buses from bus service
        public ObservableCollection<BusModel> Buses
        {
            get { return busService.Buses; }
        }

        // Selected Bus
        private BusModel selectedBus;
        public BusModel SelectedBus
        {
            get { return selectedBus; }
            set
            {
                Set(ref selectedBus, value);
                if (SelectedBus != null) { IsAssignButtonEnabled = true; } else { IsAssignButtonEnabled = false; }
            }
        }

        private bool isAssignButtonEnabled = false;
        public bool IsAssignButtonEnabled
        {
            get { return isAssignButtonEnabled; }
            set { isAssignButtonEnabled = value; OnPropertyChanged(nameof(IsAssignButtonEnabled)); }
        }

        public AssignBusViewModel()
        {
            // Retrieve bus and route service
            routeService = (App.Current as App).Container.GetService<IRouteService>();
            busService = (App.Current as App).Container.GetService<IBusService>();

            AssignBusCommand = new RelayCommand(() =>
            {
                routeService.AssignBusToRoute(SelectedBus);
            });
        }
    }
}
