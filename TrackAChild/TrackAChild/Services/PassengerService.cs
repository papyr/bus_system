using System.Collections.ObjectModel;
using TrackAChild.Interfaces;
using TrackAChild.Models;

namespace TrackAChild.Services
{
    public class PassengerService : IPassengerService
    {
        private PassengerModel passengerToEdit;

        public ObservableCollection<PassengerModel> Passengers { get; set; }

        public PassengerService()
        {
            Passengers = new ObservableCollection<PassengerModel>();
        }

        public void AddPassenger(PassengerModel passenger)
        {
            Passengers.Add(passenger);
        }

        public void RemovePassenger(object passenger)
        {
            PassengerModel passengerModel = passenger as PassengerModel;
            Passengers.Remove(passengerModel);
        }

        public void SetPassengerToEdit(PassengerModel passenger)
        {
            passengerToEdit = passenger;
        }

        public PassengerModel GetPassengerToEdit()
        {
            return passengerToEdit;
        }
    }
}
