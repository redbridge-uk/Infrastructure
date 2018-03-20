using System;
using Redbridge.Forms;
using Redbridge.Forms.ViewModels;
using UIKit;

namespace Redbridge.Xamarin.Forms.iOS
{
	public static class CellAssessoryConverter
	{
		public static UITableViewCellAccessory ToiOSCellAssessory(this CellIndicators accessory)
		{
			switch (accessory)
			{
				case CellIndicators.CheckMark:
					return UITableViewCellAccessory.Checkmark;
				case CellIndicators.Detail:
					return UITableViewCellAccessory.DetailButton;
				case CellIndicators.Disclosure:
					return UITableViewCellAccessory.DisclosureIndicator;
				case CellIndicators.DisclosureDetail:
					return UITableViewCellAccessory.DetailDisclosureButton;
				default:
					return UITableViewCellAccessory.None;
			}
		}
	}

    public static class TableCellSelectionStyleConverter
    {
        public static UITableViewCellSelectionStyle ToiOSSelectionStyle (this TableCellSelectionStyle style)
        {
            switch (style)
            {
                case TableCellSelectionStyle.Blue:
                    return UITableViewCellSelectionStyle.Blue;
                case TableCellSelectionStyle.Default:
                    return UITableViewCellSelectionStyle.Default;
                case TableCellSelectionStyle.Grey:
                    return UITableViewCellSelectionStyle.Gray;
                default:
                    return UITableViewCellSelectionStyle.None;
            }
        }
    }
}
