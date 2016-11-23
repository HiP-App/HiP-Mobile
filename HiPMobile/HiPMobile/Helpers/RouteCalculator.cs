using System.Collections.Generic;
using System.Diagnostics;
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
using System;

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

            var assembly = typeof (RouteCalculator).GetTypeInfo ().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream ("de.upb.hip.mobile.pcl.Content.osmfile.routerdb"))
            {
                routingDB = RouterDb.Deserialize (stream);
            }

            //Initialize Router after loading Deserializing is important, otherwise Profiles are not loaded properly
            routeRouter = new Router (routingDB);

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
        public IList<GeoLocation> CreateRouteWithSeveralWaypoints (GeoLocation userPosition, IList<Waypoint> listOfWayPoints)
        {
            //Contains all subroutes of the path
            IList<Itinero.Route> routes = new List<Route> ();
            //starting distance with very high placeholder value
            float distance = 1000000;
            //is the location to compute next shortest waypoint
            GeoLocation currentLocation = userPosition;
            //Buffer route
            Itinero.Route temp = null;
            //holds all waypoints which are already vistited
            IList<int> listofIndices = new List<int> ();
            //index of shortest next waypoint
            int index = 0;
            //List of Geolocations for path
            IList<GeoLocation> result = new List<GeoLocation> ();

            for (int i = 0; i < listOfWayPoints.Count;)
            {
                if (listofIndices.Contains (i) == false)
                {
                    Stopwatch stopwatch = new Stopwatch ();

                    // Begin timing
                    stopwatch.Start ();


                    var r = routeRouter.Calculate (Vehicle.Pedestrian.Fastest (),
                                                   (float) currentLocation.Latitude, (float) currentLocation.Longitude, (float) listOfWayPoints [i].Location.Latitude,
                                                   (float) listOfWayPoints [i].Location.Longitude);
                    stopwatch.Stop ();

                    Debug.WriteLine("Time elapsed: {0}", stopwatch.ElapsedMilliseconds);

                    if (r.TotalDistance < distance)
                    {
                        index = i;
                        temp = r;
                        distance = r.TotalDistance;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
                else
                    i++;


                if (i == listOfWayPoints.Count)
                {
                    routes.Add (temp);
                    listofIndices.Add (index);
                    i = 0;
                    currentLocation = listOfWayPoints [index].Location;
                    distance = 100000;
                }

                if (listofIndices.Count == listOfWayPoints.Count)
                {
                    foreach (Route rt in routes)
                    {
                        foreach (var coor in rt.Shape)
                        {
                            result.Add (new GeoLocation (coor.Latitude, coor.Longitude));
                        }
                    }
                    break;
                }
            }

            return result;
        }

    }
}