using System;
using Redbridge.SDK;
using Redbridge.Forms;

namespace TesterApp
{

    public class SetupWizardViewModel : WizardControllerViewModel
	{
		public class SetupPage1 : WizardTableViewModel
		{
			public SetupPage1(INavigationService navigationService, ISchedulerService schedulerService) : base(navigationService, schedulerService) 
			{
				Title = "Page 1";
			}
		}
		public class SetupPage2 : WizardTableViewModel 
		{
			public SetupPage2(INavigationService navigationService, ISchedulerService schedulerService) : base(navigationService, schedulerService) 
			{
				Title = "Page 2";
			}
		}
		public class SetupPage3 : WizardTableViewModel 
		{
			public SetupPage3(INavigationService navigationService, ISchedulerService schedulerService) : base(navigationService, schedulerService) 
			{
				Title = "Page 3";
			}
		}

		public SetupWizardViewModel(IViewModelFactory modelFactory, INavigationService navigationService, ISchedulerService scheduler) 
			: base(modelFactory, navigationService, scheduler) 
		{
			AddPageRange(new IWizardPageViewModel[]
			{
				ViewModelFactory.CreateModel<SetupPage1>(),
				ViewModelFactory.CreateModel<SetupPage2>(),
				ViewModelFactory.CreateModel<SetupPage3>(),
			});
		}
	}
}
