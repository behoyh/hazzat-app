using System;
using Foundation;
using Hazzat.iOS;
using UIKit;


public class WebViewGestureRecognizer : UITapGestureRecognizer
{
    private readonly WebViewExtended _extendedWebView;

    public WebViewGestureRecognizer()
    {
        this.DelaysTouchesBegan = false;
        this.DelaysTouchesEnded = false;
        this.CancelsTouchesInView = false;

        //make sure the recognizer can work together with other recognizers
        this.ShouldRecognizeSimultaneously = (a, b) => true;
    }

    public WebViewGestureRecognizer(WebViewExtended extendedWebView) : this()
    {
        _extendedWebView = extendedWebView;
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