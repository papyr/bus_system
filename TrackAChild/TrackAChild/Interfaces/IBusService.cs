using System.Collections.ObjectModel;
using TrackAChild.Helpers;
using TrackAChild.Models;

namespace TrackAChild.Interfaces
{
    internal interface IBusService
    {
        ObservableCollection<BusModel> Buses { get; set; }
        void AddBus(string busTag, string vrn, Specs specs, Maintenance maintenance);
        void RemoveBus(object bus);
        //void EditBus(string busTag, string vrn, Specs specs, Maintenance maintenance); // need to properly implement
        void EditBus(string oldBusTag, string busTag, string vrn);
        void SetBusToEdit(BusModel busModel);
        void AssignDriverToBus(DriverModel driverModel);
        BusModel GetBusToEdit();
    }
}
