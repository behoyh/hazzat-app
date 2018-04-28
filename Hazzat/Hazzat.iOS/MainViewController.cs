using System;
using CoreGraphics;
using System.Collections.Generic;
using Hazzat.Service;
using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace Hazzat.iOS
{
    public partial class MainViewController : UITableViewController
    {
        private DataSource _dataSource;
        private HazzatController _controller;

        public MainViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Menu", "Main Menu");
        }

        public void AddNewItem(object sender, EventArgs args)
        {
            using (var indexPath = NSIndexPath.FromRowSection(0, 0))
                TableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
        }

        public void AddNewItems(int length)
        {
            TableView.BeginUpdates();
            for (var i = 0; i <= length; i++)
            {
                using (var indexPath = NSIndexPath.FromRowSection(i, 0))
                    TableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
            }
            TableView.EndUpdates();
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _controller = new HazzatController();

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromFile("settings.png")
            , UIBarButtonItemStyle.Plain
                , (ooo, oo) =>
                {
                var donationView = this.Storyboard.InstantiateViewController("DonationsView") as DonationController;
                this.NavigationController.PushViewController(donationView, true);
                });

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromFile("info.png")
           , UIBarButtonItemStyle.Plain
           , (ooo, oo) =>
           {

           });

            List<KeyValuePair<int, string>> lst = new List<KeyValuePair<int, string>>();
            _controller.GetSeasons(true, (src, data) =>
            {
                foreach (var item in data.Result)
                {
                    lst.Add(new KeyValuePair<int, string>(item.ItemId,item.Name));
                }
                InvokeOnMainThread(() =>
                {
                    TableView.Source = _dataSource = new DataSource(lst, this);
                    TableView.ReloadData();
                });
            });
        }
    }
}
