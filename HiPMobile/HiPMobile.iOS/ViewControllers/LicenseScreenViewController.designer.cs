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
    [Register ("LicenseScreenViewController")]
    partial class LicenseScreenViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView LicenseTextView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LicenseTextView != null) {
                LicenseTextView.Dispose ();
                LicenseTextView = null;
            }
        }
    }
}