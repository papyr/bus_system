using TrackAChild.Helpers;

namespace TrackAChild.Models
{
    public class PassengerModel : Observable
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { Set(ref firstName, value); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { Set(ref lastName, value); }
        }
    }
}
