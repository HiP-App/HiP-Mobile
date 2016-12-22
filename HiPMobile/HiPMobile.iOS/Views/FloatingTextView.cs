using Foundation;
using ObjCRuntime;
using System;
using UIKit;
using CoreGraphics;

namespace HiPMobile.iOS
{
    public partial class FloatingTextView : UIView
    {
        public nfloat ViewBringDownY { get; set; }
        public nfloat ViewShiftUpY { get; set; }
        Boolean textExpanded;
        public UITextView TextView
        {
            get
            {
                return textView;
            }
        }

        public FloatingTextView (IntPtr handle) : base (handle)
        {
            textExpanded = false;
        }

        public static FloatingTextView Create()
        {
            var arr = NSBundle.MainBundle.LoadNib("FloatingTextView", null, null);
            var view = Runtime.GetNSObject<FloatingTextView>(arr.ValueAt(0));
            view.TranslatesAutoresizingMaskIntoConstraints = true;
            return view;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            ViewBringDownY = UIScreen.MainScreen.Bounds.Height - 100;
            ViewShiftUpY = UIScreen.MainScreen.Bounds.Height - 180;

            expandButton.TouchUpInside += delegate (object sender, EventArgs e)
            {
                if (!textExpanded)
                {
                    AnimateView(ViewShiftUpY, this);
                    expandButton.SetImage(UIImage.FromBundle("ExpandMore"), UIControlState.Normal);
                }
                else
                {
                    AnimateView(ViewBringDownY, this);
                    expandButton.SetImage(UIImage.FromBundle("ExpandLess"), UIControlState.Normal);
                }
                textExpanded = !textExpanded;
            };
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            //force the view to scroll the text to the top
            textView.SetContentOffset(CGPoint.Empty, false);
        }
        void AnimateView(nfloat frameY, UIView view)
        {
            var frame = view.Frame;
            nfloat h = (view.Frame.Y - frameY);

            UIView.Animate(0.2f, 0.1f, UIViewAnimationOptions.CurveEaseIn, delegate
            {
                frame.Y = frameY;
                view.Frame = frame;
            }, () =>
            {
                if (h < 0)
                {
                    frame.Height = view.Frame.Height + h;
                    view.Frame = frame;
                }
            });

            if (h > 0)
            {
                frame.Height = view.Frame.Height + h;
                view.Frame = frame;
            }
        }
    }
}