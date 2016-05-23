/*
* Copyright (C) 2016 History in Paderborn App - Universität Paderborn
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System.Collections.Generic;

namespace de.upb.hip.mobile.pcl.BusinessLayer.Models {
    /**
     * Model for set of routes
     */

    public class RouteSet : BusinessEntityBase 
    {
        /// <summary>
        /// Stores all different Routes
        /// </summary>
        public List<Route> Routes { get; set; }



        public RouteSet ()
        {

            Routes = new List<Route> ();

            // Inside the constructor different routes were created
            // Data for these routes comes from database.
            // I would suggest to create only a List which stores
            // all different routes. And then we add the routes in
            // the logic part (loading) part
        }


        public Route GetRouteByPosition (int position)
        {
            return Routes[position];

        }

        public Route GetRouteById (int id)
        {
            foreach (var route in Routes)
            {
                if (route.Id == id)
                {
                    return route;
                }
            }
            return null;
        }


        public void AddRoute (Route r)
        {
            Routes.Add (r);
        }

        public void RemoveRoute (Route r)
        {
            Routes.Remove (r);
        }
    }
}