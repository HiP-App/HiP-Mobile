﻿// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HiPMobile.iOS
{
    [Register ("ExhibitDetailsViewController")]
    partial class ExhibitDetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView exhibitDetailsScrollView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton subtitleButton { get; set; }

        [Action ("SubtitlesButtonPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SubtitlesButtonPressed (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (exhibitDetailsScrollView != null) {
                exhibitDetailsScrollView.Dispose ();
                exhibitDetailsScrollView = null;
            }

            if (subtitleButton != null) {
                subtitleButton.Dispose ();
                subtitleButton = null;
            }
        }
    }
}