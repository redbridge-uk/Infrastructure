using System;
using Redbridge.Forms.Markup;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class ModalPageControllerViewModel<TStartup>: NavigationControllerViewModel<TStartup>, IPageViewModel
		where TStartup: INavigationPageModel
	{
		public ModalPageControllerViewModel(IViewModelFactory viewModelFactory) : base(viewModelFactory) {}

        public string Title => "Modal Dialog";

        public ToolbarViewModel Toolbar => CurrentPage.Toolbar;

        public SearchBarViewModel SearchBar => CurrentPage.SearchBar;

        public ImageSource Icon => CurrentPage.Icon;

        public bool ShowNavigationBar => false;

        public bool NavigateBack()
        {
            return true;
        }
    }

    [View(typeof(ModalNavigationControllerPage))]
	public class ModalPageControllerViewModel : NavigationControllerViewModel
	{
		public ModalPageControllerViewModel(INavigationPageModel rootPage) : base(rootPage){}
	}
}
