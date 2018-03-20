using Redbridge.Forms;
using Redbridge.Xamarin.Forms.iOS;
using Redbridge.DependencyInjection;
using Redbridge.Unity;
using Redbridge.Identity;
using Easilog.iOS;

namespace TesterApp.iOS
{
	public class TesterAppiOSUnityConfiguration : iOSFormsContainerConfiguration
	{
        public TesterAppiOSUnityConfiguration () 
        {
            AddAssembly(typeof(App).Assembly);
            AddAssembly(typeof(Redbridge.Test.App.Configuration.TestAppContainerConfiguration).Assembly);
            AddAssembly(typeof(TesterAppiOSUnityConfiguration).Assembly);
        }

		protected override IContainer CreateContainer()
		{
			return UnityContainerResolver.New();
		}

		protected override void OnRegisterTypes(IContainer container)
		{
            base.OnRegisterTypes(container);
            container.RegisterType<IAuthenticationClientFactory, ContainerAuthenticationClientFactory>();
            container.RegisterType<IAuthenticationClient, iOSGoogleAuthenticationClient>("Google");
		}
    }
}
