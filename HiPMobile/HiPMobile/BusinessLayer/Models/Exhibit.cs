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
     * Exhibit objects store general information to exhibit points
     * they have an id which should be unique, although in theory the same id can be assigned to
     * more than one exhibit
     */

    public class Exhibit : BusinessEntityBase {

        private Dictionary<string, string> pictureDescriptions;


        public Exhibit (int id, string name, string description, double lat,
                        double lng, string categories, string tags)
        {
            Id = id;
            Name = name;
            Description = description;
            Coordinates = new GeoCoordinate(lat, lng);
            Categories = categories.Split (',');
            Tags = tags.Split (',');
        }

        public Exhibit() { }

        //changes member names of the class

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Categories { get; set; }
        public string[] Tags { get; set; }
        public int SliderId { get; set; }
        public GeoCoordinate Coordinates { get; set; }


        /// <summary>
        ///     calculates the distance from position to the exhibit location.
        /// </summary>
        public double GetDistance (GeoCoordinate g)
        {

            double d = 0;
            return d;
            //TODO calculate distance here
        }


        public Dictionary<string, string> GetPictureDescriptions ()
        {
            return new Dictionary<string, string> (); //return a new object
            // so as to leave this one intact, when the return object is changed
        }



    }
}