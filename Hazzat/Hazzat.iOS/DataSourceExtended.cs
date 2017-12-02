using System;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace Hazzat.iOS
{
    public class DataSourceExtended : UITableViewSource
    {
        static readonly NSString CellIdentifier = new NSString("cell");
        public Dictionary<int, Dictionary<int,string>> objects;

        public UITableViewController _controller;

        public DataSourceExtended(Dictionary<int, Dictionary<int, string>> items, UITableViewController controller)
        {
            objects = items;
            _controller = controller;
        }


        public Dictionary<int, Dictionary<int, string>> Objects
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
            return objects.Count;
        }

        // Customize the appearance of table view cells.
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (UITableViewCell)tableView.DequeueReusableCell(CellIdentifier, indexPath);

            var items = objects.GetValueOrDefault((int)indexPath.Item);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            //cell.TextLabel.Text = items.;

            return cell;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return "!";
        }
        public override string TitleForFooter(UITableView tableView, nint section)
        {
            //return indexedTableItems[keys[section]].Count + " items";
            return "?";
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (_controller.GetType() == typeof(SectionViewController))
            {
                HymnPageViewController hymnview = _controller.Storyboard.InstantiateViewController("HymnPage") as HymnPageViewController;
                if (hymnview != null)
                {
                    _controller.NavigationController.PushViewController(hymnview, true);
                    tableView.DeselectRow(indexPath, true);
                }
                return;
            }

            SectionViewController sectionview = _controller.Storyboard.InstantiateViewController("SectionView") as SectionViewController;
            if (sectionview != null)
            {
                _controller.NavigationController.PushViewController(sectionview, true);
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
