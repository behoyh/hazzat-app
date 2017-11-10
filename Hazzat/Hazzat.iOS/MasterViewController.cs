using System;
using CoreGraphics;
using System.Collections.Generic;
using Hazzat.Service;
using Foundation;
using UIKit;
using System.Threading.Tasks;

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
            _controller.GetSeasons(true, (evt, data) =>
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

		public class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString ("cell");
			string[] objects;

            public MasterViewController _controller;

            public DataSource(string[] items, MasterViewController controller)
            {
                objects = items;
                _controller = controller;
            }
			public IList<object> Objects {
                get { return objects; }
            }

			// Customize the number of sections in the table view.
			public override nint NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
                return objects.Length;
			}

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = (UITableViewCell)tableView.DequeueReusableCell(CellIdentifier, indexPath);

                string item = objects[indexPath.Row];

                if (cell == null)
                { 
                    cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); 
                }

                cell.TextLabel.Text = item;

                return cell;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                UIAlertController okAlertController = UIAlertController.Create("Row Selected", objects[indexPath.Row], UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
    
                _controller.PresentViewController(okAlertController, true, null);

                tableView.DeselectRow(indexPath, true);
            }

			public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}
		}
	}
}

