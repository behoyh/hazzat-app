using Foundation;
using System;
using UIKit;
using System.Drawing;
using Hazzat.Service;
using System.IO;
using Hazzat.ViewModels;

namespace Hazzat.iOS
{
    public partial class HymnPageViewController : UIViewController
    {
        private HazzatController _controller;
        public int id { get; set; }


        public HymnPageViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "HymnPage";

            string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");
            _controller = new HazzatController();

            _controller.GetSeasonServiceHymnText(id, (src, data) =>
            {
                _controller.GetTextRowDelimiterToken((s, d) =>
                {
                    string textDelimiter = d.Result;
                    InvokeOnMainThread(() =>
                    {
                        WebViewExtend.LoadHtmlString(HymnPageViewRenderer.RenderText(data.Result, textDelimiter), new NSUrl(contentDirectoryPath, true));
                        TipBox.Alpha = 0;
                    });
                });
            });
        }
    }
}
