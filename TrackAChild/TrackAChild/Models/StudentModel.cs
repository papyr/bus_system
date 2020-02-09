using TrackAChild.Helpers;

namespace TrackAChild.Models
{
    public class StudentModel : PassengerModel
    {
        private string className;
        public string ClassName { get { return className; } set { Set(ref className, value); } }

        private string parentName;
        public string ParentName { get { return parentName; } set { Set(ref parentName, value); } }

        private string parentEmail;
        public string ParentEmail { get { return parentEmail; } set { Set(ref parentEmail, value); } }

        private AgeGroupCapabilities ageGroup;
        public AgeGroupCapabilities AgeGroup { get { return ageGroup; } set { Set(ref ageGroup, value); } }

        public StudentModel()
        {
            AgeGroup = new AgeGroupCapabilities();
        }
    }
}
