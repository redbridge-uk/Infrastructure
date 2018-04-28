using System;
using System.Collections.Specialized;
using System.Reactive.Linq;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Redbridge.Linq;

namespace Redbridge.Forms
{
	public class TableViewPage : ContentPage, IView
	{
		public static readonly BindableProperty TableProperty = BindableProperty.Create("Table", typeof(TableViewModel), typeof(TableViewPage), null, propertyChanged:OnTableChanged);
		private BusyPageConfigurationManager<TableViewModel> _pageManager;
		private readonly TableView _tableView;
		private TableViewModel _currentViewModel;
		private SearchBar _searchBar;
		private readonly ITableCellFactory _cellFactory;
		private Dictionary<TableSectionViewModel, TableSection> _tableSectionViewModelMap = new Dictionary<TableSectionViewModel, TableSection>();
		private Dictionary<TableSection, TableSectionViewModel> _tableSectionViewMap = new Dictionary<TableSection, TableSectionViewModel>();
		private Dictionary<object, TableSection> _tableCellCollectionSectionMap = new Dictionary<object, TableSection>();
		private Dictionary<ITableCellViewModel, Cell> _tableCellViewModelMap = new Dictionary<ITableCellViewModel, Cell>();
        private Dictionary<Cell, ITableCellViewModel> _tableCellReverseViewModelMap = new Dictionary<Cell, ITableCellViewModel>();

		public TableViewPage(ITableCellFactory cellFactory)
		{
            _cellFactory = cellFactory ?? throw new ArgumentNullException(nameof(cellFactory));
            _tableView = new TableView
            {
                Margin = new Thickness(0, -6, 0, 0)
            };
            _searchBar = new SearchBar
            {
                Margin = 0
            };
            _pageManager = new BusyPageConfigurationManager<TableViewModel>(this);

			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // Searchbar row.
			grid.RowDefinitions.Add(new RowDefinition(){ Height = GridLength.Star }); // Content row.
			grid.Children.Add(_searchBar, 0, 0);
			grid.Children.Add(_tableView, 0, 1);
			grid.Children.Add(_pageManager.ActivityIndicator, 0, 1);
			_pageManager.SetBusyHost(_tableView);

			Content = grid;
		}

		protected override void OnAppearing()
		{
			_currentViewModel?.OnAppearing();
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			_currentViewModel?.OnDisappearing();
			base.OnDisappearing();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

            if (this.BindingContext is ITableViewModel context)
            {
                this.SetBinding(TableProperty, "Table");
            }
        }

		public TableViewModel Table
		{
			get { return (TableViewModel)GetValue(TableProperty); }
			set { SetValue(TableProperty, value); }
		}

		static void OnTableChanged(BindableObject bindable, object oldValue, object newValue)
		{
            if (bindable is TableViewPage self)
            {
                if (newValue is TableViewModel tableViewModel)
                    self.SetupTable(tableViewModel);
            }
        }

		private void SetupTable(TableViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			if (_currentViewModel != null)
				DisconnectCurrentModel();

			ConnectViewModel(viewModel);
		}

		private void DisconnectCurrentModel()
		{
			_pageManager.Dispose();
			_currentViewModel = null;
			_tableView.Root.Clear();
			_tableSectionViewModelMap.Clear();
			_tableCellCollectionSectionMap.Clear();
			_tableSectionViewMap.Clear();
			_tableCellCollectionSectionMap.Clear();
            _tableCellViewModelMap.Clear();
            _tableCellReverseViewModelMap.Clear();
		}

		public IPageViewModel CurrentModel
		{
			get { return _currentViewModel; }
		}

		private void ConnectViewModel(TableViewModel viewModel)
		{
            _currentViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
			_pageManager.ConfigurePage(_currentViewModel);
            _tableView.HasUnevenRows = _currentViewModel.HasUnevenRows;
			_tableView.Intent = viewModel.Intent;
            _tableView.BackgroundColor = viewModel.BackgroundColour;

			SetupSearchbar(_currentViewModel.SearchBar);
			SetupSections(_currentViewModel.Sections);

			_pageManager.Disposibles.Add(Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>
			(
				h => _currentViewModel.Sections.CollectionChanged += h,
			 	h => _currentViewModel.Sections.CollectionChanged -= h
			).Subscribe((args) => 
			{
				if (args.EventArgs.Action == NotifyCollectionChangedAction.Add && args.EventArgs.NewItems != null)
				{
					var addedSections = args.EventArgs.NewItems.Cast<TableSectionViewModel>();
					addedSections.ForEach(svm => _tableView.Root.Add(CreateSectionView(svm)));
				}
			}));
		}

		public void SetupSearchbar(SearchBarViewModel searchBar)
		{
			_searchBar.SetBinding(IsVisibleProperty, "ShowSearchBar");
			_searchBar.SetBinding(SearchBar.PlaceholderProperty, "SearchBar.PlaceholderText");

			if (RedbridgeThemeManager.HasTheme)
			{
				//_searchBar.PlaceholderColor = RedbridgeThemeManager.Current.TintColour;
				_searchBar.CancelButtonColor = RedbridgeThemeManager.Current.TintColour;
			}
			else
			{
				_searchBar.SetBinding(SearchBar.PlaceholderColorProperty, "SearchBar.PlaceholderTextColor");
				_searchBar.SetBinding(SearchBar.CancelButtonColorProperty, "SearchBar.CancelButtonColor");
			}
		}

		private int VisibleIndexOf(ObservableCollection<TableSectionViewModel> sections, TableSectionViewModel section)
		{
			var actualIndex = sections.Where(s => s.IsVisible).ToList();
			return actualIndex.IndexOf(section);
		}

