namespace TrackAChild.Helpers
{
    public class Maintenance : Observable
    {
        private bool hasMot;
        public bool HasMOT { get { return hasMot; } set { hasMot = value; OnPropertyChanged(nameof(HasMOT)); } }

        private bool hasInsurance;
        public bool HasInsurance { get { return hasInsurance; } set { hasInsurance = value; OnPropertyChanged(nameof(HasInsurance)); } }

        private bool hasFuel = true;
        public bool HasFuel { get { return hasFuel; } set { hasFuel = value; OnPropertyChanged(nameof(HasFuel)); } }

        private bool hasBeenServiced;
        public bool HasBeenServiced { get { return hasBeenServiced; } set { hasBeenServiced = value; OnPropertyChanged(nameof(HasBeenServiced)); } }
    }
}
