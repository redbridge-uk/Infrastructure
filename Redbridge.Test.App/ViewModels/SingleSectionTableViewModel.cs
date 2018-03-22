using System;
using System.Windows.Input;
using Redbridge.SDK;
using Redbridge.Forms;
using Xamarin.Forms;

namespace TesterApp
{

    public class SingleSectionTableViewModel : TableViewModel
	{
		private TableSectionViewModel _topSection;
        private TableSectionViewModel _secondSection;
		private Command _saveCommand;

		public SingleSectionTableViewModel(IActionSheetController sheetController, INavigationService navigationService, ISchedulerService scheduler, IViewModelFactory viewModelFactory) 
			: base(navigationService, scheduler)
		{
			if (viewModelFactory == null) throw new ArgumentNullException(nameof(viewModelFactory));
			Title = "Date Picker";
			ShowNavigationBar = true;
			ShowSearchBar = true;
			SearchBar.PlaceholderText = "Find something";
			Intent = TableIntent.Form;

			_saveCommand = new Command((obj) => 
			{
				var viewModel = viewModelFactory.CreateModel<IntegrationViewModel>();
				navigationService.PushModalAsync(viewModel);
			});;

			SeparatorStyle = TableCellSeparatorStyle.SingleLine;
			_topSection = AddSection("Settings");

			var dataThing = new SomeDataThing() { MyDateTimeValue = DateTime.UtcNow, MySwitchValue = true, MyTextValue = "Balls would be", MyEnumValue = MyTestEnum.Bags };
			_topSection.AddCell(new NullableSwitchCellViewModel<SomeDataThing>(dataThing, d => d.MyNullableSwitchValue) { Label = "Nullable switch" });
			_topSection.AddCell(new NumericEntryCellViewModel<SomeDataThing>(dataThing, d => d.MyDecimalValue) { Label = "Numeric" });
			_topSection.AddCell(new NumericEntryCellViewModel<SomeDataThing>(dataThing, d => d.MyNullableDecimalValue) { Label = "Nullable numeric" });
            _topSection.AddBusyCell("Wooooah!");
            _topSection.AddDateTimeCell(dataThing, d => d.MyDateTimeValue, "Start", "Enter start date");
            _topSection.AddDateTimeCell(dataThing, d => d.MyDateTimeValue, "Finish", "Enter end date");
			Toolbar.Add(new TextToolbarItemViewModel() { Text = "Save", Command = _saveCommand });

            _secondSection = AddSection("Stuff");
            _secondSection.AddCell(new BusyCellViewModel() { Text = "Bloody busy!"});

            var commandSection = AddSection("Commands");
            commandSection.AddCell(new TextCellViewModel("Click me")
            {
                Command = new Command(() => 
                {
                    _secondSection.Cells.Clear();
                })
            });

            Icon = "schedule.png";
		}
	}
}
