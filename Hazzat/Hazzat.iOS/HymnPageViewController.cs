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
        private HazzatController _controller;


        public HymnPageViewController(IntPtr handle) : base (handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "HymnPage";

            string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");
            _controller = new HazzatController();
            _controller.GetSeasonServiceHymnText(392, (src, data) => {
                InvokeOnMainThread(() =>
                {
                    WebViewExtend.LoadHtmlString($"<html> {data?.Result?[0]?.Content_Coptic} </html>", new NSUrl(contentDirectoryPath, true));
                    TipBox.Alpha = 0;
                });
            });
        }
    }
}
