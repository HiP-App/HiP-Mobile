// Copyright (C) 2016 History in Paderborn App - Universit�t Paderborn
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


using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Text.Method;
using Android.Views;
using Android.Widget;

namespace de.upb.hip.mobile.droid.Activities {
    [Activity (Theme = "@style/AppTheme", Label = "LicensingActivity", MainLauncher = false, Icon = "@drawable/icon")]
    public class LegalNoticeActivity : AppCompatActivity {

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.activity_legal_notice);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            MakeLinksClickable ();

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = Resources.GetString(Resource.String.license_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //TODO: uncomment this when BaseActivity is ported

        }


        /// <summary>
        ///     Make URL links from the licensing information clickable, so that they open in brower when the user link on them
        /// </summary>
        private void MakeLinksClickable ()
        {
            ((TextView) FindViewById (Resource.Id.licensingGoogleMaterialBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView) FindViewById (Resource.Id.licensingOSMDroidBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView) FindViewById (Resource.Id.licensingRealmBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView) FindViewById (Resource.Id.licensingMapiconsBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingUnityBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingSettingspluginBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingHockeyappBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingXamarinSupportBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingFodyBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingCommonServiceBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.contributionForFlaticon)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.impressum)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingOsmSharpBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingItineroBody)).MovementMethod = LinkMovementMethod.Instance;
            ((TextView)FindViewById(Resource.Id.licensingReminiBody)).MovementMethod = LinkMovementMethod.Instance;





        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                //TODO: Comment this in again when RouteFilterActivity is ported
                case Android.Resource.Id.Home:
                    SupportFinishAfterTransition();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}