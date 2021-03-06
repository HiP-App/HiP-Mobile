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
using Android.Content.PM;
using Android.Content.Res;
using Android.Locations;
using Android.OS;
using Android.Widget;
using de.upb.hip.mobile.droid.Contracts;
using de.upb.hip.mobile.droid.Listeners;
using de.upb.hip.mobile.pcl.BusinessLayer.Managers;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using de.upb.hip.mobile.pcl.BusinessLayer.Routing;
using de.upb.hip.mobile.pcl.Common;
using de.upb.hip.mobile.pcl.Common.Contracts;
using de.upb.hip.mobile.pcl.DataAccessLayer;
using de.upb.hip.mobile.pcl.DataLayer;
using Microsoft.Practices.Unity;
using Orientation = Android.Content.Res.Orientation;

namespace de.upb.hip.mobile.droid.Activities {
    [Activity (Theme = "@style/AppTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class SplashScreenActivity : Activity {

        private const int StartupDelay = 0;
        private Action action;
        private string DatabaseVersionKey = "DBVersion";
        private TextView textAction;
        private TextView textWaiting;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.activity_splash_screen);

            textAction = (TextView) FindViewById (Resource.Id.splashScreenActionText);
            textWaiting = (TextView) FindViewById (Resource.Id.splashScreenWaitingText);

            textAction.SetText (Resource.String.splash_screen_loading);
            textWaiting.SetText (Resource.String.splash_screen_waiting);

            ThreadPool.QueueUserWorkItem (state => {
                                              // setup IoCManager
                                              IoCManager.UnityContainer.RegisterType<IDataAccess, RealmDataAccess> ();
                                              IoCManager.UnityContainer.RegisterType<IDataLoader, EmbeddedResourceDataLoader> ();
                                              IoCManager.UnityContainer.RegisterType<IImageDimension, AndroidImageDimension> ();
                                              //IoCManager.UnityContainer.RegisterInstance (typeof(IDataLoader), new AndroidDataLoader (Assets));
                                              var calculator = RouteCalculator.Instance;
                                              // setup KeyManager
                                              KeyManager.Instance.RegisterProvider (new AndroidKeyProvider ());

                                              DbManager.UpdateDatabase ();

                                              action = StartMainActivity;

                                              //setup the ExtendedLocationListener by calling it once
                                              var extendedLocationListener = ExtendedLocationListener.GetInstance ();
                                              extendedLocationListener.SetContext (this);
                                              extendedLocationListener.Initialize (GetSystemService (LocationService) as LocationManager);
                                              extendedLocationListener.Unregister (); //the listener should just be created here, but not used

                                              RunOnUiThread (() => {
                                                                 var handler = new Handler ();
                                                                 handler.PostDelayed (action, StartupDelay);
                                                             });
                                          });
        }


        private void StartMainActivity ()
        {
            StartActivity (typeof (MainActivity));
            Finish ();
        }

        public override void OnConfigurationChanged (Configuration newConfig)
        {
            base.OnConfigurationChanged (newConfig);

            if (newConfig.Orientation == Orientation.Portrait)
            {
            }
            else
                if (newConfig.Orientation == Orientation.Landscape)
                {
                }
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy ();
        }

    }
}