		private int VisibleIndexOf(ObservableCollection<ITableCellViewModel> cells, ITableCellViewModel cell)
		{
			var actualIndex = cells.Where(s => s.IsVisible).ToList();
			return actualIndex.IndexOf(cell);
		}

		private void SetupSections(ObservableCollection<TableSectionViewModel> sections)
		{
			_tableView.Root.Clear();
			sections.Where(s => s.IsVisible)
			        .ForEach((s) =>
			{
				var index = VisibleIndexOf(sections, s);
				AddSection(s, index);
			}); // Setup the currently visible sections.

			_pageManager.Disposibles.Add(Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>
				(
					h => sections.CollectionChanged += h,
					 h => sections.CollectionChanged -= h
				).Subscribe((args) => 
			{
				if (args.EventArgs.Action == NotifyCollectionChangedAction.Add)
				{
					foreach (TableSectionViewModel section in args.EventArgs.NewItems)
					{
						if (section.IsVisible)
						{
							var visibleIndex = VisibleIndexOf(sections, section);
							AddSection(section, visibleIndex);
						}
					}
				}
			}));

			// Regardless of visibility, check that we react to sections becoming visible.
			foreach (var section in sections)
			{
				_pageManager.Disposibles.Add(section.Visibility.Subscribe((visible) => 
				{
					var visibleIndex = VisibleIndexOf(sections, section);
					if (visible)
					{
						AddSection(section, visibleIndex);
					}
					else
						RemoveSection(section);
				})); 
			}
		}

		private void RemoveSection(TableSectionViewModel obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj));

			var view = _tableSectionViewModelMap[obj];
            foreach (var cell in obj.Cells)
            {
                RemoveCell(view, cell);
            }

			_tableCellCollectionSectionMap.Remove(obj.Cells);
			_tableSectionViewModelMap.Remove(obj);
			_tableSectionViewMap.Remove(view);
			_tableView.Root.Remove(view);
		}

        private void AddSection(TableSectionViewModel obj, int sectionIndex)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj));

			var view = CreateSectionView(obj);
			_tableSectionViewModelMap.Add(obj, view);
			_tableSectionViewMap.Add(view, obj);
			_tableCellCollectionSectionMap.Add(obj.Cells, view);

			if (_tableView.Root.Count == 0)
				_tableView.Root.Add(view);
			else 
			{
				// We need to insert somewhere.
				_tableView.Root.Insert(sectionIndex, view);
			}

			_pageManager.Disposibles.Add(Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>
			(
				h => obj.Cells.CollectionChanged += h,
				 h => obj.Cells.CollectionChanged -= h
			).Subscribe((args) =>
			{
					// The cell collection has changed, so we need to track where abouts this cell change needs to go...
					// Find the appropriate actual section that this relates by mapping from the source observable collection
					// to the applicable table section view model
					if (args.Sender != null)
				{
					// We should probably check that the section is actually visible before doing the cell add here
					// It is possible that things will be added to an invisible section before it is shown.
					var section = _tableCellCollectionSectionMap[args.Sender];
					var sectionViewModel = _tableSectionViewMap[section];

					if (args.EventArgs.Action == NotifyCollectionChangedAction.Add)
					{
						var cellModels = args.EventArgs.NewItems.Cast<TableCellViewModel>();
						cellModels.ForEach(cm => 
						{
							var index = VisibleIndexOf(sectionViewModel.Cells, cm); 
							AddCell(section, cm, index); 
						});
					}
                    else if ( args.EventArgs.Action == NotifyCollectionChangedAction.Remove)
                    {
						var cellModels = args.EventArgs.OldItems.Cast<TableCellViewModel>();
						cellModels.ForEach(cm =>
						{
							var index = VisibleIndexOf(sectionViewModel.Cells, cm);
                            RemoveCell(section, cm);
						});
                    }
                    else if (args.EventArgs.Action == NotifyCollectionChangedAction.Reset)
                    {
                        var itemsToRemove = new List<Cell>(section.ToArray());
                        foreach (var cell in itemsToRemove)
                        {
                            var cellModel = _tableCellReverseViewModelMap[cell];
                            RemoveCell(section, cellModel);
                        }
                    }
				}
			}));
		}

		private TableSection CreateSectionView(TableSectionViewModel svm)
		{
			TableSection section;

			if (!string.IsNullOrWhiteSpace(svm.Name))
				section = new TableSection(svm.Name);
			else
				section = new TableSection();

			foreach (var cell in svm.Cells)
			{
				_pageManager.Disposibles.Add(cell.Visibility.Subscribe((visible) =>
				{
					var visibleIndex = VisibleIndexOf(svm.Cells, cell);
					if (visible)
					{
						AddCell(section, cell, visibleIndex);
					}
					else
						RemoveCell(section, cell);
				}));
			}

			svm.Cells.Where(cv => cv.IsVisible).ForEach((c) => 
			{
				var visibleIndex = VisibleIndexOf(svm.Cells, c);
				AddCell(section, c, visibleIndex);
			});

    
			return section;
		}

		private void AddCell(TableSection section, ITableCellViewModel cell, int index)
		{
			var cellView = _cellFactory.CreateCellView(cell);
			_tableCellViewModelMap.Add(cell, cellView);
            _tableCellReverseViewModelMap.Add(cellView, cell);
			section.Insert(index, cellView);
		}

		private void RemoveCell(TableSection section, ITableCellViewModel cell)
		{
			var cellView = _tableCellViewModelMap[cell];
			_tableCellViewModelMap.Remove(cell);
            _tableCellReverseViewModelMap.Remove(cellView);
			section.Remove(cellView);
		}
	}
}
