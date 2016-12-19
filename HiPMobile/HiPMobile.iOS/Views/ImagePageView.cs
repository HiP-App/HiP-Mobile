using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using Foundation;
using ObjCRuntime;
using System;
using UIKit;

namespace HiPMobile.iOS
{
    public partial class ImagePageView : ExhibitDetailsPageView
    {
        ImagePage ImagePage
        {
            get
            {
                return Page.ImagePage;
            }
        }
        public ImagePageView (IntPtr handle) : base (handle)
        {
        }

        public static ImagePageView Create(Page page)
        {
            var arr = NSBundle.MainBundle.LoadNib("ImagePageView", null, null);
            var view = Runtime.GetNSObject<ImagePageView>(arr.ValueAt(0));
            view.Page = page;
            view.populateViews();
            return view;
        }

        void populateViews()
        {
            NSData imageData = NSData.FromArray(ImagePage.Image.Data);
            exhibitImage.Image = new UIImage(imageData);
        }

        //public NSAttributedString FormatPageText()
        //{
            //var titleAttributes = new UIStringAttributes
            //{
            //    Font = UIFont.BoldSystemFontOfSize(13)
            //};

            //String title = ImagePage.Title;
            //NSMutableAttributedString attributedString = new NSMutableAttributedString(title + "\n\n" + ImagePage.Text);
            //attributedString.SetAttributes(titleAttributes, new NSRange(0, title.Length));
            //return attributedString;
        //}
    }
}