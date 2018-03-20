using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface ICellTextProvider : ITableCellViewModel
	{
		string Text { get; }
	}

	public class TextCellFactory : CellFactory<ICellTextProvider>
	{
		protected override Cell OnCreateCell(ICellTextProvider model)
		{
			return new TextCell();
		}

		protected override void OnSetBindings(Cell cell)
		{
			base.OnSetBindings(cell);
			cell.SetBinding(TextCell.TextProperty, "Text");
			cell.SetBinding(TextCell.DetailProperty, "Detail");
		}
	}
}
