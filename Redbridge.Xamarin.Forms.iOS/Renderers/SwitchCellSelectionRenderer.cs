using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Redbridge.Xamarin.Forms.iOS;
using Redbridge.Forms;

[assembly: ExportRenderer(typeof(SwitchCell), typeof(SwitchCellSelectionRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
	public class SwitchCellSelectionRenderer : SwitchCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var viewModel = item.BindingContext as ITableCellViewModel;
			UITableViewCell cell = base.GetCell(item, reusableCell, tv);

			if (viewModel != null)
			{
				if (viewModel.AllowCellSelection)
					cell.SelectionStyle = UITableViewCellSelectionStyle.Default;
				else
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			}

			return cell;
		}
	}
}
