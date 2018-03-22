using Redbridge.SDK;
using Redbridge.Forms;

namespace TesterApp
{
    public class BasicStartupPage : NavigationPageViewModel
    {
        public BasicStartupPage(INavigationService navigationService, ISchedulerService scheduler)
            : base(navigationService, scheduler)
        {
            Toolbar.Add(new TextToolbarItemViewModel() { Text = "Startup" });
        }
    }
}
