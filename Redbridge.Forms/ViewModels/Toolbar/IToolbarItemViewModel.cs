using System.Windows.Input;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface IToolbarItemViewModel
	{
		ICommand Command { get; }
		object CommandParameter { get; }
		FileImageSource Icon { get; }
		string Text { get; set; }
		ToolbarItemOrder Position { get; }
	}
	
}