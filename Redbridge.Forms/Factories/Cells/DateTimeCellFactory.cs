using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class DateTimeCellFactory : TextEntryCellFactory
	{
		protected override Cell OnCreateCell(IEntryCellViewModel model)
		{
			var cell = base.OnCreateCell(model);
			return cell;
		}
	}
}
