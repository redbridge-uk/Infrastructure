using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class RedbridgeBusyContentPage: ContentPage, IView, IHardwareNavigationAware
	{
        public event EventHandler<Page> BackButtonPressed;
		private IBusyPageViewModel _currentViewModel;
		private BusyPageConfigurationManager<IBusyPageViewModel> _pageManager;

        public RedbridgeBusyContentPage()
		{
			_pageManager = new BusyPageConfigurationManager<IBusyPageViewModel>(this);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

            if (this.BindingContext is IBusyPageViewModel context)
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
            _currentViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
			_pageManager.ConfigurePage(_currentViewModel);

			if (Content != null)
			{
                if (Content is Layout<View> layout)
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
         
        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is IPageViewModel model)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (await model.NavigateBack())
                        BackButtonPressed?.Invoke(this, this);
                });

                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}
