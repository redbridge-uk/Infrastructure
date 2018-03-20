using System;
using Redbridge.SDK;

namespace Redbridge.Forms
{
	public class TabbedViewModel : NavigationPageViewModel
	{
		public TabbedViewModel(INavigationService navigationService, ISchedulerService scheduler) : base(navigationService, scheduler) { }
	}
}
