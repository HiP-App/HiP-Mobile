using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using Itinero;
using Itinero.IO.Osm;
using Itinero.LocalGeo;
using Itinero.Osm.Vehicles;

namespace de.upb.hip.mobile.pcl.Helpers
{
   public class RouteCalculator {

        private RouterDb routingDB;
        private Router routeRouter;
        private IList<GeoLocation> route;

        public RouteCalculator ()
        {
            routingDB = new RouterDb ();
            routeRouter = new Router (routingDB);

            var assembly = Assembly.Load (new AssemblyName ("HiPMobilePCL"));
            Stream stream = assembly.GetManifestResourceStream("de.upb.hip.mobile.pcl.content.Route_Network_Paderborn.pbf");


            routingDB.LoadOsmData(stream, Vehicle.Pedestrian);
            route = new List<GeoLocation> ();
        }

        public void CreateSimpleRoute(GeoLocation start, GeoLocation end)
        {
            // calculate a route.
            var r = routeRouter.Calculate(Vehicle.Pedestrian.Fastest(),
                (float)start.Latitude, (float)start.Longitude, (float)end.Latitude,(float) end.Longitude);
            //var geoJson = route.ToGeoJson();

            foreach (Coordinate coord in r.Shape)
            {
                route.Add(new GeoLocation((double)coord.Latitude, (double)coord.Longitude));
            }
        }
    }
}
