using Foundation;
using System;
using UIKit;
using Hazzat.Service;

namespace Hazzat.iOS
{
    public partial class SectionViewController : UIViewController
    {
        public SectionViewController (IntPtr handle) : base (handle)
        {
            
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "SectionPage";

            HazzatController controller = new HazzatController();

            //controller.GetSeasons(true, (evt, data) => {
            //
              //  foreach (var item in data.Result)
               // {
                //    SectionHymnStack.Add(new UILabel() { Text = item.Name });
               // }
           // });
        }
    }
}