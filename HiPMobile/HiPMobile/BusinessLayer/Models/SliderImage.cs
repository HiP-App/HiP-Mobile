using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.upb.hip.mobile.pcl.BusinessLayer.Models
{
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


    /**
     * Model for a single image in the imageslider
     */
    public class SliderImage : BusinessEntityBase
    {

        private string ImageName { get; set; }
        private int Year { get; set; }

        /**
         * Constructor
         * @param year
         * @param imageName
         */
        public SliderImage(int year, string imageName)
        {
            this.Year = year;
            this.ImageName = imageName;
        }

    }

}
