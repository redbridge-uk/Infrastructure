using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace Redbridge.Reactive
{
	public interface ISchedulerService
	{
		IScheduler UiScheduler { get; }
		IScheduler BackgroundScheduler { get; }
		TaskScheduler TaskScheduler { get; }
	}
}
