namespace TrackAChild.Helpers
{
    public class Specs : Observable
    {
        private int capacity;
        public int Capacity { get { return capacity; } set { capacity = value; OnPropertyChanged(nameof(Capacity)); } }

        private AgeGroupCapabilities capabilities;
        public AgeGroupCapabilities Capabilities { get { return capabilities; } set { capabilities = value; OnPropertyChanged(nameof(Capabilities)); } }

        public Specs()
        {
            Capabilities = new AgeGroupCapabilities();
        }
    }
}
