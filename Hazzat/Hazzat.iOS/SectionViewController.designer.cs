// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Hazzat.iOS
{
    [Register ("SectionViewController")]
    partial class SectionViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView SectionHymnStack { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (SectionHymnStack != null) {
                SectionHymnStack.Dispose ();
                SectionHymnStack = null;
            }
        }
    }
}