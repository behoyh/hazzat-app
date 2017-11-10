using System;
using Foundation;
using UIKit;
using Hazzat;
using System.Collections.Generic;
using Touch;

namespace Hazzat.iOS
{
    public class DataSource : UITableViewSource
    {
        static readonly NSString CellIdentifier = new NSString("cell");
        string[] objects;

        public UITableViewController _controller;

        public DataSource(string[] items, UITableViewController controller)
        {
            objects = items;
            _controller = controller;
        }
        public IList<object> Objects
        {
            get { return objects; }
        }

        // Customize the number of sections in the table view.
        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
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
            HymnPageViewController view = _controller.Storyboard.InstantiateViewController("HymnPage") as HymnPageViewController;
            if (tableView.GetType() == typeof(SectionViewController))
            {
                _controller.NavigationController.PushViewController(view,true);
            }
            SectionViewController newview = _controller.Storyboard.InstantiateViewController("SectionView") as SectionViewController;
            if (newview != null)
            {
                _controller.NavigationController.PushViewController(newview, true);
            }
            tableView.DeselectRow(indexPath, true);
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            // Return false if you do not want the specified item to be editable.
            return true;
        }
    }
}
