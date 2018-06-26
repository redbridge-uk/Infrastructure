using Redbridge.SDK;
using Redbridge.Forms;

namespace TesterApp
{
    public class WizardTableViewModel : TableViewModel, IWizardPageViewModel
    {
        public WizardTableViewModel(INavigationService navigationService, ISchedulerService schedulerService) : base(navigationService, schedulerService) { }

        public bool CanCancel
        {
            get
            {
                return true;
            }
        }

        public bool CanFinish
        {
            get
            {
                return true;
            }
        }

        public bool CanGoBackwards
        {
            get
            {
                return true;
            }
        }

        public bool CanGoForwards
        {
            get
            {
                return true;
            }
        }
    }
}
