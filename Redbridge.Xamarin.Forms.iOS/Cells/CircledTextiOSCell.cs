using System;
using CoreGraphics;
using Foundation;
using Redbridge.Forms;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Redbridge.Xamarin.Forms.iOS.Cells
{
    public partial class CircledTextiOSCell : UITableViewCell
    {
        public UILabel CircledTextLabel { get; private set; }
        public UILabel HeadingLabel { get; private set; }
        public UILabel DetailLabel { get; private set; }
        public CircledTextCell NativeCell { get; private set; }

		public CircledTextiOSCell(NSString cellId, CircledTextCell nativeCell) : base(UITableViewCellStyle.Default, cellId)
        {
			NativeCell = nativeCell;
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			CircledTextLabel = new UILabel()
			{
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.Black,
			};

            CircledTextLabel.ClipsToBounds = true;
            CircledTextLabel.TextAlignment = UITextAlignment.Center;


			HeadingLabel = new UILabel()
			{
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.Black,
			};

			DetailLabel = new UILabel()
			{
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.Black,
			};

            ContentView.Add(CircledTextLabel);
            ContentView.Add(HeadingLabel);
            ContentView.Add(DetailLabel);
		}

        internal void UpdateCell(CircledTextCell nativeCell)
        {
            CircledTextLabel.Layer.CornerRadius = 16;
            CircledTextLabel.Text = nativeCell.CircledText;
            CircledTextLabel.TextColor = nativeCell.CircledTextColour.ToUIColor();
            CircledTextLabel.BackgroundColor = nativeCell.CircleBackgroundColour.ToUIColor();
            HeadingLabel.Text = nativeCell.Text;
            HeadingLabel.TextColor = nativeCell.TextColour.ToUIColor();
        }

        public override void LayoutSubviews()
		{
			base.LayoutSubviews();
            CircledTextLabel.Frame = new CGRect(10, 4, 32, 32);
            HeadingLabel.Frame = new CGRect(50, 8, ContentView.Bounds.Width - 63, 25);
		}
    }
}
