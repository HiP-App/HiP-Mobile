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
using System.Linq;
using Itinero.LocalGeo;

namespace de.upb.hip.mobile.pcl.Helpers {
    public sealed class RouteCalculator {

        private static Router routeRouter;
        private static IList<GeoLocation> route;

        private static RouteCalculator instance;
        private static readonly object Padlock = new object ();


        /// <summary>
        /// Initializes the database for the routing from pbf
        /// </summary>
        private RouteCalculator ()
        {
           RouterDb routingDb = new RouterDb ();

            var assembly = typeof (RouteCalculator).GetTypeInfo ().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream ("de.upb.hip.mobile.pcl.Content.osmfile.routerdb"))
            {
                routingDb = RouterDb.Deserialize (stream);
            }

            //Initialize Router after loading Deserializing is important, otherwise Profiles are not loaded properly
            routeRouter = new Router (routingDb);

            route = new List<GeoLocation> ();
        }

        public static RouteCalculator Instance {
            get {
                lock (Padlock)
                {
                    if (instance == null)
                    {
                        instance = new RouteCalculator ();
                    }
                    return instance;
                }
            }
        }


        /// <summary>
        /// Simple route from start to endpoint
        /// </summary>
        /// <param name="start">start position</param>
        /// <param name="end">end position</param>
        /// <returns>IList GeoLocation</returns>
        public IList<GeoLocation> CreateSimpleRoute (GeoLocation start, GeoLocation end)
        {
            IList<GeoLocation> result = new List<GeoLocation> ();
            // calculate a route.
            var r = routeRouter.Calculate (Vehicle.Pedestrian.Fastest (),
                                           (float) start.Latitude, (float) start.Longitude, (float) end.Latitude, (float) end.Longitude);

            foreach (Coordinate c in r.Shape)
            {
                result.Add (new GeoLocation (c.Latitude, c.Longitude));
            }

            return result;
        }


        /// <summary>
        /// This method calculates one route from several waypoints
        /// </summary>
        /// <param name="userPosition">position of user</param>
        /// <param name="listOfWayPoints">list of all waypoints</param>
        /// <returns>IList GeoLocation</returns>
        public IList<GeoLocation> CreateRouteWithSeveralWaypoints (GeoLocation userPosition, IList<Waypoint> listOfWayPoints)
        {
            //List of Geolocations for path
            IList<GeoLocation> result = new List<GeoLocation> ();
            IList<Coordinate> locations = new List<Coordinate> ();


            locations.Add (new Coordinate ((float) userPosition.Latitude, (float) userPosition.Longitude));

            foreach (var v in listOfWayPoints)
            {
                locations.Add (new Coordinate ((float) v.Location.Latitude, (float) v.Location.Longitude));
            }


            var route = routeRouter.Calculate (Vehicle.Pedestrian.Fastest (), locations.ToArray ());

            foreach (Coordinate c in route.Shape)
            {
                result.Add (new GeoLocation (c.Latitude, c.Longitude));
            }


            return result;
        }

    }
}