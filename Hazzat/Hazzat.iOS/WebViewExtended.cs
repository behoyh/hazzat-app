using Foundation;
using System;
using UIKit;
using Hazzat.iOS;
using System.Drawing;

namespace Hazzat.iOS
{
    public partial class WebViewExtended : UIWebView
    {
        UIImageView img;
        private RectangleF originalImageFrame = RectangleF.Empty;

        public WebViewExtended(IntPtr handle): base(handle)
        {
            img = new UIImageView(UIImage.FromFile("DragMe.png"));
            img.Frame = new CoreGraphics.CGRect(64, 64, img.Image.CGImage.Width, img.Image.CGImage.Height);
            this.AddSubview(img);

            // Save initial state
            originalImageFrame = (RectangleF)img.Frame;

            WireUpDragGestureRecognizer();
            WireUpTapGestureRecognizer();
        }

        public WebViewExtended(UIImageView img) : base()
        {
            this.img = img;

            this.AddSubview(img);

            // Save initial state
            originalImageFrame = (RectangleF)img.Frame;

            WireUpDragGestureRecognizer();
            WireUpTapGestureRecognizer();
        }

        public void ThreeDTouchEventHandler()
        {
            Action act = new Action(() => { 
                img.Alpha = 1;
            });
        }
        private void HandleDrag(UIPanGestureRecognizer recognizer)
        {
            // If it's just began, cache the location of the image
            if (recognizer.State == UIGestureRecognizerState.Began)
            {
                originalImageFrame = (System.Drawing.RectangleF)img.Frame;
            }

            // Move the image if the gesture is valid
            if (recognizer.State != (UIGestureRecognizerState.Cancelled | UIGestureRecognizerState.Failed
| UIGestureRecognizerState.Possible))
            {
                // Move the image by adding the offset to the object's frame
                PointF offset = (System.Drawing.PointF)recognizer.TranslationInView(img);
                RectangleF newFrame = originalImageFrame;
                newFrame.Offset(offset.X, offset.Y);
                //img.Frame = newFrame;
            }
        }

        private void WireUpDragGestureRecognizer()
        {
            // Create a new tap gesture
            UIPanGestureRecognizer gesture = new UIPanGestureRecognizer();

            // Wire up the event handler (have to use a selector)
            gesture.AddTarget(() => HandleDrag(gesture));

            // Add the gesture recognizer to the view
            img.AddGestureRecognizer(gesture);
        }

        private void WireUpTapGestureRecognizer()
        {
            // Create a new tap gesture
            WebViewGestureRecognizer tapGesture = null;

            // Report touch
            Action action = () => {
                img.Alpha = 0;
            };

            tapGesture = new WebViewGestureRecognizer(this)
            {
                // Configure it
                NumberOfTapsRequired = 2,  
            };

            // Add the gesture recognizer to the view
            this.AddGestureRecognizer(new WebViewGestureRecognizer(this));
        }
    }
}