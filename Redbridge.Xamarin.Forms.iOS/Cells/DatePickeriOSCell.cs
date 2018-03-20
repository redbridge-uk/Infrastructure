using System;
using Foundation;
using Redbridge.Forms;
using Redbridge.SDK.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Redbridge.Xamarin.Forms.iOS
{
	public class DatePickeriOSCell : UITableViewCell
	{
		public DatePickerCell NativeCell { get; private set; }
		public Element Element => NativeCell;
        private UIDatePicker _datePickerView;

		public DatePickeriOSCell(NSString cellId, DatePickerCell nativeCell) : base(UITableViewCellStyle.Default, cellId)
		{
            NativeCell = nativeCell;

			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			_datePickerView = new UIDatePicker();
            _datePickerView.Date = nativeCell.Date.ToNSDate();

            _datePickerView.ValueChanged += (sender, e) => 
            { 
                NativeCell.Date = _datePickerView.Date.ToDateTime(); 
            };

			ContentView.Add(_datePickerView);
		}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            _datePickerView.Frame = Bounds;
        }
	}
}
