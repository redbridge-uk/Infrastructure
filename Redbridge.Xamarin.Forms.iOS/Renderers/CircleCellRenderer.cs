using System;
using System.ComponentModel;
using Foundation;
using Redbridge.Forms;
using Redbridge.Xamarin.Forms.iOS;
using Redbridge.Xamarin.Forms.iOS.Cells;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircledTextCell), typeof(CircledTextCellRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
	public class CircledTextCellRenderer : ViewCellRenderer
	{
		static NSString rid = new NSString("CircleCell");
        CircledTextiOSCell cell;

		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var nativeCell = (CircledTextCell)item;

			cell = reusableCell as CircledTextiOSCell;
			if (cell == null)
				cell = new CircledTextiOSCell(rid, nativeCell);
			else
				cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;

			nativeCell.PropertyChanged += OnNativeCellPropertyChanged;
			cell.UpdateCell(nativeCell);
			return cell;
		}

		void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var nativeCell = (CircledTextCell)sender;

			if (e.PropertyName == CircledTextCell.TextProperty.PropertyName)
			{
				cell.TextLabel.Text = nativeCell.Text;
			}

			if (e.PropertyName == CircledTextCell.CircledTextProperty.PropertyName)
			{
				cell.CircledTextLabel.Text = nativeCell.Text;
			}
		}
	}
}
