using TrackAChild.Helpers;

namespace TrackAChild.Models
{
    public class UserModel : Observable
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { Set(ref username, value); }
        }
    }
}
