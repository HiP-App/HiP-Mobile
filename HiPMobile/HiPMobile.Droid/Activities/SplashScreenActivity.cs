// Copyright (C) 2016 History in Paderborn App - Universit�t Paderborn
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
using Android.Widget;
using de.upb.hip.mobile.droid.Helpers;
using de.upb.hip.mobile.pcl.Common;
using de.upb.hip.mobile.pcl.DataAccessLayer;
using de.upb.hip.mobile.pcl.DataLayer;
using HockeyApp;
using HockeyApp.Objects;
using Java.Lang;
using Microsoft.Practices.Unity;
using Realms;

namespace de.upb.hip.mobile.droid.Activities {
    [Activity (Theme = "@style/AppTheme", MainLauncher = true)]
    public class SplashScreenActivity : Activity {

        private const int StartupDelay = 0;
        private Action action;
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
                IoCManager.UnityContainer.RegisterType<IDataAccess, RealmDataAccess>();

                // Delete current database to avoid migration issues, remove this when wanting persistent database usage
                Realm.DeleteRealm(new RealmConfiguration());

                // Insert Data
                var filler = new DbDummyDataFiller(Assets);
                filler.InsertData();

                action = StartMainActivity;

                RunOnUiThread (() => {
                    var handler = new Handler();
                    handler.PostDelayed(action, StartupDelay);
                });  
            });

            // make HockeyApp feedback available
            FeedbackManager.Register (
                ApplicationContext, 
                "9947e2434fe64d318214cfc6972d4800", 
                new HipFeedbackListener ()
            );

        }


        private void StartMainActivity ()
        {
            StartActivity (typeof (MainActivity));
            Finish ();
        }

    }

    public class HipFeedbackListener : FeedbackManagerListener {

        public override bool FeedbackAnswered (FeedbackMessage p0)
        {
            // intentionally left blank
            return true;
        }

        public override Class FeedbackActivityClass => Class.FromType (typeof(HipFeedbackActivity));

    }
}