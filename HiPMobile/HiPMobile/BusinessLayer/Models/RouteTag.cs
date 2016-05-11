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
     * Model class for the route tag.
     */

    public class RouteTag : BusinessEntityBase
    {

        private string ImageFilename { get; }
        public string Name { get; }

        public string Tag { get; }

        //We need an Image as replacement for drawable
        private byte[] image;


        /// <summary>
        /// Constructor for the RouteTag model
        /// </summary>
        /// <param name="tag">Internal name of the tag.</param>
        /// <param name="name">Displayed name of the tag in the app.</param>
        /// <param name="imageFilename">Name of the image of the tag.</param>
        public RouteTag (string tag, string name, string imageFilename)
        {
            Tag = tag;
            Name = name;
            ImageFilename = imageFilename;
        }


        /**
     * Getter for the tag image. Gets the image from the database on the first call of this method.
     *
     * @param routeId ID of the route.
     * @param context The android application context
     * @return Image Drawable
     */

       /* public Drawable GetImage (int routeId, Context context)
        {
            if (mImage != null)
            {
                return mImage;
            }

            Attachment attachment = DBAdapter.getAttachment (routeId, mImageFilename);

            try
            {
                Bitmap bitmap = BitmapFactory.decodeStream (attachment.getContent ());
                mImage = new BitmapDrawable (context.getResources (), bitmap);
            }
            catch (CouchbaseLiteException e)
            {
                Log.e ("routes", e.toString ());
            }

            return mImage;
        }*/

        /**
     * Getter for the name of the image belonging to the tag.
     * IMPORTANT: Can only be called if getImage(routeId, imageFilename) was called at least once.
     * Else returns null.
     *
     * @return image Drawable
     */

        public byte[] GetImage ()
        {
            return image;
        }




    }
}