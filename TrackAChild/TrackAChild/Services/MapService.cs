using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace TrackAChild.Services
{
    public class MapService : IMapService
    {
        private readonly Router router;
        private readonly Itinero.Profiles.Profile profile;
        public MapService()
        {
            var routerDb = new RouterDb();

            var s = (App.Current as App).Container.GetService<IOSPathDependencies>().GetStoragePath();
            var fullpath = "test.pbf";

            using (var stream = new FileInfo(fullpath).OpenRead())
            {
                // create the network for cars only.
                routerDb.LoadOsmData(stream, Vehicle.Car);
            }

            // create a router.
            router = new Router(routerDb);

            // get a profile.
            profile = Vehicle.Car.Fastest(); // the default OSM car profile.
        }

        public RouterPoint RetrieveRouterPoint(float latitude, float longitude)
        {
            return router.Resolve(profile, latitude, longitude);
        }

        public Route CalculateRoute(RouterPoint start, RouterPoint end)
        {
            return router.Calculate(profile, start, end);
        }
    }
}
