using System;
using System.ComponentModel;
using Foundation;
using Redbridge.Forms;
using Redbridge.Xamarin.Forms.iOS;
using Redbridge.Xamarin.Forms.iOS.Cells;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IconTextCell), typeof(IconTextCellRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
    public class IconTextCellRenderer : ViewCellRenderer
	{
		static NSString rid = new NSString("IconTextCell");
        IconiOSCell cell;

		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
            var nativeCell = (IconTextCell)item;

            cell = reusableCell as IconiOSCell;
			if (cell == null)
                cell = new IconiOSCell(rid, nativeCell);
			else
				cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;

			nativeCell.PropertyChanged += OnNativeCellPropertyChanged;
			cell.UpdateCell(nativeCell);
			return cell;
		}

		void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var nativeCell = (IconTextCell)sender;
            if (e.PropertyName == IconTextCell.TextProperty.PropertyName)
			{
				cell.TextLabel.Text = nativeCell.Text;
			}
            if (e.PropertyName == IconTextCell.TextColourProperty.PropertyName)
            {
                cell.TextLabel.TextColor = nativeCell.TextColour.ToUIColor();
            }
            if (e.PropertyName == IconTextCell.DetailProperty.PropertyName)
            {
                cell.DetailLabel.Text = nativeCell.Detail;
            }
            if (e.PropertyName == IconTextCell.DetailColourProperty.PropertyName)
            {
                cell.DetailLabel.TextColor = nativeCell.DetailColour.ToUIColor();
            }
            if (e.PropertyName == IconTextCell.IconCodeProperty.PropertyName)
            {
                
            }
		}
	}
}
