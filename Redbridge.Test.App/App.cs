using Redbridge.Forms;
using Redbridge.Diagnostics;
using TesterApp.ViewModels;

namespace TesterApp
{
    
    public class App : MvvmApp<StartupPageViewModel>
	{
		public App(ILogger logger, IViewFactory viewFactory) : base(logger, viewFactory) {}
	}
}
