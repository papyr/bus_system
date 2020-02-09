using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAChild.Interfaces
{
    internal interface IStopService
    {
        ObservableCollection<StopModel> Stops { get; set; }

        void AddStop(object stop);
        void RemoveStop(object stop);
        int RetrieveIndexOfStop(object stop);
        void AddStopAtIndex(object stop, int index);
    }
}
