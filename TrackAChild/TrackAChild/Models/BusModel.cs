using TrackAChild.Helpers;

namespace TrackAChild.Models
{
    public class BusModel : Observable
    {
        private Specs specs;
        public Specs Specification
        {
            get { return specs; }
            set { specs = value; OnPropertyChanged(nameof(Specification)); }
        }

        private Maintenance busMaintenance;
        public Maintenance BusMaintenance
        {
            get { return busMaintenance; }
            set { busMaintenance = value; OnPropertyChanged(nameof(BusMaintenance)); }
        }

        public BusModel()
        {
            Specification = new Specs();
            BusMaintenance = new Maintenance();
        }

        private string busTag;
        public string BusTag
        {
            get { return busTag; }
            set { busTag = value; OnPropertyChanged(nameof(BusTag)); }
        }

        private string vrn;
        public string VRN
        {
            get { return vrn; }
            set { vrn = value; OnPropertyChanged(nameof(VRN)); }
        }

        private DriverModel assignedDriver = null;
        public DriverModel AssignedDriver
        {
            get { return assignedDriver; }
            set { assignedDriver = value; OnPropertyChanged(nameof(AssignedDriver)); }
        }
    }
}
