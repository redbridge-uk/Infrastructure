using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Redbridge.Linq;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class RedbridgeTabbedPage : TabbedPage, IView, IHardwareNavigationAware
    {
		private ITabbedNavigationControllerViewModel _viewModel;
		private readonly IViewFactory _viewFactory;
        private readonly Dictionary<Page, INavigationPageModel> _pageMap = new Dictionary<Page, INavigationPageModel>();

		public RedbridgeTabbedPage(IViewFactory viewFactory)
		{
			if (viewFactory == null) throw new ArgumentNullException(nameof(viewFactory));
			_viewFactory = viewFactory;
		}

        public event EventHandler<Page> OnHardwareBackButtonPressed;

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is IPageViewModel model)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (await model.NavigateBack())
                        OnHardwareBackButtonPressed?.Invoke(this, this);
                });

                return true;
            }

            return base.OnBackButtonPressed();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            var model = _pageMap[CurrentPage];
            _viewModel.SetPage(model);
        }

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var context = this.BindingContext as ITabbedNavigationControllerViewModel;
			if (context != null)
			{
				if (_viewModel != null)
					DisconnectCurrentModel();

				ConnectMasterViewModel(context);
			}
		}

		private void DisconnectCurrentModel()
		{
			_viewModel = null;
		}

		private void ConnectMasterViewModel(ITabbedNavigationControllerViewModel viewModel)
		{
			_viewModel = viewModel;
			BarBackgroundColor = viewModel.NavigationBarColour;
			BarTextColor = viewModel.NavigationBarTextColour;

			this.SetBinding(BarBackgroundColorProperty, "NavigationBarColour");
			this.SetBinding(BarTextColorProperty, "NavigationBarTextColour");

            _viewModel.Pages.ForEach(p => 
            {
                var page = _viewFactory.CreatePage(p);
                _pageMap.Add(page, p);
                Children.Add(page); 
            });
            CurrentPage = Children[0];
			//if (_viewModel.CurrentPage == null) throw new NotSupportedException("You must specify a current page for the master navigation controller.");
		}

		public INavigationPageModel CurrentModel
		{
			get { return _viewModel.CurrentPage; }
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
    }
}
