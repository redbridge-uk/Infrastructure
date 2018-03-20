using Redbridge.Configuration;
using Redbridge.DependencyInjection;
using Redbridge.Diagnostics;
using Redbridge.SDK.Droid;
using Redbridge.Unity;

namespace TesterApp.Droid
{
	public class DroidContainerConfiguration : ContainerConfiguration
	{
		protected override IContainer CreateContainer()
		{
			return UnityContainerResolver.New();
		}

		protected override IApplicationSettingsRepository CreateAppSettingsRepository()
		{
			return new DroidApplicationSettingsRepository();
		}

		protected override ILogger CreateLogger()
		{
			return new DroidLogger();
		}
	}
}
