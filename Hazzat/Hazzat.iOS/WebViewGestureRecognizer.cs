using System;
using Foundation;
using Hazzat.iOS;
using UIKit;


public class TapGestureRecognizer : UITapGestureRecognizer
{
    private readonly WebViewExtended _extendedWebView;
    private readonly bool _is3DTouchCompat;

    public TapGestureRecognizer()
    {

    }

    public TapGestureRecognizer(WebViewExtended extendedWebView) : this()
    {
        _extendedWebView = extendedWebView;

        this.DelaysTouchesBegan = false;
        this.DelaysTouchesEnded = false;
        this.CancelsTouchesInView = false;

        //make sure the recognizer can work together with other recognizers
        this.ShouldRecognizeSimultaneously = (a, b) => true;

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
                _extendedWebView.ThreeDTouchEventHandler();
            }

        }
    }
}