using System;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace Redbridge.SDK
{
	public interface ISchedulerService
	{
		IScheduler UiScheduler { get; }
		IScheduler BackgroundScheduler { get; }
		TaskScheduler TaskScheduler { get; }
	}
}
