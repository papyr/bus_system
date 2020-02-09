namespace TrackAChild.Helpers
{
    public class AgeGroupCapabilities : Observable
    {
        private bool isNurseryCapable;
        public bool IsNurseryCapable { get { return isNurseryCapable; } set { isNurseryCapable = value; OnPropertyChanged(nameof(IsNurseryCapable)); } }

        private bool isPrePreparatory;
        public bool IsPrePreparatory { get { return isPrePreparatory; } set { isPrePreparatory = value; OnPropertyChanged(nameof(IsPrePreparatory)); } }

        private bool isPreparatory;
        public bool IsPreparatory { get { return isPreparatory; } set { isPreparatory = value; OnPropertyChanged(nameof(IsPreparatory)); } }
    }
}
