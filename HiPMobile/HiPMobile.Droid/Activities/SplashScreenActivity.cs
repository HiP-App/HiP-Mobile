// Copyright (C) 2016 History in Paderborn App - Universitšt Paderborn
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Widget;
using de.upb.hip.mobile.droid.Contracts;
using de.upb.hip.mobile.droid.Helpers;
using de.upb.hip.mobile.pcl.Common;
using de.upb.hip.mobile.pcl.Common.Contracts;
using de.upb.hip.mobile.pcl.DataAccessLayer;
using de.upb.hip.mobile.pcl.DataLayer;
using Microsoft.Practices.Unity;
using Realms;

namespace de.upb.hip.mobile.droid.Activities
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class SplashScreenActivity : Activity
    {

        private const int StartupDelay = 0;
        private Action action;
        private TextView textAction;
        private TextView textWaiting;
        private string DatabaseVersionKey = "DBVersion";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_splash_screen);

            textAction = (TextView)FindViewById(Resource.Id.splashScreenActionText);
            textWaiting = (TextView)FindViewById(Resource.Id.splashScreenWaitingText);

            textAction.SetText(Resource.String.splash_screen_loading);
            textWaiting.SetText(Resource.String.splash_screen_waiting);

            ThreadPool.QueueUserWorkItem(state =>
            {
                // setup IoCManager
                IoCManager.UnityContainer.RegisterType<IDataAccess, RealmDataAccess>();
                IoCManager.UnityContainer.RegisterType<IImageDimension, AndroidImageDimension> ();
                IoCManager.UnityContainer.RegisterInstance (typeof(IDataLoader), new AndroidDataLoader (Assets));

                if (!IsDatabaseUpToDate ())
                {
                    // Delete current database to avoid migration issues
                    Realm.DeleteRealm (new RealmConfiguration ());

                    // Insert Data
                    var filler = new DbDummyDataFiller ();
                    filler.InsertData ();

                    // Update preferences indicating which database version is present
                    ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences (Application);
                    var edit = pref.Edit ();
                    edit.PutInt (DatabaseVersionKey, AndroidConstants.DatabaseVersion);
                    edit.Apply ();
                }

                action = StartMainActivity;

                RunOnUiThread(() =>
                {
                    var handler = new Handler();
                    handler.PostDelayed(action, StartupDelay);
                });
            });


        }


        private void StartMainActivity()
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
            }
            else if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private bool IsDatabaseUpToDate ()
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(Application);
            var storedVersion = pref.GetInt (DatabaseVersionKey, -1);
            return storedVersion == AndroidConstants.DatabaseVersion;
        }
    }
}