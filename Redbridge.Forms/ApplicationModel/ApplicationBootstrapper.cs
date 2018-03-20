using Redbridge.SDK;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public abstract class ApplicationBootstrapper<TApp, TContainerConfiguration>
		where TApp : Application
		where TContainerConfiguration : ContainerConfiguration, new()
	{
		public static TApp Create<TApplication, TContainer>()
			where TApplication : Application
			where TContainer : ContainerConfiguration, new()
		{
			var configuration = new TContainerConfiguration();
			var container = configuration.Container;
			container.RegisterInstance<ISchedulerService>(SchedulerService.FromCurrentSynchronizationContext());
			var app = container.Resolve<TApp>();
			return app;
		}

		public TApp Create()
		{
			var configuration = GetContainerConfiguration();
			var container = configuration.Container;
			container.RegisterInstance<ISchedulerService>(SchedulerService.FromCurrentSynchronizationContext());
			var app = container.Resolve<TApp>();
			return app;
		}

		protected virtual TContainerConfiguration GetContainerConfiguration()
		{
			return new TContainerConfiguration();
		}
	}
}
