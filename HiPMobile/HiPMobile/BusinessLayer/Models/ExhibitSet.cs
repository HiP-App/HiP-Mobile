
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.upb.hip.mobile.pcl.BusinessLayer.Models {

    /**
     * Model for an Set of Exhibits
     */

    public class ExhibitSet : BusinessEntityBase {

        internal readonly List<Exhibit> InitSet = new List<Exhibit> ();
        private List<Exhibit> activeSet = new List<Exhibit> ();
        private readonly List<string> categories = new List<string> ();
        private GeoCoordinate position; //TODO put user position in logic part for ios and android and winphone

        /**
         * Constructor for an set of exhibits
         *
         * @param listOfExhibits     listOfExhibits of exhibits as Map<String, Object>
         * @param position a LatLng Object with current position
         */

        public ExhibitSet (List<Exhibit> listOfExhibits, GeoCoordinate position)
        {
            this.position = position;


            foreach (Exhibit e in listOfExhibits)
            {
                InitSet.Add(e);
                foreach (var  c in e.Categories)
                {
                    if (categories.Contains(c) == false)
                        categories.Add (c);
                }
            }

            foreach (var item in InitSet)
                activeSet.Add (item);

            this.OrderByDistance ();
        }


        /**
         * update the categories
         *
         * @param strArray array with categories as string
         */

        public void updateCategories (List<string> strArray)
        {
            activeSet = new List<Exhibit> ();

            for (var i = 0; i < strArray.Count; i++)
            {
                foreach (var exhibit in InitSet)
                {
                    if (exhibit.Categories.Contains (strArray [i]))
                    {
                        activeSet.Add (exhibit);
                    }
                }

                this.OrderByDistance ();
            }
        }

        /**
         * update the current position
         *
         * @param position current position as LatLng
         */
            public void updatePosition (GeoCoordinate position)
            {
                this.position = position;

                this.OrderByDistance ();
            }


            /**
         * orders the exhibits by distance to the current position
         */
        private void OrderByDistance ()
            {
                List<Exhibit> tmpList = new List<Exhibit> ();

                double minDistance = 0;
                var minPosition = 0;
                double currentDistance;
                var i = 0;

                while (activeSet.Count > 0)
                {
                    currentDistance = activeSet[i].GetDistance (position);
                    if (minDistance == 0)
                    {
                        minDistance = currentDistance;
                        minPosition = i;
                    }
                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        minPosition = i;
                    }
                    if (i == activeSet.Count - 1)
                    {
                        tmpList.Add (activeSet[minPosition]);
                        activeSet.RemoveAt (minPosition);
                        minDistance = 0;
                        i = 0;
                    }
                    else
                        i++;
                }

                activeSet = tmpList;
            }


        //TODO create markers of teh map in the logic area
            /**
         * adds a marker to the map
         *
         * @param mMarker the marker
         * @param ctx     actual context
         */
      /*  public void addMarker (SetMarker mMarker, Context ctx)
            {
                mMarker.mFolderOverlay.closeAllInfoWindows ();
                mMarker.mFolderOverlay.getItems ().clear ();

                foreach (Exhibit exhibit in mActiveSet)
                {
                    Drawable d = DBAdapter.getImage (exhibit.GetId (), "image.jpg", 32);
                    byte[] 
                    Dictionary<string, int> data = new Dictionary<string, int> ();
                    data.Add (exhibit.GetName (), exhibit.GetId ());

                    Drawable icon = ContextCompat.getDrawable (ctx, R.drawable.marker_via);

                    mMarker.addMarker (null, exhibit.GetName (), exhibit.GetDescription (),
                                       new GeoPoint (exhibit.GetLatlng ().latitude, exhibit.GetLatlng ().longitude), d, icon, data);
                }
            }*/

            /**
         * getter for an exhibit
         *
         * @param position position of the exhibit in the ExhibitSet
         * @return Exhibit
         */
        public Exhibit getExhibit (int position)
        {
            return activeSet.ElementAt (position);
        }

            /**
         * gets the size of the exhibitSet
         *
         * @return size
         */
        public int getAmountOfExhibits ()
            {
                return activeSet.Count;
            }
        }

    }