using System.Collections.ObjectModel;
using TrackAChild.Models;

namespace TrackAChild.Interfaces
{
    internal interface IPassengerService
    {
        ObservableCollection<PassengerModel> Passengers { get; set; }
        void AddPassenger(PassengerModel passenger);
        void RemovePassenger(object passenger);
        void SetPassengerToEdit(PassengerModel passenger);
        PassengerModel GetPassengerToEdit();
    }
}
