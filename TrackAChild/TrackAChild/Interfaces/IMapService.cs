using Itinero;

namespace TrackAChild.Interfaces
{
    internal interface IMapService
    {
        RouterPoint RetrieveRouterPoint(float latitude, float longitude);
        Route CalculateRoute(RouterPoint start, RouterPoint end);
    }
}
