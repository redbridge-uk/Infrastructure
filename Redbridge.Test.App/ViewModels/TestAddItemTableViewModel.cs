using Redbridge.SDK;
using Redbridge.Forms;
using Xamarin.Forms;

namespace TesterApp
{
    public class TestAddItemTableViewModel : TableViewModel
    {
        private readonly Command _doneCommand;
        private readonly Command _cancelCommand;

        public TestAddItemTableViewModel(IAlertController alerts, INavigationService navigationService, ISchedulerService scheduler)
            : base(navigationService, scheduler)
        {
            _doneCommand = new Command(async () => { await Navigation.PopModalAsync(); });
            _cancelCommand = new Command(async () => { await Navigation.PopModalAsync(); });

            Toolbar.Add(new TextToolbarItemViewModel()
            {
                Text = "Done",
                Command = _doneCommand,
                Position = ToolbarItemOrder.Primary
            });

            Toolbar.Add(new TextToolbarItemViewModel()
            {
                Text = "Cancel",
                Command = _cancelCommand,
                Position = ToolbarItemOrder.Secondary
            });

            Title = "Hard coded";
            ShowNavigationBar = true;
        }
    }
}
