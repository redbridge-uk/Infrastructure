using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Redbridge.Forms;
using UIKit;
using Foundation;
using System;

[assembly: ExportRenderer(typeof(TableView), typeof(Redbridge.Xamarin.Forms.iOS.MenuTableViewRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
    public class RedbridgeTableViewModelRenderer : TableViewModelRenderer
    {
        private readonly ITableViewModel _model;

        public RedbridgeTableViewModelRenderer(ITableViewModel model, TableView view) : base(view)
        {
            _model = model;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var section = _model.Table.Sections[(int)indexPath.LongSection];
            var cell = section.Cells[(int)indexPath.LongRow];
            switch (cell.CellHeight)
            {
                case TableCellHeight.Custom:
                    return 220F;

                case TableCellHeight.Large:
                    return 120F;

                case TableCellHeight.Medium:
                    return 80F;

                default:
                    return 40F;
            }
        }
    }

    public class MenuTableViewRenderer : TableViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
				return;

			if (e.NewElement != null)
			{
                var tableView = Control as UITableView;
                if (e.NewElement.BindingContext is ITableViewModel model)
                {
                    tableView.RowHeight = UITableView.AutomaticDimension;
                    tableView.EstimatedRowHeight = 40;

                    if (!model.HasUnevenRows)
                        tableView.Source = new RedbridgeTableViewModelRenderer(model, e.NewElement);

                    switch (model.SeparatorStyle)
                    {
                        case TableCellSeparatorStyle.SingleLine:
                            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
                            break;

                        case TableCellSeparatorStyle.SingleLineEtched:
                            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLineEtched;
                            break;

                        case TableCellSeparatorStyle.DoubleLineEtched:
                            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.DoubleLineEtched;
                            break;

                        default:
                            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                            break;
                    }
                }
            }
		}
	}
}