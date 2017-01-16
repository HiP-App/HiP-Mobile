// Copyright (C) 2016 History in Paderborn App - Universität Paderborn
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//       http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using de.upb.hip.mobile.droid.fragments.bottomsheetfragment;
using de.upb.hip.mobile.droid.Helpers;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;

//using Android.view.ViewTreeListener.onGlobalLayoutListener;

namespace de.upb.hip.mobile.droid.fragments.exhibitpagefragment {
    public class AppetizerExhibitPageFragment : ExhibitPageFragment {

        /// <summary>
        ///     Height of the Bottom Sheet in dp.
        /// </summary>
        private readonly int BOTTOM_SHEET_HEIGHT = 265;
        /// <summary>
        ///     Stores the model instance for this page.
        /// </summary>
        private AppetizerPage page;

        /// <summary>
        ///     Title for the appetizer bottom sheet
        /// </summary>
        public string AppetizerTitle { set; get; } = "Default Appetizer Title";


        private SimpleBottomSheetFragment bsFragment;

        public override BottomSheetConfig GetBottomSheetConfig ()
        {
             bsFragment = new SimpleBottomSheetFragment ();
            bsFragment.Title = AppetizerTitle;
            if (page != null)
                bsFragment.Description = page.Text;

            var bottomSheetConfig = new BottomSheetConfig
            {

                MaxHeight = BOTTOM_SHEET_HEIGHT,
                PeekHeight = BOTTOM_SHEET_HEIGHT,
                fabAction = BottomSheetConfig.FabAction.Next,
                BottomSheetFragment = bsFragment
            };
            return bottomSheetConfig;
        }

        public override void SetPage (Page page)
        {
            this.page = page.AppetizerPage;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            var v = inflater.Inflate (Resource.Layout.fragment_exhibitpage_appetizer, container, false);

            // set image
            var imgView = (ImageView) v.FindViewById (Resource.Id.imgAppetizer);
            if (imgView != null && page != null)
            { 
                // adjust height of ImageView by subtracting height of bottom sheet & ActionBar
                var screenHeightInPx = Resources.DisplayMetrics.HeightPixels;
                var bsHeightInPx = PixelDpConversion.ConvertDpToPixel (BOTTOM_SHEET_HEIGHT);
                var actionBar = ((AppCompatActivity) Activity).SupportActionBar;
                var appbarHeightInPx = actionBar?.Height ?? 0;
                var correction = -10;
                imgView.LayoutParameters.Height = (int) (screenHeightInPx - bsHeightInPx - appbarHeightInPx - correction);
                
                // sets actual image resource
                var img = page.Image;
                var drawable = img.GetDrawable (Context, imgView.Width, imgView.Height);
                imgView.SetImageDrawable (drawable); 
            }

            return v;
        }

    }
}