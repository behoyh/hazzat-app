using System;
using CoreGraphics;
using System.Collections.Generic;
using Hazzat.Service;
using Foundation;
using UIKit;

namespace Touch
{
	public partial class MasterViewController : UITableViewController
	{
		DataSource _dataSource;
        HazzatController _controller;
		public MasterViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString("Menu", "Main Menu");

			// Custom initialization
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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			NavigationItem.LeftBarButtonItem = EditButtonItem;

			var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, AddNewItem);
			NavigationItem.RightBarButtonItem = addButton;

			

            _controller = new HazzatController();

            List<string> lst = new List<string>();
            _controller.GetSeasons(true, (evt, data) =>
            {
                foreach (var item in data.Result)
                {
                    lst.Add(item.Name);
                }
                InvokeOnMainThread(() =>
                {
                    TableView.Source = _dataSource = new DataSource(lst.ToArray());
                });
            });
		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString ("cell");
			string[] objects;

            public DataSource(string[] items)
            {
                objects = items;
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

			public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}
		}
	}
}

