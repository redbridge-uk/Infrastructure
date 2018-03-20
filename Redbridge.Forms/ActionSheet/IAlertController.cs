using System;
using System.Threading.Tasks;

namespace Redbridge.Forms
{
	public interface IActionSheetController<T>
	{
		Task<ActionSheetResponse<T>> ShowActionSheet(ActionSheetViewModel sheet);
		IObservable<ActionSheetResponse<T>> Response { get; }
}

	public interface IActionSheetController
	{
		Task<ActionSheetResponse> ShowActionSheet(ActionSheetViewModel sheet);
		IObservable<ActionSheetResponse> Response { get; }
	}
}
