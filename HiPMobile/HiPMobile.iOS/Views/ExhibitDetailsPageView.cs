using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using CoreGraphics;
using Foundation;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;

namespace HiPMobile.iOS
{
    public class ExhibitDetailsPageView : UIView
    {
        public FloatingTextView floatingText;
        public Page Page { get; set; }

        public ExhibitDetailsPageView(IntPtr handle) : base(handle)
        {
        }

        public void AddFloaingTextView()
        {
            // to think how to fix the magic numbers
            CGRect frame = Frame;
            frame.Y = UIScreen.MainScreen.Bounds.Height - 100;
            floatingText = FloatingTextView.Create();
            floatingText.Frame = frame;
            AddSubview(floatingText);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            AddFloaingTextView();
        }
    }
}
