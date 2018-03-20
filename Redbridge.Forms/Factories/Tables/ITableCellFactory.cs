using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface ITableCellFactory
	{
		Cell CreateCellView(ITableCellViewModel arg);
	}
}