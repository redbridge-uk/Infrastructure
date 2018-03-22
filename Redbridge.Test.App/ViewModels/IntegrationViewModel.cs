using System.Windows.Input;
using Redbridge.SDK;
using Redbridge.Forms;
using Xamarin.Forms;

namespace TesterApp
{
	public class IntegrationViewModel : TableViewModel
	{
		private ICommand _saveCommand;
		readonly IActionSheetController _actionController;

		public IntegrationViewModel(IActionSheetController actionController, INavigationService navigationService, ISchedulerService scheduler) : base(navigationService, scheduler) 
		{
			_actionController = actionController;
			Title = "Toolbar Tester";

			_saveCommand = new Command((obj) => 
			{
				var options = new ActionSheetViewModel()
				{
					CancelMessage = "Cancel",
					DestructionMessage = "Darth!",
					Title = "Evil?",
					Options = new [] 
					{ 
						new ActionSheetOption() { Title = "A" },
						new ActionSheetOption() { Title = "B" },
						new ActionSheetOption() { Title = "C" }
					}
				};
				_actionController.ShowActionSheet(options);
			});

			Toolbar.Add(new TextToolbarItemViewModel("Save", _saveCommand));
			var section = AddSection("Balls");
			section.AddCell(new TextCellViewModel() { Text = "Tester" });
			section.AddCell(new NumericEntryCellViewModel(23.5M) { Label = "Tester" });
		}
	}
}
