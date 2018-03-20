using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Redbridge.SDK;

namespace Redbridge.Forms
{
	public abstract class WizardControllerViewModel : TabbedViewModel
	{
		private List<IWizardPageViewModel> _pages = new List<IWizardPageViewModel>();
		private BehaviorSubject<IWizardPageViewModel> _current;
		private IViewModelFactory _viewModelFactory;

		public WizardControllerViewModel(IViewModelFactory viewModelFactory, INavigationService navigationService, ISchedulerService scheduler) : base(navigationService, scheduler) 
		{
			if (viewModelFactory == null) throw new ArgumentNullException(nameof(viewModelFactory));
			_viewModelFactory = viewModelFactory;
		}

		protected IViewModelFactory ViewModelFactory => _viewModelFactory;

		protected virtual void AddPage(IWizardPageViewModel page)
		{
			if (page == null) throw new ArgumentNullException(nameof(page));
			_pages.Add(page);
		}

		protected virtual void AddPageRange(IWizardPageViewModel [] pages)
		{
			if (pages == null) throw new ArgumentNullException(nameof(pages));
			_pages.AddRange(pages);
		}

		public IEnumerable<IWizardPageViewModel> PageModels => _pages;

		public IObservable<IWizardPageViewModel> Current
		{
			get 
			{
				if (_current == null) throw new NotSupportedException("You must start the wizard first.");
				return _current; 
			}
		}

		public void StartWizard()
		{
			var firstPage = _pages.FirstOrDefault();
			_current = new BehaviorSubject<IWizardPageViewModel>(firstPage);
		}

		public int GetPageIndex(IWizardPageViewModel vm)
		{
			return _pages.IndexOf(vm);
		}
	}
}
