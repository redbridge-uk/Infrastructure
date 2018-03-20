using System;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;

namespace Redbridge.SDK
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
			if (uiScheduler == null) throw new ArgumentNullException(nameof(uiScheduler));
			if (backgroundScheduler == null) throw new ArgumentNullException(nameof(backgroundScheduler));
			if (taskScheduler == null) throw new ArgumentNullException(nameof(taskScheduler));
			UiScheduler = uiScheduler;
			BackgroundScheduler = backgroundScheduler;
			TaskScheduler = taskScheduler;
		}

		public IScheduler UiScheduler { get; }
		public IScheduler BackgroundScheduler { get; }
		public TaskScheduler TaskScheduler { get; }
	}
}
