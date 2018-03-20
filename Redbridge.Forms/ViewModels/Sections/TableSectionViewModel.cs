using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Subjects;
using Redbridge.Forms.ViewModels.Cells;
using Redbridge.Linq;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class TableSectionViewModel : ViewModel
	{
		private string _name;
		private bool _showHeader = true;
		private Subject<bool> _visibilitySubject = new Subject<bool>();
        private BehaviorSubject<bool> _editMode = new BehaviorSubject<bool>(false);
        private bool _isVisible = true;
        private DatePickerCellViewModel _datePickerCellViewModel = new DatePickerCellViewModel(); // This may need to move up to the table view level as we probably only ever want one 

		public TableSectionViewModel()
		{
			Cells = new ObservableCollection<ITableCellViewModel>();
			Cells.CollectionChanged += (sender, e) => OnPropertyChanged("HasCells");
            HideWhenNoCellsVisible = false;
            _editMode.Subscribe((e) => Cells.ForEach(c => c.BeginEdit()));
		}

		public TableSectionViewModel(string name, bool showHeader = true, params ITableCellViewModel [] cellModels) : this()
		{
			Name = name;
			ShowHeader = showHeader;

			if (cellModels != null)
			{
				cellModels.ForEach(Cells.Add);
			}
		}

        public bool EditMode
        {
            get { return _editMode.Value; }
            set 
            {
                if ( value != _editMode.Value )
                    _editMode.OnNext(value);
            }
        }

		public ValidationResultCollection Validate()
		{
			var results = new ValidationResultCollection();

			foreach (var cell in Cells)
			{
				var validatableCell = cell as IEntryCellViewModel;
				if (validatableCell != null)
				{
					results.AddResult(validatableCell.Validate());
				}
			}

			return results;
		}

		public IObservable<bool> Visibility
		{
			get { return _visibilitySubject; }
		}

		public bool HasCells
		{
			get { return Cells.Any(); }
		}

        public bool Contains (ITableCellViewModel cell)
        {
            return Cells.Contains(cell);
        }

		public TCell AddCell<TCell>(TCell cell) where TCell : class, ITableCellViewModel
		{
            if (cell == null) throw new ArgumentNullException(nameof(cell));

            var dateCell = cell as DateTimeCellViewModel;
            if ( dateCell != null )
            {
                if ( dateCell.PickerStyle == DatePickerStyle.Inline )
                {
                    dateCell.PickerVisibility.Subscribe(pv => 
                    {
                        _datePickerCellViewModel.SetParentHost(dateCell);

						if (Contains(_datePickerCellViewModel))
							Remove(_datePickerCellViewModel);

                        if (pv)
                        {
                            var parentDatePickerCellIndex = Cells.IndexOf(dateCell);
                            Cells.Insert(parentDatePickerCellIndex + 1, _datePickerCellViewModel);
                        }
                    });
                }
            }

            Cells.Add(cell);

            if ( _editMode.Value)
                cell.BeginEdit();

			return cell;
		}

        public void Remove (ITableCellViewModel cell)
        {
            Cells.Remove(cell);
        }

        public void Clear ()
        {
            Cells.Clear();
        }

        public IconTextCellViewModel AddIconCell(Icon icon, string text, string detail = "", CellIndicators indicators = CellIndicators.None)
        {
            var cell = new IconTextCellViewModel(icon, text, detail, indicators);
            return AddCell(cell);
        }

        public TextCellViewModel AddTextCell (string text, string detail = "", CellIndicators indicators = CellIndicators.None)
        {
            var cell = new TextCellViewModel(text, detail, indicators);
            return AddCell(cell);
        }

		public DateTimeCellViewModel<T> AddDateTimeCell<T>(T data, Expression<Func<T, DateTime?>> property, string label, string placeholder = "", Icon placeholderIcon = Icon.None, string dateFormat = "dd/MM/yyyy", CellIndicators indicators = CellIndicators.None)
			where T : class
		{
			var cell = new DateTimeCellViewModel<T>(data, property)
			{
				Label = label,
				PlaceholderText = placeholder,
                PlaceholderIcon = placeholderIcon,
				Accessories = indicators,
                DateFormat = dateFormat,
			};

            return AddCell(cell);
		}

        public TextCellViewModel<T> AddTextCell<T>(T data, Expression<Func<T, string>> property, string text, CellIndicators indicators = CellIndicators.None)
            where T : class
        {
            var cell = new TextCellViewModel<T>(data, property)
            {
                Text = text,
                Accessories = indicators,
            };

            return AddCell(cell);
        }

		public TextEntryCellViewModel<T> AddTextEntryCell<T>(T data, Expression<Func<T, string>> property, string label, string placeholder = "", Icon placeholderIcon = Icon.None, CellIndicators indicators = CellIndicators.None)
            where T: class
		{
            var cell = new TextEntryCellViewModel<T>(data, property) 
            {
                Label = label,
                PlaceholderText = placeholder,
                Accessories = indicators,
                PlaceholderIcon = placeholderIcon,
            };

            return AddCell(cell);
        }

		public SwitchCellViewModel<T> AddSwitchCell<T>(T data, Expression<Func<T, bool>> property, string label, CellIndicators indicators = CellIndicators.None)
			where T : class
		{
			var cell = new SwitchCellViewModel<T>(data, property)
			{
				Label = label,
				Accessories = indicators,
			};

			return AddCell(cell);
		}

		public ObservableCollection<ITableCellViewModel> Cells
		{
			get;
		}

		public bool ShowHeader
		{
			get { return _showHeader; }
			set
			{
				if (_showHeader != value)
				{
					_showHeader = value;
					OnPropertyChanged("Name");
				}
			}
		}

        public BusyCellViewModel AddBusyCell(string text = "Loading")
        {
            var cell = new BusyCellViewModel(text);
            return AddCell(cell);
        }

        public void BeginEdit()
        {
            _editMode.OnNext(true);
        }

        public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				if (_isVisible != value)
				{
					_visibilitySubject.OnNext(value);
					OnPropertyChanged("IsVisible");
				}
			}
		}

        public bool HideWhenNoCellsVisible
        {
            get; set;
        }

		public string Name
		{
			get 
			{
                if (_showHeader)
                    return _name;
                
                return null;
            }
			set 
			{
				if (_name != value)
				{
					_name = value;
					OnPropertyChanged("Name");
				}
			}
		}
	}
}
