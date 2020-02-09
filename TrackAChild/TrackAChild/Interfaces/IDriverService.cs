using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrackAChild.Models;

namespace TrackAChild.Interfaces
{
    internal interface IDriverService
    {
        ObservableCollection<DriverModel> Drivers { get; set; }
        void AddDriver(string driverFirstName, string driverLastName, string driverNumber);
        void RemoveDriver(object driver);
        void EditDriver(string oldDriverName, string driverFirstName, string driverLastName, string driverNumber);
        void SetDriverToEdit(DriverModel driverModel);
        DriverModel GetDriverToEdit();
    }
}
