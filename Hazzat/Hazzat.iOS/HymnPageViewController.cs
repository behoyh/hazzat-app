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
        string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");
        public int id { get; set; }


        public HymnPageViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "HymnPage";

            var TextButton = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (s, e) =>
            {
                LoadText();
            });

            var HazzatButton = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (s, e) =>
            {
                LoadHazzat();
            });

            var VerticalHazzatButton = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (s, e) =>
            {
                LoadVerticalHazzat();
            });

            this.SetToolbarItems(new UIBarButtonItem[] {
                TextButton, HazzatButton, VerticalHazzatButton
            }, false);

            this.NavigationController.ToolbarHidden = false;


            _controller = new HazzatController();

            LoadText();

            HymnView.Animate(5, 0, 0, () =>
            {
                TipBox.Alpha = 0;
            }, () =>
            {
                TipBox.Layer.RemoveAllAnimations();
                TipBox.Dispose();
            });
        }

        private void LoadText()
        {
            _controller.GetSeasonServiceHymnText(id, (src, data) =>
            {
                if (data.Result != null && data.Result.Length != 0)
                {
                    _controller.GetTextRowDelimiterToken((s, d) =>
                    {
                        string textDelimiter = d.Result;
                        InvokeOnMainThread(() =>
                        {
                            WebViewExtend.LoadHtmlString(HymnPageViewRenderer.RenderText(data.Result, textDelimiter), new NSUrl(contentDirectoryPath, true));
                        });
                    });
                }
            });
        }

        private void LoadHazzat()
        {
            _controller.GetSeasonServiceHymnHazzat(id, (src, data) =>
            {
                if (data.Result != null && data.Result.Length != 0)
                {
                    InvokeOnMainThread(() =>
                    {
                        WebViewExtend.LoadHtmlString(HymnPageViewRenderer.RenderHazzatHtml(data.Result), new NSUrl(contentDirectoryPath, true));
                    });
                }
            });
        }

        private void LoadVerticalHazzat()
        {
            _controller.GetSeasonServiceHymnVerticalHazzat(id, (src, data) =>
            {
                if (data.Result != null && data.Result.Length != 0)
                {
                    InvokeOnMainThread(() =>
                    {
                    WebViewExtend.LoadHtmlString(HymnPageViewRenderer.RenderHazzatHtml(data.Result), new NSUrl(contentDirectoryPath, true));
                    });
                }
            });
        }
    }
}