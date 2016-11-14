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

        public static TimeSliderPageView Create(TimeSliderPage page)
        {
            var arr = NSBundle.MainBundle.LoadNib("TimeSliderPageView", null, null);
            var view = Runtime.GetNSObject<TimeSliderPageView>(arr.ValueAt(0));
            view.Page = page;
            return view;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            AddFloaingTextView();
            //textView.AttributedText = FormatPageText();            
        }

        void AddFloaingTextView()
        {
            // to think how to fix the magic numbers
            CGRect frame = new CGRect(0, Frame.Height * 0.79f, Frame.Width, Frame.Height - Frame.Height * 0.79f);
            textView = FloatingTextView.Create();
            textView.Frame = frame;
            AddSubview(textView);
        }

        NSAttributedString FormatPageText()
        {
            var titleAttributes = new UIStringAttributes
            {
                Font = UIFont.BoldSystemFontOfSize(13)
            };

            String title = Page.Title;
            NSMutableAttributedString attributedString = new NSMutableAttributedString(title + "\n\n" + Page.Text);
            attributedString.SetAttributes(titleAttributes, new NSRange(0, title.Length));
            return attributedString;
        }

        void SetImage(Image rawImage)
        {
            NSData imageData = NSData.FromArray(rawImage.Data);
            imageView.Image = new UIImage(imageData);
        }
    }
}