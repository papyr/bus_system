using TrackAChild.Interfaces;
using Windows.Storage;

namespace TrackAChild.Services
{
    public class OSPathDependencies : IOSPathDependencies
    {
        public string GetStoragePath()
        {
            return ApplicationData.Current.LocalFolder.Path.ToString();
        }
    }
}
