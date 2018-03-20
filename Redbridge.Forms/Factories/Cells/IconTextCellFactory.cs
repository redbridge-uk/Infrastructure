using System;
using Redbridge.Forms.ViewModels.Cells;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class IconTextCellFactory : CellFactory<IconTextCellViewModel>
    {
        readonly IIconResourceMapper _iconMapper;

        public IconTextCellFactory(IIconResourceMapper iconMapper)
        {
            if (iconMapper == null) throw new ArgumentNullException(nameof(iconMapper));
            _iconMapper = iconMapper;
        }

        protected override Cell OnCreateCell(IconTextCellViewModel model)
        {
            return new IconTextCell();
        }

        protected override void OnSetBindings(Cell cell)
        {
            base.OnSetBindings(cell);
            cell.SetBinding(IconTextCell.AccessoriesProperty, "Accessories");
            cell.SetBinding(IconTextCell.IconColourProperty, "IconColour");
            cell.SetBinding(IconTextCell.IconCodeProperty, "IconCode", BindingMode.OneWay, _iconMapper);
            cell.SetBinding(IconTextCell.TextProperty, "Text");
            cell.SetBinding(IconTextCell.TextColourProperty, "TextColour");
            cell.SetBinding(IconTextCell.DetailProperty, "Detail");
            cell.SetBinding(IconTextCell.DetailColourProperty, "DetailColour");
            cell.SetBinding(IconTextCell.BackgroundColourProperty, "BackgroundColour");
            cell.SetBinding(IconTextCell.DisplayModeProperty, "DisplayMode");
            cell.SetBinding(IconTextCell.CellHeightProperty, "CellHeight");
        }
    }
}