using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class RedbridgeContentPage : ContentPage, IView, IHardwareNavigationAware
	{
        public event EventHandler<Page> BackButtonPressed;
		private IPageViewModel _currentViewModel;
		private PageConfigurationManager<IPageViewModel> _pageManager;

        public RedbridgeContentPage()
		{
			_pageManager = new PageConfigurationManager<IPageViewModel>(this);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

            if (this.BindingContext is IPageViewModel context)
            {
                ConnectViewModel(context);
            }
        }

		private void DisconnectCurrentModel()
		{
			_pageManager.Dispose();
			_currentViewModel = null;
		}

		private void ConnectViewModel(IPageViewModel viewModel)
		{
			_currentViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
			_pageManager.ConfigurePage(_currentViewModel);
		}

		protected PageConfigurationManager<IPageViewModel> PageManager
		{
			get { return _pageManager; }
		}

		public IPageViewModel CurrentModel
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
            if (CurrentModel != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (await CurrentModel.NavigateBack())
                        BackButtonPressed?.Invoke(this, this);
                });

                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}
