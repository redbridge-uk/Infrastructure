using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class DatePickerCellFactory : CellFactory
	{
		protected override Cell OnCreate(ITableCellViewModel model)
		{
			return new DatePickerCell();
		}

        protected override void OnSetBindings(Cell cell)
        {
            base.OnSetBindings(cell);
            cell.SetBinding(DatePickerCell.DateProperty, "Date");
        }
	}
}
