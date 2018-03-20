using System;
using Redbridge.SDK;

namespace Redbridge.Forms
{
	public class WizardPageViewModel : NavigationPageViewModel, IWizardPageViewModel
	{
		public WizardPageViewModel(INavigationService navigationService, ISchedulerService schedulerService) : base(navigationService, schedulerService) { }

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
