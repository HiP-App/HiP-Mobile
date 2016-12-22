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
    [Register ("FloatingTextView")]
    partial class FloatingTextView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MaterialControls.MDButton expandButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView textView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (expandButton != null) {
                expandButton.Dispose ();
                expandButton = null;
            }

            if (textView != null) {
                textView.Dispose ();
                textView = null;
            }
        }
    }
}