using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class RedbridgeBusyContentPage : ContentPage, IView
	{
		private IBusyPageViewModel _currentViewModel;
		private BusyPageConfigurationManager<IBusyPageViewModel> _pageManager;

		public RedbridgeBusyContentPage()
		{
			_pageManager = new BusyPageConfigurationManager<IBusyPageViewModel>(this);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var context = this.BindingContext as IBusyPageViewModel;

			if (context != null)
			{
				ConnectViewModel(context);
			}
		}

		private void DisconnectCurrentModel()
		{
			_pageManager.Dispose();
			_currentViewModel = null;
		}

		protected BusyPageConfigurationManager<IBusyPageViewModel> PageManager
		{
			get { return _pageManager; }
		}

		private void ConnectViewModel(IBusyPageViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			_currentViewModel = viewModel;
			_pageManager.ConfigurePage(_currentViewModel);

			if (Content != null)
			{
				var layout = Content as Layout<View>;
				if (layout != null)
				{
					layout.Children.Add(_pageManager.ActivityIndicator);
				}

				_pageManager.SetBusyHost(Content);
			}
		}

		public IBusyPageViewModel CurrentModel
		{
			get { return _currentViewModel; }
		}

		protected override void OnAppearing()
		{
			_currentViewModel?.OnAppearing();
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			_currentViewModel?.OnDisappearing();
			base.OnDisappearing();
		}
	}
}
