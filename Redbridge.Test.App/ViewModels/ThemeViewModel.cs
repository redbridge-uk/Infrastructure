using Redbridge.SDK;
using Redbridge.Forms;
using System.Reactive.Linq;
using System;
using System.Threading.Tasks;

namespace TesterApp
{
	public class ThemeViewModel : TableViewModel
	{
		public ThemeViewModel(INavigationService navigationService, ISchedulerService scheduler) : base(navigationService, scheduler) 
		{
            ShowNavigationBar = false;
			var section = AddSection("Test section");
			section.AddCell(new TextCellViewModel() { Text = "Tester" });
			section.AddCell(new NumericEntryCellViewModel(23.5M) { Label = "Tester" });
			RedbridgeThemeManager.Theme
			                     .Where(rt => rt != null)
			                     .ObserveOn(Scheduler.UiScheduler)
								 .Subscribe((rt) =>
			{
				Title = rt.Name;
			});
		}
	}
}
