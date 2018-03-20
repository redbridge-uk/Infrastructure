using Foundation;
using UIKit;
using Redbridge.Forms;
using Redbridge.Xamarin.Forms.iOS;

namespace TesterApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public class Bootstrapper : ApplicationBootstrapper<App, TesterAppiOSUnityConfiguration> {}

		public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
		{
			global::Xamarin.Forms.Forms.Init();
			RedbridgeForms.Init();
            RedbridgeThemeManager.SetTheme(RedbridgeTheme.Black);
			var bootstrapper = new Bootstrapper();
			LoadApplication(bootstrapper.Create());
			return base.FinishedLaunching(uiApplication, launchOptions);
		}
	}
}
