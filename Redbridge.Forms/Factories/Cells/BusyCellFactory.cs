using System;
using Redbridge.Forms;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class BusyCellFactory : CellFactory<BusyCellViewModel>
	{
		protected override Cell OnCreateCell(BusyCellViewModel model)
		{
            return new BusyCell() { Text = model.Text };
		}

		protected override void OnSetBindings(Cell cell)
		{
			base.OnSetBindings(cell);
			cell.SetBinding(TextCell.TextProperty, "Text");
		}
	}
}
