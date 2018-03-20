using System;
using System.Threading.Tasks;

namespace Redbridge.Forms
{

	public class AlertController : IAlertController
	{
		readonly INavigationService _navigationService;
        private bool _alertDisplayed = false;

		public AlertController(INavigationService navigationService)
		{
			_navigationService = navigationService;
			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
		}

		public async Task<AlertResponse> ShowAlert(AlertViewModel alert)
		{
            if (_alertDisplayed) throw new NotSupportedException("An alert is already on display on a page. Please ensure it is dismissed before raising another.");

			var page = _navigationService.CurrentPage;
			bool result = true;
            _alertDisplayed = true;

			if (string.IsNullOrWhiteSpace(alert.CancelMessage))
			{ 
				await page.DisplayAlert(alert.Title, alert.Message, alert.AcceptMessage);
			}
			else
				result = await page.DisplayAlert(alert.Title, alert.Message, alert.AcceptMessage, alert.CancelMessage);

            _alertDisplayed = false;
			return new AlertResponse(result);
		}
	}
	
}
