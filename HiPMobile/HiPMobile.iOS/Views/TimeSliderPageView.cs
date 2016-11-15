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
        FloatingTextView floatingText;

        public TimeSliderPageView (IntPtr handle) : base (handle)
        {
        }

        public static TimeSliderPageView Create(TimeSliderPage page)
        {
            var arr = NSBundle.MainBundle.LoadNib("TimeSliderPageView", null, null);
            var view = Runtime.GetNSObject<TimeSliderPageView>(arr.ValueAt(0));
            view.Page = page;
            view.populateViews();
            return view;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            AddFloaingTextView();            
        }

        void AddFloaingTextView()
        {
            // to think how to fix the magic numbers
            CGRect frame = new CGRect(0, Frame.Height * 0.79f, Frame.Width, Frame.Height - Frame.Height * 0.79f);
            floatingText = FloatingTextView.Create();
            floatingText.Frame = frame;            
            AddSubview(floatingText);
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

        void populateViews()
        {
            if(Page != null)
            {
                if (floatingText != null)
                {
                    floatingText.TextView.AttributedText = FormatPageText();
                }

                timeSlider.MinimumValue = 0;
                timeSlider.MaximumValue = Page.Dates.Count - 1;
                timeSlider.Value = 0;
                timeSlider.ValueChanged += TimeSLiderValueChanged;
                timeSlider.Step = 1f;

                SetImage(Page.Images[(int)timeSlider.Value]);
                sliderLabel.Text = Page.Images[(int)timeSlider.Value].Description;
            }
        }

        void TimeSLiderValueChanged(Object sender, EventArgs e)
        {
            SetImage(Page.Images[(int)timeSlider.Value]);
            sliderLabel.Text = Page.Images[(int)timeSlider.Value].Description;
        }
    }
}