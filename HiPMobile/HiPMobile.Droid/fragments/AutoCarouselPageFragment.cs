// Copyright (C) 2016 History in Paderborn App - Universitšt Paderborn
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


using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace de.upb.hip.mobile.droid.fragments {
    public class AutoCarouselPageFragment : Fragment {

        public const string PAGE_NUMBER = "PageNumber";

        // current page number
        public int pageNum;

        public override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Create your fragment here
            pageNum = Arguments.GetInt (PAGE_NUMBER);
        }

        public static AutoCarouselPageFragment Create (int pageNumber)
        {
            var fragment = new AutoCarouselPageFragment ();
            var args = new Bundle ();
            args.PutInt (PAGE_NUMBER, pageNumber);
            fragment.Arguments = args;
            return fragment;
        }


        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            // Inflate the layout containing an image, title desc and subtitile description of the page
            var rootView = (ViewGroup) inflater.Inflate (Resource.Layout.fragment_auto_carousel_page, container, false);

            var root = rootView.RootView;
            var image = rootView.FindViewById<ImageView> (Resource.Id.image_holder);
            var title = rootView.FindViewById<TextView> (Resource.Id.title_text_holder);
            var subtitle = rootView.FindViewById<TextView> (Resource.Id.subtitle_text_holder);

            if (pageNum == 0)
            {
                root.SetBackgroundColor(Resources.GetColor(Resource.Color.bg_color_green_600));
                title.Text = Resources.GetText (Resource.String.slide_1_title);
                subtitle.Text = Resources.GetText (Resource.String.slide_1_desc);
                image.SetImageResource (Resource.Drawable.ac_erkunden);
            }

            if (pageNum == 1)
            {
                root.SetBackgroundColor(Resources.GetColor(Resource.Color.bg_color_orange_600));
                title.Text = Resources.GetText (Resource.String.slide_2_title);
                subtitle.Text = Resources.GetText (Resource.String.slide_2_desc);
                image.SetImageResource(Resource.Drawable.ac_route2);
            }

            if (pageNum == 2)
            {
                root.SetBackgroundColor(Resources.GetColor(Resource.Color.bg_color_blue_600));
                title.Text = Resources.GetText (Resource.String.slide_3_title);
                subtitle.Text = Resources.GetText (Resource.String.slide_3_desc);
                image.SetImageResource(Resource.Drawable.ac_students);
            }

            return rootView;
        }

    }
}