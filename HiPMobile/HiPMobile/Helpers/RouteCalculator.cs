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
using Route = Itinero.Route;

namespace de.upb.hip.mobile.pcl.Helpers {
    public sealed class RouteCalculator {

        private static RouterDb routingDB;
        private static Router routeRouter;
        private static IList<GeoLocation> route;

        private static RouteCalculator instance = null;
        private static readonly object padlock = new object ();

        //Initializes the database for the routing from pbf
        public RouteCalculator ()
        {
            routingDB = new RouterDb ();
            routeRouter = new Router (routingDB);

            //var assembly = Assembly.Load (new AssemblyName ("HiPMobilePCL"));
            var assembly = typeof(pcl.Helpers.RouteCalculator).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream ("de.upb.hip.mobile.pcl.Content.Route_Network_Paderborn_01.pbf");


            routingDB.LoadOsmData (stream, Vehicle.Pedestrian);
            route = new List<GeoLocation> ();
        }

        public static RouteCalculator Instance {
            get {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RouteCalculator ();
                    }
                    return instance;
                }
            }
        }

        //Simple route from start to endpoint
        public void CreateSimpleRoute (GeoLocation start, GeoLocation end)
        {
            // calculate a route.
            var r = routeRouter.Calculate (Vehicle.Pedestrian.Fastest (),
                                           (float) start.Latitude, (float) start.Longitude, (float) end.Latitude, (float) end.Longitude);
        }

        //This method calculates one route from several sub routes
        public void CreateRouteWithSeveralWaypoints (GeoLocation userPosition, IList<Waypoint> listOfWayPoints)
        {
            //Contains all subroutes of the path
            IList<Itinero.Route> routes = new List<Route> ();
            //starting distance with very high placeholder value
            double distance = 1000000;
            //is the location to compute next shortest waypoint
            GeoLocation currentLocation = userPosition;
            Itinero.Route temp = null;

            foreach (Waypoint w in listOfWayPoints)
            {
                var r = routeRouter.Calculate (Vehicle.Pedestrian.Fastest (),
                                               (float) currentLocation.Latitude, (float) currentLocation.Longitude, (float) w.Location.Latitude, (float) w.Location.Longitude);

                if (r.TotalDistance < distance)
                {
                    if (routes.Count > 0)
                    {
                        routes.Remove (temp);
                        temp = r;
                        routes.Add (r);
                        currentLocation = w.Location;
                        distance = r.TotalDistance;
                    }
                    else
                    {
                        temp = r;
                        routes.Add (r);
                        currentLocation = w.Location;
                        distance = r.TotalDistance;
                    }
                }
            }

            //here we create route from several subroutes
            //First calculate the shortest route from user position to first waypoint
            //then calculate shortest route from that waypoint to next waypoint and so on
            //save all waypoints in one route an return it
        }

    }
}