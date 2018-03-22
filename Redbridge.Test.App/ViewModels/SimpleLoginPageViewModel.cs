using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Redbridge.SDK;
using Redbridge.Forms;
using Xamarin.Forms;
using Redbridge.Forms.Markup;

namespace TesterApp
{
    [View(typeof(SimpleLogPageView))]
	public class SimpleLoginPageViewModel : BusyPageViewModel
	{
		private Command _busyCommand;

		public SimpleLoginPageViewModel(ISchedulerService scheduler) : base(scheduler) 
		{
			_busyCommand = new Command((obj) => 
			{
				IsBusy = true;

				Observable.Interval(TimeSpan.FromSeconds(2), Scheduler.BackgroundScheduler).Take(1)
				          .ObserveOn(Scheduler.UiScheduler).Subscribe((l) => 
				{
					IsBusy = false;
				});
			}, (arg) => !IsBusy);
		}

		public ICommand SetBusyCommand
		{
			get { return _busyCommand; }
		}
	}
}
