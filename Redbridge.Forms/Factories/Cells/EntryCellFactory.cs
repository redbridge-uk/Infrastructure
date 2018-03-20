using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public abstract class EntryCellFactory : CellFactory<IEntryCellViewModel>
	{
		protected override Cell OnCreateCell(IEntryCellViewModel model)
		{
			return new EntryCell() { };
		}

		protected override void OnSetBindings(Cell cell)
		{
            base.OnSetBindings(cell);
			cell.SetBinding(EntryCell.LabelProperty, "Label");
			cell.SetBinding(EntryCell.TextProperty, "Text");
			cell.SetBinding(EntryCell.KeyboardProperty, "Keyboard");
			cell.SetBinding(EntryCell.PlaceholderProperty, "PlaceholderText");

		}

		protected override void OnSetEvents(Cell cell, ITableCellViewModel model)
		{
			base.OnSetEvents(cell, model);
			var entryCellViewModel = model as IEntryCellViewModel;
			SetEvents(cell, entryCellViewModel);
		}

		private void SetEvents(Cell cell, IEntryCellViewModel entryCellViewModel)
		{
		}
}
}
