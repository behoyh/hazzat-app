using Foundation;
using System;
using UIKit;
using System.Drawing;
using Hazzat.Service;
using System.IO;

namespace Hazzat.iOS
{
    public partial class HymnPageViewController : UIViewController
    {
        private RectangleF originalImageFrame = RectangleF.Empty;
        private HazzatController _controller;


        public HymnPageViewController(IntPtr handle) : base (handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "HymnPage";

            // Save initial state
            originalImageFrame = (System.Drawing.RectangleF)DragImage.Frame;

            string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");
            _controller = new HazzatController();
            _controller.GetSeasonServiceHymnText(392, (src, data) => {
                InvokeOnMainThread(() =>
                {
                    //WebView.LoadHtmlString($"<html> {data?.Result?[0]?.Content_Coptic} </html>", new NSUrl(contentDirectoryPath, true));
                    TipBox.Alpha = 0;
                });
            });

            WireUpDragGestureRecognizer();
            WireUpTapGestureRecognizer();
        }


        private void HandleDrag(UIPanGestureRecognizer recognizer)
        {
            // If it's just began, cache the location of the image
            if (recognizer.State == UIGestureRecognizerState.Began)
            {
                originalImageFrame = (System.Drawing.RectangleF)DragImage.Frame;
            }

            // Move the image if the gesture is valid
            if (recognizer.State != (UIGestureRecognizerState.Cancelled | UIGestureRecognizerState.Failed
| UIGestureRecognizerState.Possible))
            {
                // Move the image by adding the offset to the object's frame
                PointF offset = (System.Drawing.PointF)recognizer.TranslationInView(DragImage);
                RectangleF newFrame = originalImageFrame;
                newFrame.Offset(offset.X, offset.Y);
                DragImage.Frame = newFrame;
            }
        }

        private void WireUpDragGestureRecognizer()
        {
            // Create a new tap gesture
            UIPanGestureRecognizer gesture = new UIPanGestureRecognizer();

            // Wire up the event handler (have to use a selector)
            gesture.AddTarget(() => HandleDrag(gesture));

            // Add the gesture recognizer to the view
            DragImage.AddGestureRecognizer(gesture);
        }

        private void WireUpTapGestureRecognizer()
        {
            // Create a new tap gesture
            UITapGestureRecognizer tapGesture = null;

            // Report touch
            Action action = () => {
                DragImage.Alpha = 0;
            };

            tapGesture = new UITapGestureRecognizer(action);

            // Configure it
            tapGesture.NumberOfTapsRequired = 2;

            // Add the gesture recognizer to the view
            Canvas.AddGestureRecognizer(tapGesture);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            UITouch touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                // Get the pressure
                var force = touch.Force;
                var maxForce = touch.MaximumPossibleForce;

                if (force == maxForce)
                {
                    DragImage.Alpha = 1;
                }

            }
        }
    }
}
