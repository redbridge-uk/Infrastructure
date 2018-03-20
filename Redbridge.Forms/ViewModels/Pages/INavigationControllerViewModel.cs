using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public interface ITabbedNavigationControllerViewModel : INavigationControllerViewModel
    {
        ObservableCollection<INavigationPageModel> Pages { get; }

        void SetPage(INavigationPageModel model);
    }

	public interface INavigationControllerViewModel : IViewModel
	{
		Color NavigationBarColour { get; set; }
		Color NavigationBarTextColour { get; set; }
		ImageSource NavigationBarIcon { get; set; }
		INavigationPageModel CurrentPage { get; }
	}
}