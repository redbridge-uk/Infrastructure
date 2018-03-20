using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class NullableSwitchCellFactory : CellFactory<NullableSwitchCellViewModel>
	{
		protected override Cell OnCreateCell(NullableSwitchCellViewModel model)
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
