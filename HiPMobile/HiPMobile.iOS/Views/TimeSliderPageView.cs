using CoreGraphics;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using Foundation;
using ObjCRuntime;
using System;
using UIKit;

namespace HiPMobile.iOS
{
    public partial class TimeSliderPageView : ExhibitDetailsPageView
    {
        TimeSliderPage TimeSliderPage {
            get{
                return Page.TimeSliderPage;
            }
        }

        public TimeSliderPageView (IntPtr handle) : base (handle)
        {
        }

        public static TimeSliderPageView Create(Page page)
        {
            var arr = NSBundle.MainBundle.LoadNib("TimeSliderPageView", null, null);
            var view = Runtime.GetNSObject<TimeSliderPageView>(arr.ValueAt(0));
            view.Page = page;
            view.populateViews();
            return view;
        }

        public NSAttributedString FormatPageText()
        {
            var titleAttributes = new UIStringAttributes
            {
                Font = UIFont.BoldSystemFontOfSize(13)
            };

            String title = TimeSliderPage.Title;
            NSMutableAttributedString attributedString = new NSMutableAttributedString(title + "\n\n" + TimeSliderPage.Text);
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
            if(TimeSliderPage != null)
            {
                if (floatingText != null)
                {
                    floatingText.TextView.AttributedText = FormatPageText();
                }

                timeSlider.MinimumValue = 0;
                timeSlider.MaximumValue = TimeSliderPage.Dates.Count - 1;
                timeSlider.Value = 0;
                timeSlider.ValueChanged += TimeSLiderValueChanged;
                timeSlider.Step = 1f;

                SetImage(TimeSliderPage.Images[(int)timeSlider.Value]);
                sliderLabel.Text = TimeSliderPage.Images[(int)timeSlider.Value].Description;
            }
        }

        void TimeSLiderValueChanged(Object sender, EventArgs e)
        {
            SetImage(TimeSliderPage.Images[(int)timeSlider.Value]);
            sliderLabel.Text = TimeSliderPage.Images[(int)timeSlider.Value].Description;
        }
    }
}