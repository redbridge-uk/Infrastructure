using System;
using Redbridge.Diagnostics;
using Xamarin.Forms;

namespace Redbridge.Forms
{

    public abstract class MvvmApp<TStartViewModel> 
		: Application
	{
		private readonly ILogger _logger; 

		public MvvmApp(ILogger logger, IViewFactory viewFactory)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			var masterModel = viewFactory.CreatePageFromModel<TStartViewModel>();
			var masterPage = masterModel;
			MainPage = masterPage;
		}

        protected virtual RedbridgeTheme ApplicationTheme
        {
            get { return RedbridgeTheme.Default; }
        }

		protected ILogger Logger
		{
			get { return _logger; }
		}

		protected override void OnStart()
		{
            // Handle when your app starts
            RedbridgeThemeManager.SetTheme(ApplicationTheme);
            Logger.WriteDebug("MvvmApp application is starting...");
		}

		protected override void OnSleep()
		{
            Logger.WriteDebug("MvvmApp application is going to sleep...");
		}

		protected override void OnResume()
		{
            Logger.WriteDebug("MvvmApp application is resuming from sleep...");
		}
	}
}
