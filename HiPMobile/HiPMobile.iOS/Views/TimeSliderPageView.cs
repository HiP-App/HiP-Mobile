using CoreGraphics;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using Foundation;
using ObjCRuntime;
using System;
using UIKit;

namespace HiPMobile.iOS
{
    public partial class TimeSliderPageView : UIView
    {
        TimeSliderPage Page { get; set; }
        FloatingTextView textView;

        public TimeSliderPageView (IntPtr handle) : base (handle)
        {
        }

        public static TimeSliderPageView Create()
        {
            var arr = NSBundle.MainBundle.LoadNib("TimeSliderPageView", null, null);
            var view = Runtime.GetNSObject<TimeSliderPageView>(arr.ValueAt(0));
            return view;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            // to think how to fix the magic numbers
            CGRect frame = new CGRect(0, Frame.Height * 0.8f, Frame.Width, Frame.Height - Frame.Height * 0.8f);
            textView = FloatingTextView.Create();
            textView.Frame = frame;
            AddSubview(textView);
        }
    }
}