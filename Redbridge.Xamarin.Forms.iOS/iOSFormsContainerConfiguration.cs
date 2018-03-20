using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.LocationServices;
using Redbridge.SDK.iOS;
using Redbridge.SDK.iOS.LocationServices;

namespace Redbridge.Xamarin.Forms.iOS
{
	public abstract class iOSFormsContainerConfiguration : ContainerConfiguration
	{
		protected override ILogger CreateLogger()
		{
			return new iOSLogger();
		}

		protected override IApplicationSettingsRepository CreateAppSettingsRepository()
		{
			return new iOSApplicationSettingsRepository(); 
		}

        protected override void OnRegisterTypes(DependencyInjection.IContainer container)
        {
            base.OnRegisterTypes(container);
            container.RegisterType<ILocationService, iOSLocationService>(DependencyInjection.LifeTime.Container);
        }
	}
}
