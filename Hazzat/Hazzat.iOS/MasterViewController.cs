using System;
using CoreGraphics;
using System.Collections.Generic;
using Hazzat.Service;
using Foundation;
using UIKit;
using System.Threading.Tasks;
using Hazzat.iOS;

namespace Touch
{
	public partial class MasterViewController : UITableViewController
	{
		private DataSource _dataSource;
        private HazzatController _controller;

		public MasterViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString("Menu", "Main Menu");
		}

		void AddNewItem (object sender, EventArgs args)
		{
			_dataSource.Objects.Insert (0, DateTime.Now);

			using (var indexPath = NSIndexPath.FromRowSection (0, 0))
				TableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
		}
        void AddNewItems(int length)
        {
            for (var i = 0; i <= length ; i++)
            {
                using (var indexPath = NSIndexPath.FromRowSection(i, 0))
                    TableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
            }
        }

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _controller = new HazzatController();

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromFile("settings.png")
                ,UIBarButtonItemStyle.Plain 
                ,(ooo,oo)=>{
                    
                });

            List<string> lst = new List<string>();
            _controller.GetSeasons(true, (src, data) =>
            {
                foreach (var item in data.Result)
                {
                    lst.Add(item.Name);
                }
                InvokeOnMainThread(() =>
                {
                    TableView.Source = _dataSource = new DataSource(lst.ToArray(), this);
                });
            });
		}
	}
}

