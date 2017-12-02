using Foundation;
using System;
using UIKit;
using Hazzat.iOS;
using System.Drawing;

namespace Hazzat.iOS
{
    public partial class WebViewExtended : UIWebView
    {
        private readonly UIImageView _img;
        private RectangleF _originalImageFrame = RectangleF.Empty;
        private bool _is3DTouchCompat;


        public WebViewExtended(IntPtr handle) : base(handle)
        {
            _img = new UIImageView(UIImage.FromFile("DragMe.png"));
            _img.Frame = new CoreGraphics.CGRect(Center.X, Center.Y, 32, 32);
            this.AddSubview(_img);
            _img.Alpha = 0;
            _img.ExclusiveTouch = true;
            _img.UserInteractionEnabled = true;

            _originalImageFrame = (RectangleF)_img.Frame;

            if (TraitCollection.ForceTouchCapability != UIForceTouchCapability.Unavailable)
            {
                _is3DTouchCompat = true;
            }

            WireUpDragGestureRecognizer();
            WireUpTapGestureRecognizer();
        }

        public void ThreeDTouchEventHandler()
        {
            Action act = new Action(() =>
            {
                _img.Alpha = 1;
            });

            act.Invoke();
        }
        private void HandleDrag(UIPanGestureRecognizer recognizer)
        {
            // If it's just began, cache the location of the image
            if (recognizer.State == UIGestureRecognizerState.Began)
            {
                _originalImageFrame = (System.Drawing.RectangleF)_img.Frame;
            }

            // Move the image if the gesture is valid
            if (recognizer.State != (UIGestureRecognizerState.Cancelled | UIGestureRecognizerState.Failed | UIGestureRecognizerState.Possible))
            {
                // Move the image by adding the offset to the object's frame
                PointF offset = (System.Drawing.PointF)recognizer.TranslationInView(_img);
                RectangleF newFrame = _originalImageFrame;
                newFrame.Offset(offset.X, offset.Y);
                _img.Frame = newFrame;
            }
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            //Important: call the base function
            base.TraitCollectionDidChange(previousTraitCollection);

            //See if the new TraitCollection value includes force touch
            if (TraitCollection.ForceTouchCapability == UIForceTouchCapability.Available)
            {
                _is3DTouchCompat = true;
            }
            _is3DTouchCompat = false;
        }

        private void WireUpDragGestureRecognizer()
        {
            // Create a new tap gesture
            UIPanGestureRecognizer gesture = new UIPanGestureRecognizer()
            {
                ShouldRecognizeSimultaneously = (a, b) => true,
                DelaysTouchesBegan = false,
                DelaysTouchesEnded = false,
                CancelsTouchesInView = true
            };

            // Report touch
            Action action = () =>
            {
                HandleDrag(gesture);
            };

            // Wire up the event handler (have to use a selector)
            gesture.AddTarget(action);

            // Add the gesture recognizer to the view
            _img.AddGestureRecognizer(gesture);
        }

        private void WireUpTapGestureRecognizer()
        {
            // Create a new tap gesture
            UILongPressGestureRecognizer longGesture = new UILongPressGestureRecognizer()
            {
                ShouldRecognizeSimultaneously = (a, b) => true,
                DelaysTouchesBegan = false,
                DelaysTouchesEnded = false,
                CancelsTouchesInView = false
            };


            // Report touch
            Action action = () =>
            {
                _img.Alpha = 1;
            };

            longGesture.AddTarget(action);


            if (_is3DTouchCompat)
            {
                //3D Touch Enabled
                this.AddGestureRecognizer(new TapGestureRecognizer(this));
            }
            else
            {
                //No 3D Touch
                this.AddGestureRecognizer(longGesture);
            }

            UITapGestureRecognizer tapGesture = new UITapGestureRecognizer()
            {
                ShouldRecognizeSimultaneously = (a, b) => true,
                DelaysTouchesBegan = false,
                DelaysTouchesEnded = false,
                CancelsTouchesInView = false,
                NumberOfTapsRequired = 2,
            };

            //Hide _img
            Action action2 = () =>
            {
                _img.Alpha = 0;
            };

            tapGesture.AddTarget(action2);
            this.AddGestureRecognizer(tapGesture);
        }
    }
}