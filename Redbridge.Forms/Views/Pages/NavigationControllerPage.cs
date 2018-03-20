using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class ModalNavigationControllerPage : NavigationControllerPage
    {
        public ModalNavigationControllerPage(IViewFactory viewFactory) : base(viewFactory) {}
    }

    public class NavigationControllerPage : NavigationPage, IView
	{
		private NavigationControllerViewModel _viewModel;
		private readonly IViewFactory _viewFactory;

		public NavigationControllerPage(IViewFactory viewFactory)
		{
			_viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == "CurrentPage")
			{
				if (_viewModel != null )
				{
					var currentPageViewModel = CurrentPage.BindingContext as INavigationPageModel;
					_viewModel.CurrentPage = currentPageViewModel;
					BarBackgroundColor = _viewModel.CurrentPage.NavigationBarColour;
					BarTextColor = _viewModel.CurrentPage.NavigationBarTextColour;
				}
			}
		}

		protected override void OnAppearing()
		{
			_viewModel?.OnAppearing();
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			_viewModel?.OnDisappearing();
			base.OnDisappearing();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var context = BindingContext as NavigationControllerViewModel;
			if (context != null)
			{
				if (_viewModel != null)
					DisconnectCurrentModel();
				
				ConnectMasterViewModel(context);
			}
		}

		private void ConnectMasterViewModel(NavigationControllerViewModel viewModel)
		{
			_viewModel = viewModel;

			if (_viewModel.CurrentPage == null) throw new NotSupportedException("You must specify a current page for the master navigation controller.");
			BarBackgroundColor = viewModel.NavigationBarColour;
			BarTextColor = viewModel.NavigationBarTextColour;
			this.SetBinding(BarBackgroundColorProperty, "NavigationBarColour");
			this.SetBinding(BarTextColorProperty, "NavigationBarTextColour");

			var page = _viewFactory.CreatePage(_viewModel.CurrentPage);
			PushAsync(page);
		}

		protected override bool OnBackButtonPressed()
		{
			var result = base.OnBackButtonPressed();
			CurrentPageModel.NavigateBack();
			return result;
		}

		private void DisconnectCurrentModel()
		{
			_viewModel = null;
		}

		public INavigationPageModel CurrentPageModel
		{
			get { return _viewModel.CurrentPage; }
		}
	}
}