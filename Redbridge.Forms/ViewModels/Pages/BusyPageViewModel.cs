using System;
using System.Reactive.Subjects;
using Redbridge.SDK;
using Xamarin.Forms;
using System.Reactive.Linq;
using Redbridge.Validation;

namespace Redbridge.Forms
{
	public abstract class BusyPageViewModel : PageViewModel, IBusyPageViewModel
	{
		private bool _isBusy = false;
		private Color _busyColor = RedbridgeThemeManager.HasTheme ? RedbridgeThemeManager.Current.BusyIndicatorColour : RedbridgeTheme.Default.BusyIndicatorColour;
		private BehaviorSubject<bool> _busySubject = new BehaviorSubject<bool>(false);

		public BusyPageViewModel(ISchedulerService scheduler) : base(scheduler) 
		{
            if (RedbridgeThemeManager.HasTheme)
			{
				BusyIndicatorColour = RedbridgeThemeManager.Current.BusyIndicatorColour;
			}

			AddToDisposables(RedbridgeThemeManager.Theme.Where(rt => rt != null)
								 .ObserveOn(Scheduler.UiScheduler)
								 .Subscribe(rt => 
			{
				BusyIndicatorColour = rt.BusyIndicatorColour;
			}));
		}

        public IObservable<bool> Busy 
		{ 
			get { return _busySubject; }
		}

		public Color BusyIndicatorColour
		{
			get { return _busyColor; }
			set
			{
				if (_busyColor != value)
				{
					_busyColor = value;
					OnPropertyChanged("BusyIndicatorColour");
				}
			}
		}

		public bool IsBusy
		{
			get { return _isBusy; }
			set 
			{
				if (_isBusy != value)
				{
					_isBusy = value;
					OnPropertyChanged("IsBusy");
					_busySubject.OnNext(_isBusy);
				}
			}
		}

		protected BusyOperation BeginBusyOperation()
		{
            return new BusyOperation(this);
		}

        protected override void OnDispose()
        {
            base.OnDispose();
            _busySubject.OnCompleted();
            _busySubject.Dispose();
        }
    }
}
