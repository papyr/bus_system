using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackAChild.Interfaces;

namespace TrackAChild.Services
{
    public class StopService : IStopService
    {
        public ObservableCollection<StopModel> Stops { get; set; }

        public StopService()
        {
            Stops = new ObservableCollection<StopModel>();
        }

        public void AddStop(object stop)
        {
            StopModel stopModel = stop as StopModel;
            Stops.Add(stopModel);
        }

        public void RemoveStop(object stop)
        {
            StopModel stopModel = stop as StopModel;
            Stops.Remove(stopModel);
        }

        public int RetrieveIndexOfStop(object stop)
        {
            StopModel stopModel = stop as StopModel;
            return Stops.IndexOf(stopModel);
        }

        public void AddStopAtIndex(object stop, int index)
        {
            StopModel stopModel = stop as StopModel;
            Stops.Insert(index, stopModel);
        }
    }
}
