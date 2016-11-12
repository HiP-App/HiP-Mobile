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


using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Widget;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace de.upb.hip.mobile.droid.Dialogs {
    public class HelpDialogFragment : DialogFragment, IDialogInterfaceOnDismissListener {

        public enum HelpWindows {

            AutoAudio,
            AutoSwitch

        }


        public  static HelpDialogFragment Fragment;
        private static AlertDialog.Builder alert;
        private string message;

        private ISharedPreferences sharedPreferences;
        private ISharedPreferencesEditor sharedPreferencesEditor;

        private string title;

        public static Action OnCloseDialogAction { get; set; }

        public static HelpWindows Type { get; set; }


        public override void OnDismiss (IDialogInterface dialog)
        {
            OnCloseDialogAction ();
            //Activity.FragmentManager.BeginTransaction().Remove(this).Commit();
        }


        public static HelpDialogFragment NewHelpDialogFragment (HelpWindows helpWindow, Action onCloseDialogAction)
        {
            Fragment = new HelpDialogFragment ();
            Type = helpWindow;
            OnCloseDialogAction = onCloseDialogAction;
            return Fragment;
        }


        public override Dialog OnCreateDialog (Bundle savedInstanceState)
        {
            alert = new AlertDialog.Builder (Activity, Resource.Style.HelpDialogTheme);

            title = Resources.GetString (Resource.String.hint_message);
            alert.SetTitle (title);

            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences (Activity);
            sharedPreferencesEditor = sharedPreferences.Edit ();

            switch (Type)
            {
                case HelpWindows.AutoAudio:
                    message = Resources.GetString (Resource.String.auto_audio_message);

                    alert.SetPositiveButton (Resources.GetString (Resource.String.keep_feature_on), (senderAlert, args) => {
                        sharedPreferencesEditor.PutBoolean ("pref_auto_start_audio_key_onboarding", false);
                        sharedPreferencesEditor.PutBoolean ("pref_auto_start_audio_key", true);
                        sharedPreferencesEditor.Commit ();
                        Toast.MakeText (Activity, Resources.GetString (Resource.String.choice_keep_audio), ToastLength.Short).Show ();
                    });

                    alert.SetNegativeButton (Resources.GetString (Resource.String.disregard_feature), (senderAlert, args) => {
                        sharedPreferencesEditor.PutBoolean ("pref_auto_start_audio_key_onboarding", false);
                        sharedPreferencesEditor.PutBoolean ("pref_auto_start_audio_key", false);
                        sharedPreferencesEditor.Commit ();
                        Toast.MakeText (Activity, Resources.GetString (Resource.String.choice_disregard_audio), ToastLength.Short).Show ();
                    });
                    break;

                case HelpWindows.AutoSwitch:
                    message = Resources.GetString (Resource.String.auto_switch_message);

                    alert.SetPositiveButton (Resources.GetString (Resource.String.keep_feature_on), (senderAlert, args) => {
                        sharedPreferencesEditor.PutBoolean ("pref_auto_switch_page_key_onboarding", false);
                        sharedPreferencesEditor.PutBoolean ("pref_auto_page_switch_key", true);
                        sharedPreferencesEditor.Commit ();
                        Toast.MakeText (Activity, Resources.GetString (Resource.String.choice_keep_switch_pages), ToastLength.Short).Show ();
                    });

                    alert.SetNegativeButton (Resources.GetString (Resource.String.disregard_feature), (senderAlert, args) => {
                        sharedPreferencesEditor.PutBoolean ("pref_auto_switch_page_key_onboarding", false);
                        sharedPreferencesEditor.PutBoolean ("pref_auto_page_switch_key", false);
                        sharedPreferencesEditor.Commit ();
                        Toast.MakeText (Activity, Resources.GetString (Resource.String.choice_disregard_switch_pages), ToastLength.Short).Show ();
                    });
                    break;
                default:
                    break;
            }

            alert.SetMessage (message);
            return alert.Create ();
        }

    }
}