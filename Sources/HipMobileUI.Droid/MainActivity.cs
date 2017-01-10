﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using de.upb.hip.mobile.droid.Contracts;
using de.upb.hip.mobile.pcl.Common;
using de.upb.hip.mobile.pcl.Common.Contracts;
using HipMobileUI;
using HipMobileUI.Navigation;
using HipMobileUI.Pages;

namespace de.upb.hip.mobile.droid
{
    [Activity(Label = "Historisches Paderborn", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            IoCManager.RegisterType<IImageDimension, AndroidImageDimensions>();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Init Navigation
            NavigationService.Instance.RegisterViewModels(typeof(MainPage).Assembly);
            IoCManager.RegisterInstance(typeof(INavigationService), NavigationService.Instance);
            IoCManager.RegisterInstance(typeof(IViewCreator), NavigationService.Instance);

            Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}
