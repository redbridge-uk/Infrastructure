using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class SwitchCellFactory : CellFactory<SwitchCellViewModel>
	{
		protected override Cell OnCreateCell(SwitchCellViewModel model)
		{
			return new SwitchCell();
		}

		protected override void OnSetBindings(Cell cell)
		{
			base.OnSetBindings(cell);
			cell.SetBinding(SwitchCell.TextProperty, "Label");
			cell.SetBinding(SwitchCell.OnProperty, "Value");
		}
	}
}
