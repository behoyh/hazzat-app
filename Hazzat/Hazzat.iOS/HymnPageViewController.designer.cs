// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Hazzat.iOS
{
    [Register ("HymnPageViewController")]
    partial class HymnPageViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Hazzat.iOS.HymnView Canvas { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView TipBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Hazzat.iOS.WebViewExtended WebViewExtend { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Canvas != null) {
                Canvas.Dispose ();
                Canvas = null;
            }

            if (TipBox != null) {
                TipBox.Dispose ();
                TipBox = null;
            }

            if (WebViewExtend != null) {
                WebViewExtend.Dispose ();
                WebViewExtend = null;
            }
        }
    }
}