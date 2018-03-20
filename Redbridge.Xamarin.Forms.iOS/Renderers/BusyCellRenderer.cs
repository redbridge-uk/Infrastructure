using System;
using System.ComponentModel;
using Foundation;
using Redbridge.Forms;
using Redbridge.Xamarin.Forms.iOS;
using Redbridge.Xamarin.Forms.iOS.Cells;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BusyCell), typeof(BusyCellRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
	public class BusyCellRenderer : ViewCellRenderer
	{
		static NSString rid = new NSString("BusyCell");
        BusyiOSCell cell;

		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var nativeCell = (BusyCell)item;

			cell = reusableCell as BusyiOSCell;
			if (cell == null)
				cell = new BusyiOSCell(rid, nativeCell);
			else
				cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;

			nativeCell.PropertyChanged += OnNativeCellPropertyChanged;
			cell.UpdateCell(nativeCell);
			return cell;
		}

		void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var nativeCell = (BusyCell)sender;
			if (e.PropertyName == BusyCell.TextProperty.PropertyName)
			{
				cell.TextLabel.Text = nativeCell.Text;
			}
		}
	}
}
