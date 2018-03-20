using System;
using System.Reactive.Linq;
using Redbridge.Diagnostics;
using Xamarin.Forms;

namespace Redbridge.Forms
{

	public class BusyPageConfigurationManager<TViewModel> : PageConfigurationManager<TViewModel>
												where TViewModel : IBusyPageViewModel
	{
		private View _busyHost;
		ActivityIndicator _activityIndicator = new ActivityIndicator() { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };

		public BusyPageConfigurationManager(ContentPage page) : base(page)
		{
			_busyHost = page.Content;
		}

		public ActivityIndicator ActivityIndicator { get { return _activityIndicator; } }

		public override void ConfigurePage(TViewModel viewModel)
		{
			base.ConfigurePage(viewModel);

			Disposibles.Add(viewModel.Busy.Subscribe(state =>
			{
				_activityIndicator.Color = viewModel.BusyIndicatorColour;
				_activityIndicator.IsRunning = state;
				_activityIndicator.IsEnabled = state;
				_activityIndicator.IsVisible = state;
                if (_busyHost != null)
                {
                    if (RedbridgeThemeManager.HasTheme)
                        _busyHost.Opacity = state ? RedbridgeThemeManager.Current.BusyOverlayOpacity : 1.0D;
                    else
                        _busyHost.Opacity = state ? 0.3D: 1D;
                }

				if (ParentPage != null)
					ParentPage.IsEnabled = !state;
			}));
		}

		public void SetBusyHost(View host)
		{
			if (host == null) throw new ArgumentNullException(nameof(host));
			_busyHost = host;
		}
	}

}
