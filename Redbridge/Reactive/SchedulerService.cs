using System;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;

namespace Redbridge.Reactive
{
	public class SchedulerService : ISchedulerService
	{
		public static SchedulerService FromCurrentSynchronizationContext()
		{
			var scheduler = new SynchronizationContextScheduler(SynchronizationContext.Current);
			return new SchedulerService(scheduler, Scheduler.Default, TaskScheduler.Default);
		}

		public SchedulerService(IScheduler uiScheduler, IScheduler backgroundScheduler, TaskScheduler taskScheduler)
		{
            UiScheduler = uiScheduler ?? throw new ArgumentNullException(nameof(uiScheduler));
			BackgroundScheduler = backgroundScheduler ?? throw new ArgumentNullException(nameof(backgroundScheduler));
			TaskScheduler = taskScheduler ?? throw new ArgumentNullException(nameof(taskScheduler));
		}

		public IScheduler UiScheduler { get; }
		public IScheduler BackgroundScheduler { get; }
		public TaskScheduler TaskScheduler { get; }
	}
}
