using Moq;
using Redbridge.SDK;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace Redbridge.Xamarin.Forms.Tests.Mocks
{
    public class MockSchedulerService : ISchedulerService
    {
        public IScheduler UiScheduler => new Mock<IScheduler>().Object;

        public IScheduler BackgroundScheduler => new Mock<IScheduler>().Object;

        public TaskScheduler TaskScheduler => throw new System.NotImplementedException();
    }
}
