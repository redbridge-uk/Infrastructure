using System;
using Redbridge.Forms.ViewModels.Cells;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class CircledTextCellFactory : CellFactory<CircledTextCellViewModel>
	{
		protected override Cell OnCreateCell(CircledTextCellViewModel model)
		{
			return new CircledTextCell();
		}

		protected override void OnSetBindings(Cell cell)
		{
			base.OnSetBindings(cell);
            cell.SetBinding(IconTextCell.AccessoriesProperty, "Accessories");
            cell.SetBinding(CircledTextCell.CircledTextProperty, "CircledText");
            cell.SetBinding(CircledTextCell.CircledTextColourProperty, "CircledTextColour");
            cell.SetBinding(CircledTextCell.CircleBackgroundColourProperty, "CircleBackgroundColour");
			cell.SetBinding(CircledTextCell.TextProperty, "Text");
            cell.SetBinding(CircledTextCell.TextColourProperty, "TextColour");
			cell.SetBinding(CircledTextCell.DetailProperty, "Detail");
            cell.SetBinding(CircledTextCell.DetailColourProperty, "DetailColour");
		}
	}
}
