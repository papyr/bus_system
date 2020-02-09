using System;
using System.Collections.ObjectModel;
using System.Linq;
using TrackAChild.Interfaces;
using TrackAChild.Models;

namespace TrackAChild.Services
{
    public class DriverService : IDriverService
    {
        public ObservableCollection<DriverModel> Drivers { get; set; }

        private DriverModel driverToEdit;

        public DriverService()
        {
            Drivers = new ObservableCollection<DriverModel>()
            {
                new DriverModel { DriverName = "Bob Smith", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Toots Mush", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Test Hello", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Test Bye", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Test Cat", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Test Dog", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Test Rabbit", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Test Fox", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Test Fish", DriverNumber = "+447877787778" },
                new DriverModel { DriverName = "Test Chicken", DriverNumber = "+447877787778" }
            };
        }

        public void AddDriver(string driverFirstName, string driverLastName, string driverNumber)
        {
            DriverModel driverModel = new DriverModel() { DriverName = driverFirstName + " " + driverLastName,
                DriverNumber = driverNumber };
            Drivers.Add(driverModel);
        }

        public void RemoveDriver(object driver)
        {
            DriverModel driverModel = driver as DriverModel;
            var driverToDelete = Drivers.Where(x => x == driverModel).FirstOrDefault();

            if (driverToDelete != null)
            {
                Drivers.Remove(driverToDelete);
            }
        }

        public void EditDriver(string oldDriverName, string driverFirstName, string driverLastName, string driverNumber)
        {
            var newDriverDetails = Drivers.Where(x => x.DriverName == oldDriverName).FirstOrDefault();
            if (newDriverDetails != null)
            {
                newDriverDetails.DriverName = driverFirstName + " " + driverLastName;
                newDriverDetails.DriverNumber = driverNumber;
            }
        }

        public void SetDriverToEdit(DriverModel driverModel)
        {
            driverToEdit = driverModel;
        }

        public DriverModel GetDriverToEdit()
        {
            return driverToEdit;
        }
    }
}
