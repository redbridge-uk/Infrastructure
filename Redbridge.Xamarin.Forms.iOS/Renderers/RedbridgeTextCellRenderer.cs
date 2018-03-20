using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Redbridge.Forms;
using Redbridge.Xamarin.Forms.iOS;

[assembly: ExportRenderer(typeof(TextCell), typeof(AccessoryItemTextCellRenderer))]
[assembly: ExportRenderer(typeof(ImageCell), typeof(AccessoryItemImageCellRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
	public class AccessoryItemTextCellRenderer : TextCellRenderer
	{
		public override UIKit.UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var viewModel = item.BindingContext as ITableCellViewModel;
			UITableViewCell cell = base.GetCell(item, reusableCell, tv);

			if (viewModel != null)
			{
				cell.Accessory = viewModel.Accessories.ToiOSCellAssessory();
			}

			return cell;
		}
	}

    public class AccessoryItemImageCellRenderer : ImageCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var viewModel = item.BindingContext as ITableCellViewModel;
            UITableViewCell cell = base.GetCell(item, reusableCell, tv);

            if (viewModel != null)
            {
                cell.Accessory = viewModel.Accessories.ToiOSCellAssessory();
            }

            return cell;
        }
    }
}
