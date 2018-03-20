using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface INavigationService
	{
		Page CurrentPage { get; }
		Task PopAsync();
		Task PopModalAsync();
		Task PushAsync(IPageViewModel model);
		Task PushAsync<T>() where T: IPageViewModel;
		Task PushModalAsync(INavigationPageModel model);
		Task PushModalAsync<T>() where T:INavigationPageModel;
	}
}
