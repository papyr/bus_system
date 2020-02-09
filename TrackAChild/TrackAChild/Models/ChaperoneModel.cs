namespace TrackAChild.Models
{
    public class ChaperoneModel : PassengerModel
    {
        private string emergencyContactNumber;
        public string EmergencyContactNumber
        {
            get { return emergencyContactNumber; }
            set { Set(ref emergencyContactNumber, value); }
        }
    }
}
