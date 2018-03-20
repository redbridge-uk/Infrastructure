using System;
using System.Collections.ObjectModel;
using Redbridge.SDK;
using Xamarin.Forms;
using System.Reactive.Linq;
using Redbridge.Forms.Markup;
using Redbridge.Validation;

namespace Redbridge.Forms
{
    [View(typeof(TableViewPage))]
    public class TableViewModel : NavigationPageViewModel, ITableViewModel
	{
		private bool _isFocused;
		private bool _pullToRefreshEnabled = true;
		private TableCellSeparatorStyle _separatorStyle;
		private Color _tintColour;

		public TableViewModel(INavigationService navigationService, ISchedulerService schedulerService) : base(navigationService, schedulerService)
		{
			Sections = new ObservableCollection<TableSectionViewModel>();
			Intent = TableIntent.Data;
			SeparatorStyle = TableCellSeparatorStyle.SingleLine;
			ShowNavigationBar = true;
			PullToRefreshEnabled = true;
			if (RedbridgeThemeManager.HasTheme)
			{
				TintColour = RedbridgeThemeManager.Current.TintColour;
			}
			RedbridgeThemeManager.Theme
			                     .Where(rt => rt != null)
			                     .ObserveOn(Scheduler.UiScheduler)
								 .Subscribe((rb) => 
			{
				TintColour = rb.TintColour;
			});
		}

        public virtual bool Editable => false;

		protected virtual TableSectionViewModel AddSection(string name, bool showHeader = true, bool isVisible = true)
		{
			var section = new TableSectionViewModel(name, showHeader) { IsVisible = isVisible };
			Sections.Add(section);
			return section;
		}

		protected virtual TableSectionViewModel AddSection()
		{
			return AddSection(new TableSectionViewModel());
		}

		protected virtual TableSectionViewModel AddSection(TableSectionViewModel section)
		{
			if (section == null) throw new ArgumentNullException(nameof(section));
			Sections.Add(section);
			return section;
		}

		public void RefreshTable()
		{
			OnRefreshTable();
		}

		protected virtual void OnRefreshTable() {}

        public bool IsFocused
		{
			get { return _isFocused; }
			set 
			{
				if (value != _isFocused)
				{
					_isFocused = value;
					OnPropertyChanged("IsFocused");
				}
			}
		}

		public Color TintColour
		{
			get { return _tintColour; }
			set
			{
				if (value != _tintColour)
				{
					_tintColour = value;
					OnPropertyChanged("TintColour");
				}
			}
		}

		public bool PullToRefreshEnabled
		{
			get { return _pullToRefreshEnabled; }
			set
			{
				if (value != _pullToRefreshEnabled)
				{
					_pullToRefreshEnabled = value;
					OnPropertyChanged("PullToRefreshEnabled");
				}
			}
		}

		public TableIntent Intent
		{
			get;
			set;
		}

		public ObservableCollection<TableSectionViewModel> Sections { get; private set; }

		public ITableViewModel Table { get { return this; } }

		public TableCellSeparatorStyle SeparatorStyle
		{
			get { return _separatorStyle; }
			set
			{
				if (value != _separatorStyle)
				{
					_separatorStyle = value;
					OnPropertyChanged("SeparatorStyle");
				}
			}
		}

		protected override ValidationResultCollection OnValidate()
		{
            var results = new ValidationResultCollection();

            foreach (var section in Sections)
            {
                results.AddResult(section.Validate());
            }

            return results;
		}
	}
}
