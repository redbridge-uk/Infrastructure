using System;
using CoreGraphics;
using Redbridge.Forms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Redbridge.Xamarin.Forms.iOS.Cells
{
	internal class BusyiOSCell : UITableViewCell
	{
		public UILabel HeadingLabel { get; private set; }
        public UIActivityIndicatorView BusyIndicatorView { get; private set; }

		public BusyCell NativeCell { get; private set; }
		public Element Element => NativeCell;

		public BusyiOSCell(string cellId, BusyCell cell) : base(UITableViewCellStyle.Default, cellId)
		{
			NativeCell = cell;

			SelectionStyle = UITableViewCellSelectionStyle.None;

            HeadingLabel = new UILabel()
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.Black,
			};
            BusyIndicatorView = new UIActivityIndicatorView()
            {
                
            };
            BusyIndicatorView.HidesWhenStopped = false;
            BusyIndicatorView.Color = UIColor.Black;
            BusyIndicatorView.StartAnimating();
			ContentView.Add(HeadingLabel);
			ContentView.Add(BusyIndicatorView);
		}

		public void UpdateCell(BusyCell cell)
		{
			HeadingLabel.Text = cell.Text;
            HeadingLabel.TextColor = cell.TextColour.ToUIColor();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			HeadingLabel.Frame = new CGRect(15, 9, ContentView.Bounds.Width - 63, 25);
			BusyIndicatorView.Frame = new CGRect(ContentView.Bounds.Width - 63, 6, 33, 33);
		}
	}
}
