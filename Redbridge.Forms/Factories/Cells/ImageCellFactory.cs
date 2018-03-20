using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class ImageCellFactory : CellFactory<ImageCellViewModel>
	{
		protected override Cell OnCreateCell(ImageCellViewModel model)
		{
			return new ImageCell();
		}

		protected override void OnSetBindings(Cell cell)
		{
			base.OnSetBindings(cell);
			cell.SetBinding(TextCell.TextProperty, "Text");
			cell.SetBinding(TextCell.DetailProperty, "Detail");
			cell.SetBinding(ImageCell.ImageSourceProperty, new Binding("Icon", BindingMode.OneWay));
		}
	}
}
