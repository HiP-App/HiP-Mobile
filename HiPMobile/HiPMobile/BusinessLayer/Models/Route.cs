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
     * Model Class the route.
     */

    public class Route : BusinessEntityBase {

        /// <summary>
        ///     Constructor for route.
        /// </summary>
        /// <param name="id">The id of the route.</param>
        /// <param name="title">The title of the route.</param>
        /// <param name="description">The description of the route.</param>
        /// <param name="wayPoints">The way points of the route.</param>
        /// <param name="duration">The duration of the route in seconds</param>
        /// <param name="distance">The distance of the route in kilometer.</param>
        /// <param name="tags">The tags of the route.</param>
        /// <param name="imageName">The name of the image, that belongs to the route.</param>
        public Route (int id, string title, string description, List<Waypoint> wayPoints,
                      int duration, double distance, List<RouteTag> tags, string imageName)
        {
            Id = id;
            Title = title;
            Description = description;
            WayPoints = wayPoints;
            Duration = duration;
            Distance = distance;
            Tags = tags;
            ImageName = imageName;
        }

        public string Description { get; set; }
        public double Distance { get; set; } //in km
        public int Duration { get; set; } //in seconds

        public int Id { get; set; }
        public string ImageName { get; set; }
        public List<RouteTag> Tags { get; set; }
        public string Title { get; set; }
        public List<Waypoint> WayPoints { get; set; }

    }
}