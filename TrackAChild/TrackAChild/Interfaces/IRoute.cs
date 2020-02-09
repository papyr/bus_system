using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TrackAChild.Interfaces
{
    public interface IRoute
    {
        ObservableCollection<StopModel> GetStops();
    }
}
