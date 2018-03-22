using System;
using System.Reactive.Linq;
using Redbridge.SDK;
using Redbridge.Forms;
using Xamarin.Forms;

namespace TesterApp
{
	public class CategoryPickerViewModel : ItemPickerTableViewModel<CategoryData>
	{
		readonly IViewModelFactory _viewModelFactory;

		public CategoryPickerViewModel(IViewModelFactory viewModelFactory, INavigationService navigationService, ISchedulerService scheduler) : base(new InMemoryCategorySource(), navigationService, scheduler)
		{
			if (viewModelFactory == null) throw new ArgumentNullException(nameof(viewModelFactory));
			_viewModelFactory = viewModelFactory;
			Title = "Choose category";
			Intent = TableIntent.Form;
		}

		protected override void OnViewAppearing()
		{
			base.OnViewAppearing();
		}

		protected override ITableCellViewModel CreateItemViewModel(CategoryData item)
		{
			return new ImageCellViewModel(item.Name, item.ImageSource, CellIndicators.Disclosure)
			{
				Command = new Command(() =>
				{
					IsBusy = true;

					Observable.Interval(TimeSpan.FromSeconds(2), Scheduler.BackgroundScheduler).Take(1)
							  .ObserveOn(Scheduler.UiScheduler)
					          .Subscribe((l) => 
					{ 
						IsBusy = false; 
						var viewModel = _viewModelFactory.CreateModel<IntegrationViewModel>();
						Navigation.PushAsync(viewModel);
					});
				})
			};
		}
	}
}
