// WARNING
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
    [Register ("ImagePageView")]
    partial class ImagePageView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView exhibitImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (exhibitImage != null) {
                exhibitImage.Dispose ();
                exhibitImage = null;
            }
        }
    }
}