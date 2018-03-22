using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Redbridge.Forms;
using Redbridge.SDK;
using Xamarin.Forms;

namespace TesterApp
{
	public class ThemePickerViewModel : ItemPickerTableViewModel<RedbridgeTheme>
	{
		readonly IViewModelFactory _viewModelFactory;	

		public ThemePickerViewModel(IViewModelFactory viewModelFactory, INavigationService navigationService, ISchedulerService scheduler) 
			: base(new InMemoryThemeSource(), navigationService, scheduler)
		{
			if (viewModelFactory == null) throw new ArgumentNullException(nameof(viewModelFactory));
			_viewModelFactory = viewModelFactory;
			Title = "Choose theme";
			Intent = TableIntent.Form;
		}

		protected override void OnViewAppearing()
		{
			base.OnViewAppearing();
            Debug.WriteLine("Theme picker appearing");
		}

		protected override ITableCellViewModel CreateItemViewModel(RedbridgeTheme item)
		{
			return new TextCellViewModel(item.Name, CellIndicators.Disclosure)
			{
				Command = new Command(async () =>
				{
                    IsBusy = true;
                    await Task.Delay(1000);
                    IsBusy = false;
					RedbridgeThemeManager.SetTheme(item);
					var viewModel = _viewModelFactory.CreateModel<ThemeViewModel>();
					await Navigation.PushAsync(viewModel);
				})
			};
		}
	}
}
