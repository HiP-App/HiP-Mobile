using Foundation;
using ObjCRuntime;
using System;
using UIKit;

namespace HiPMobile.iOS
{
    public partial class FloatingTextView : UIView
    {
        public nfloat ViewBringDownY { get; set; }
        public nfloat ViewShiftUpY { get; set; }

        public FloatingTextView (IntPtr handle) : base (handle)
        {
        }

        public static FloatingTextView Create()
        {
            var arr = NSBundle.MainBundle.LoadNib("FloatingTextView", null, null);
            var view = Runtime.GetNSObject<FloatingTextView>(arr.ValueAt(0));
            return view;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            ViewBringDownY = Frame.Height * 0.8f;
            ViewShiftUpY = Frame.Height - Frame.Height / 3f;

            expandButton.SetImage(UIImage.FromBundle("ExpandMore"), UIControlState.Normal);
            expandButton.SetImage(UIImage.FromBundle("ExpandLess"), UIControlState.Selected);

            expandButton.TouchUpInside += delegate (object sender, EventArgs e)
            {
                if (!expandButton.Selected)
                {
                    AnimateView(ViewShiftUpY, this);
                }
                else
                {
                    AnimateView(ViewBringDownY, this);
                }
                expandButton.Selected = !expandButton.Selected;
            };
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