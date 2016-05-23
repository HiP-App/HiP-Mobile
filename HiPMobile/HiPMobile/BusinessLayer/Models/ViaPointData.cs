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

namespace de.upb.hip.mobile.pcl.BusinessLayer.Models {
    /**
     * Holds data for a specific point the route should pass through
     * Note that this may but does not have to be connected to an exhibit
     */

    public class ViaPointData : BusinessEntityBase {

        /**
         * Default constructor initializes class with dummy data
         */

        public ViaPointData ()
        {
            GeoCoordinate = new GeoCoordinate (51.7276064, 8.7684325);
            Title = "";
            Description = "";
            ExhibitId = -1;
        }

        public ViaPointData (GeoCoordinate geo, string title, string description, int exhibitId)
        {
            SetViaPointData (geo, title, description, exhibitId);
        }

        //private GeoPoint mGeo = new GeoPoint(51.7276064, 8.7684325); // Paderborn
        public GeoCoordinate GeoCoordinate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ExhibitId { get; set; }

        public void SetViaPointData (GeoCoordinate geo, string title, string description, int exhibitId)
        {
            GeoCoordinate = geo;
            Title = title;
            Description = description;
            ExhibitId = exhibitId;
        }

    }
}