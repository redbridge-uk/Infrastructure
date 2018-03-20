using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Foundation;
using Redbridge.Forms;
using Redbridge.Xamarin.Forms.iOS;
using Redbridge.Xamarin.Forms.iOS.Cells;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerCell), typeof(DatePickerCellRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
	public class DatePickerCellRenderer : ViewCellRenderer
	{
		static NSString rid = new NSString("DatePickerCell");
		DatePickeriOSCell cell;

		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var nativeCell = (DatePickerCell)item;

			cell = reusableCell as DatePickeriOSCell;
            if (cell == null)
                cell = new DatePickeriOSCell(rid, nativeCell);
            else
                cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;

			cell.NativeCell.PropertyChanged += OnNativeCellPropertyChanged;
			//cell.UpdateCell(nativeCell);

			//item.Height = 200;
			//item.ForceUpdateSize();

			CGRect rectCell = cell.Frame;
			rectCell.Height = 200;
			cell.Frame = rectCell;
            cell.Bounds = rectCell;
            cell.LayoutSubviews();
			return cell;
		}

		void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var nativeCell = (DatePickerCell)sender;
			if (e.PropertyName == DatePickerCell.DateProperty.PropertyName)
			{
                return;
			}
		}
	}
}
