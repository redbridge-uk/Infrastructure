using Redbridge.Forms;

namespace TesterApp
{
    public class StartupPageViewModel : NavigationControllerViewModel<TestEditableTableViewModel>
	{
		public StartupPageViewModel(IViewModelFactory viewModelFactory) : base(viewModelFactory) 
		{
		}
	}
}
