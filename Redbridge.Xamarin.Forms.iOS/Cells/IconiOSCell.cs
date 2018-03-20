using System;
using CoreGraphics;
using Redbridge.Forms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Redbridge.Xamarin.Forms.iOS.Constraints;

namespace Redbridge.Xamarin.Forms.iOS.Cells
{
    internal class IconiOSCell : UITableViewCell
    {
        public UILabel HeadingLabel { get; private set; }
        public UILabel DetailLabel { get; private set; }
        public UIImageView IconImage { get; private set; }
        public UIStackView TextContainer { get; private set; }

		public IconTextCell NativeCell { get; private set; }
		public Element Element => NativeCell;

        public IconiOSCell(string cellId, IconTextCell cell) : base(UITableViewCellStyle.Default, cellId)
		{
			NativeCell = cell;

            SelectionStyle = cell.SelectionStyle.ToiOSSelectionStyle(); 

            var code = cell.IconCode; // Icon code is mapped by a converter further up the chain. It is managed by binding from the cell factory.
            if ( !string.IsNullOrWhiteSpace(code) )
                IconImage = new UIImageView(UIImage.FromFile(code));
            else
                IconImage = new UIImageView();

            TextContainer = new UIStackView();
            TextContainer.Axis = UILayoutConstraintAxis.Vertical;
            TextContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            TextContainer.Alignment = UIStackViewAlignment.Fill;

            HeadingLabel = new UILabel()
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.Black,
                Font = UIFont.PreferredBody,
			};

            DetailLabel = new UILabel()
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.Black,
                Font = UIFont.PreferredSubheadline,
            };

            ContentView.Add(IconImage);
            ContentView.Add(TextContainer);
            TextContainer.AddArrangedSubview(HeadingLabel);

            if ( cell.DisplayMode != Redbridge.Forms.ViewModels.Cells.IconCellViewMode.TitleOnly )
                TextContainer.AddArrangedSubview(DetailLabel);

            switch ( cell.CellHeight )
            {
                case TableCellHeight.Large:
                    IconImage.AddWidthAndHeightConstraints(32, 32);
                    this.AddLeftConstraint(IconImage, 15);
                    break;
                case TableCellHeight.Medium:
                    IconImage.AddWidthAndHeightConstraints(24, 24);
                    this.AddLeftConstraint(IconImage, 10);
                    break;
                default:
                    IconImage.AddWidthAndHeightConstraints(16, 16);
                    this.AddLeftConstraint(IconImage, 5);
                    break;
            }

            this.AddVerticalAlignmentConstraint(IconImage);
            this.AddVerticalAlignmentConstraint(TextContainer);
            this.AddLeftMarginConstraintBetween(IconImage, TextContainer, 5);

            HeadingLabel.AddHeightConstraint(16);
            DetailLabel.AddHeightConstraint(16);
		}

		public void UpdateCell(IconTextCell cell)
		{
			HeadingLabel.Text = cell.Text;
            HeadingLabel.TextColor = cell.TextColour.ToUIColor();
            DetailLabel.Text = cell.Detail;
            DetailLabel.TextColor = cell.DetailColour.ToUIColor();
            BackgroundColor = cell.BackgroundColour.ToUIColor();
            //var code = cell.IconCode;
            Accessory = cell.Accessories.ToiOSCellAssessory();
		}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            TextContainer.LayoutSubviews();
        }

	}
}
