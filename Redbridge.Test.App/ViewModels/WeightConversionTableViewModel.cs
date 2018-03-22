using System;
using System.Windows.Input;
using Redbridge.SDK;
using Redbridge.Forms;
using Xamarin.Forms;
using System.Reactive.Linq;
using Redbridge.Converters;

namespace TesterApp
{
	public class WeightConversionTableViewModel : TableViewModel
	{
		private TableSectionViewModel _topSection;

		public WeightConversionTableViewModel(IActionSheetController sheetController, INavigationService navigationService, ISchedulerService scheduler, IViewModelFactory viewModelFactory) 
			: base(navigationService, scheduler)
		{
			if (viewModelFactory == null) throw new ArgumentNullException(nameof(viewModelFactory));

			Title = "Weight Conversion";
			ShowNavigationBar = true;
			Intent = TableIntent.Form;

			SeparatorStyle = TableCellSeparatorStyle.SingleLine;
			_topSection = AddSection("Converter");

			var poundsCell = new NumericEntryCellViewModel() { Label = "Pounds", IsInteger = false };
			var kilosCell = new NumericEntryCellViewModel() { Label = "Kilos", IsInteger = false };


			poundsCell.Values.ObserveOn(Scheduler.UiScheduler)
			          .Where(e => e.HasValue)
					  .Subscribe((e) => 
			{
				// We have a new pounds value, we need to process this and push the related kilos value to the kilos cell.
				kilosCell.Value = WeightConverter.ToKilosFromPounds(e.Value);
			});
			
			_topSection.AddCell(poundsCell);


			kilosCell.Values.ObserveOn(Scheduler.UiScheduler)
					 .Where(e => e.HasValue)
					  .Subscribe((e) =>
			{
				poundsCell.Value = ImperialWeight.FromKilos(e.Value).TotalPounds;
			});

			_topSection.AddCell(kilosCell);

		}
	}
}
