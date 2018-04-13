using System;
using Redbridge.SDK;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public abstract class NavigationPageViewModel : BusyPageViewModel, INavigationPageModel
	{
		private readonly INavigationService _navigationService;
		private ImageSource _barImageSource;

		public NavigationPageViewModel(INavigationService navigationService, ISchedulerService scheduler) : base(scheduler)
		{
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
			ShowToolbar = false;
		}

		public ImageSource NavigationBarIcon
		{
			get { return _barImageSource; }
			set
			{
				if (_barImageSource != value)
				{
					_barImageSource = value;
					OnPropertyChanged(nameof(NavigationBarIcon));
				}
			}
		}

        protected INavigationService Navigation => _navigationService;
	}
}
