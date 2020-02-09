using System.Collections.ObjectModel;
using System.Linq;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using TrackAChild.Models;

namespace TrackAChild.Services
{
    public class BusService : IBusService
    {
        public ObservableCollection<BusModel> Buses { get; set; }

        private BusModel busToEdit;

        public BusService()
        {
            Buses = new ObservableCollection<BusModel>();
        }

        public void AddBus(string busTag, string vrn, Specs specs, Maintenance maintenance)
        {
            BusModel busModel = new BusModel() { BusTag = busTag, VRN = vrn, Specification = specs, BusMaintenance = maintenance };
            Buses.Add(busModel);
        }

        public void RemoveBus(object bus)
        {
            BusModel busModel = bus as BusModel;
            var busToDelete = Buses.Where(x => x == busModel).FirstOrDefault();

            if (busToDelete != null)
            {
                Buses.Remove(busToDelete);
            }
        }

        //public void EditBus(string busTag, string vrn, Specs specs, Maintenance maintenance)
        //{
        //    //var newDriverDetails = Buses.Where(x => x.DriverName == oldDriverName).FirstOrDefault();
        //    //if (newDriverDetails != null)
        //    //{
        //    //    newDriverDetails.DriverName = driverFirstName + " " + driverLastName;
        //    //}
        //}

        public void EditBus(string oldBusTag, string busTag, string vrn)
        {
            var newBusDetails = Buses.Where(x => x.BusTag == oldBusTag).FirstOrDefault();
            if (newBusDetails != null)
            {
                if (!string.IsNullOrEmpty(busTag))
                {
                    newBusDetails.BusTag = busTag;
                }

                if (!string.IsNullOrEmpty(vrn))
                {
                    newBusDetails.VRN = vrn;
                }
            }
        }

        public void SetBusToEdit(BusModel driverModel)
        {
            busToEdit = driverModel;
        }

        public BusModel GetBusToEdit()
        {
            return busToEdit;
        }
    }
}
