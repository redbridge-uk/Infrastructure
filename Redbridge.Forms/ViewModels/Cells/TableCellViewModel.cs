using System;
using System.ComponentModel;
using System.Reactive.Subjects;
using System.Windows.Input;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public abstract class TableCellViewModel : ITableCellViewModel
	{
		private bool _isVisible = true;
		private bool _isEnabled = true;
        private bool _editMode = false;
        private Color _backgroundColour;
        private TableCellHeight _cellHeight;
        private BehaviorSubject<string> _cellTypeObservable;
		private CellIndicators _accessories = CellIndicators.None;
		private Subject<bool> _visibilitySubject = new Subject<bool>();

		public event PropertyChangedEventHandler PropertyChanged;

		public TableCellViewModel()
		{
			AllowCellSelection = true;
            _cellTypeObservable = new BehaviorSubject<string>(CellType);
            _backgroundColour = Color.Transparent;
            RedbridgeThemeManager.Theme.Subscribe(rt => OnThemeChanged(rt));
		}

        private void OnThemeChanged(RedbridgeTheme rt)
        {
            
        }

        public TableCellViewModel(CellIndicators accessories) : this()
		{
			Accessories = accessories;
		}

        public IObservable<string> CellTypeObservable => _cellTypeObservable;

        /// <summary>
        /// Gets/sets whether a cell can be selected, in the sense that the cell is highlighted when clicked.
        /// </summary>
        /// <value><c>true</c> if allow cell selection; otherwise, <c>false</c>.</value>
		public bool AllowCellSelection
		{
			get; 
			set;
		}

        public bool EditMode => _editMode;

        public void BeginEdit ()
        {
            _editMode = true;
            OnEnteringEdit();
        }

        protected virtual void OnEnteringEdit()
        {
        }

        public void EndEdit ()
        {
            _editMode = false;
            OnLeavingEdit();
        }

        protected virtual void OnLeavingEdit()
        {
        }

        /// <summary>
        /// Gets or sets the accessories shown against the cell, e.g. a right arrow in iOS or an info button or both..
        /// </summary>
        /// <value>The accessories.</value>
        public CellIndicators Accessories
		{
			get { return _accessories; }
			set 
			{
				if (_accessories != value)
				{
					_accessories = value;
					OnPropertyChanged("Accessories");
				}
			}
		}

		public IObservable<bool> Visibility
		{
			get { return _visibilitySubject; }
		}

		public void Enable()
		{
			IsEnabled = true;
		}

		public void Disable()
		{
			IsEnabled = false;
		}

		public void CellClicked()
		{
			OnCellClicked();
		}

        /// <summary>
        /// Default triggers an associated command against the cell.
        /// </summary>
		protected virtual void OnCellClicked() 
		{
			if (Command != null && Command.CanExecute(null) )
			{
				Command.Execute(CommandParameter);
			}
		}

		public ICommand Command { get; set; }

        public object CommandParameter 
        {
            get; set;
        }

		public void OnPropertyChanged(string propertyName)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

		public int CompareTo(ITableCellViewModel other)
		{
			return 0;
		}

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				if (_isEnabled != value)
				{
					_isEnabled = value;
					OnPropertyChanged("IsEnabled");
				}
			}
		}

        public Color BackgroundColour
        {
            get { return _backgroundColour; }
            set
            {
                if (_backgroundColour != value)
                {
                    _backgroundColour = value;
                    OnPropertyChanged("BackgroundColour");
                }
            }
        }

        public TableCellHeight CellHeight
        {
            get { return _cellHeight; }
            set
            {
                if (_cellHeight != value)
                {
                    _cellHeight = value;
                    OnPropertyChanged("TableCellHeight");
                }
            }
        }

		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				if (_isVisible != value)
				{
					_isVisible = value;
					_visibilitySubject.OnNext(value);
					OnPropertyChanged("IsVisible");
				}
			}
		}

        /// <summary>
        /// Gets the type of the cell used by the cell factory and the container to generate the correct type of visual cell.
        /// </summary>
        /// <value>The type of the cell.</value>
		public abstract string CellType { get; }
	}
}
