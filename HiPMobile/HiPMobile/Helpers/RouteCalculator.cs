using System.Collections.Generic;
using System.IO;
using System.Reflection;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using Itinero;
using Itinero.Data.Contracted;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using OsmSharp;
using OsmSharp.Streams;
using Route = Itinero.Route;

namespace de.upb.hip.mobile.pcl.Helpers {
    public sealed class RouteCalculator {

        private static RouterDb routingDB;
        private static Router routeRouter;
        private static IList<GeoLocation> route;

        private static RouteCalculator instance;
        private static readonly object padlock = new object ();

        //Initializes the database for the routing from pbf
        private RouteCalculator ()
        {
            routingDB = new RouterDb ();

            var assembly = typeof(RouteCalculator).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("de.upb.hip.mobile.pcl.Content.osmfile.routerdb"))
            {
                routingDB = RouterDb.Deserialize(stream);
            }

            //Initialize Router after loading Deserializing is important, otherwise Profiles are not loaded properly
            routeRouter = new Router(routingDB);

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
            IList<Route> routes = new List<Route> ();
            //starting distance with very high placeholder value
            double distance = 1000000;
            //is the location to compute next shortest waypoint
            GeoLocation currentLocation = userPosition;
            Route temp = null;

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