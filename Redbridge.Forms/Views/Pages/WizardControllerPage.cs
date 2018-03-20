using System;
using System.Reactive.Linq;
using Redbridge.SDK;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class WizardControllerPage : TabbedPage, IView
	{
		private WizardControllerViewModel _currentViewModel;
		private readonly ISchedulerService _scheduler;
		private readonly IViewFactory _viewFactory;

		public ISchedulerService Scheduler => _scheduler;

		public WizardControllerPage(ISchedulerService scheduler, IViewFactory viewFactory)
		{
			if (viewFactory == null) throw new ArgumentNullException(nameof(viewFactory));
			if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
			_scheduler = scheduler;
			_viewFactory = viewFactory;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var wizardViewModel = this.BindingContext as WizardControllerViewModel;
			if (wizardViewModel != null)
			{
				SetupWizard(wizardViewModel);
			}
		}

		static void OnWizardChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var self = bindable as WizardControllerPage;
			if (self != null)
			{
				var wizardViewModel = newValue as WizardControllerViewModel;
				if (wizardViewModel != null)
					self.SetupWizard(wizardViewModel);
			}
		}

		private void SetupWizard(WizardControllerViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			if (_currentViewModel != null)
				DisconnectCurrentModel();

			ConnectViewModel(viewModel);
		}

		void ConnectViewModel(WizardControllerViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			_currentViewModel = viewModel;
			_currentViewModel.StartWizard();

			foreach (var pageViewModel in viewModel.PageModels)
			{
				var pageView = _viewFactory.CreatePage(pageViewModel);
				Children.Add(pageView);
			}

			_currentViewModel.Current.ObserveOn(Scheduler.UiScheduler).Subscribe((vm) => 
			{
				var viewIndex = _currentViewModel.GetPageIndex(vm);
				var view = Children[viewIndex];
				CurrentPage = view;
			});
		}

		void DisconnectCurrentModel()
		{
		}
	}
}
