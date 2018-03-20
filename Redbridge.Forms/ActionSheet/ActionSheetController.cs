using System;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Redbridge.Forms
{
	public class ActionSheetController : IActionSheetController
	{
		readonly INavigationService _navigationService;
		private Subject<ActionSheetResponse> _responses;

		public ActionSheetController(INavigationService navigationService)
		{
			if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
			_navigationService = navigationService;
			_responses = new Subject<ActionSheetResponse>();
		}

		public IObservable<ActionSheetResponse> Response
		{
			get { return _responses; } 
		}

		public async Task<ActionSheetResponse> ShowActionSheet(ActionSheetViewModel alert)
		{
			if (string.IsNullOrWhiteSpace(alert.Title)) throw new NotSupportedException("An action sheet must have a title.");

			var page = _navigationService.CurrentPage;
			var options = alert.Options != null && alert.Options.Any() ? alert.Options.Select(opt => opt.Title).ToArray() : new string[] { };
			var result = await page.DisplayActionSheet(alert.Title ?? "Choose item", alert.CancelMessage, alert.DestructionMessage, options);

			var buttonOption = ActionSheetButtonResponse.Option;

			if (result == alert.CancelMessage)
				buttonOption = ActionSheetButtonResponse.Cancel;
			else if (result == alert.DestructionMessage)
				buttonOption = ActionSheetButtonResponse.Destruction;

			var response = new ActionSheetResponse(buttonOption, alert.Owner);

			if (buttonOption == ActionSheetButtonResponse.Option)
			{
				var textOptions = alert.Options.Select(opt => opt.Title).ToList();
				var responseIndex = textOptions.IndexOf(result);
				var optionList = alert.Options.ToList();
				response.Option = optionList[responseIndex];
			}

			_responses.OnNext(response);
			return response;
		}
	}
	
}