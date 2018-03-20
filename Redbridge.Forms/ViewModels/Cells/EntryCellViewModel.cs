using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Redbridge.SDK;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public abstract class EntryCellViewModel<TValue> : EntryCellViewModel, ICellValue<TValue>
	{
		private BehaviorSubject<TValue> _valueSubject;

		protected BehaviorSubject<TValue> ValueSubject => _valueSubject;

		public EntryCellViewModel()
		{
			_valueSubject = new BehaviorSubject<TValue>(default(TValue));
		}

		public EntryCellViewModel(TValue value)
		{
			_valueSubject = new BehaviorSubject<TValue>(value);
		}

		public IObservable<TValue> Values
		{
			get { return _valueSubject; }
		}

		public TValue Value
		{
			get { return _valueSubject.Value; }
			set 
			{
				// Avoid round robin updates.
				if ( !value.Equals(_valueSubject.Value) )
					SetValue(value); 
			}
		}

		protected virtual void SetValue(TValue value)
		{
            // Should we validate here and prevent the value from being published?
			_valueSubject.OnNext(value);

			// Value has changed, so we need to update the text representation
			var text = ConvertToString(value);
			Text = text;

            ValidateCell();
		}

		protected virtual string ConvertToString (TValue value)
		{
			if ( !value.Equals(null) )
				return value.ToString();
			else
				return string.Empty;
		}
	}

	public abstract class EntryCellViewModel: TableCellViewModel, IEntryCellViewModel
	{
		private string _placeholderText;
        private Icon _placeholderIcon = Icon.None;
		private string _labelText;
		private bool _allowDirectEdit = true;
        private bool _isSecure = false;
		private string _text;
        private string _fieldName;
		private Keyboard _keyboard;
		private TextClearButtonMode _clearButtonMode;
		private AutoCapitalisationMode _capitalisationMode = AutoCapitalisationMode.None;
		private readonly BehaviorSubject<ValidationResult> _validationSubject = new BehaviorSubject<ValidationResult>(ValidationResult.Pass());

        protected EntryCellViewModel ()
        {
            _validationSubject.Subscribe((s) => OnPropertyChanged("IsValid"));
        }

		public virtual bool IsSecure
		{
			get { return _isSecure; }
			set
			{
				if (_isSecure != value)
				{
					_isSecure = value;
					OnPropertyChanged("IsSecure");
				}
			}
		}

		public virtual bool AllowTextEntry(int position, int length, string content)
		{
			return true;
		}

		public TextClearButtonMode ClearButtonMode
		{
			get { return _clearButtonMode; }
			set 
			{
				if (_clearButtonMode != value)
				{
					_clearButtonMode = value;
					OnPropertyChanged("ClearButtonMode");
				}
			}
			
		}

		public Keyboard Keyboard
		{ 
			get { return _keyboard; }
			set
			{
				if (_keyboard != value)
				{
					_keyboard = value;
					OnPropertyChanged("Keyboard");
				}
			}
		}

		public AutoCapitalisationMode AutoCapitalisationMode
		{
			get { return _capitalisationMode; }
			set
			{
				if (_capitalisationMode != value)
				{
					_capitalisationMode = value;
					OnPropertyChanged("AutoCapitalisationMode");
				}
			}
		}

		protected virtual void OnBeginEdit() {}

		public IObservable<ValidationResult> Validation 
		{
			get { return _validationSubject; }
		}

        public abstract ValidationResult Validate();
		
        protected void ValidateCell ()
        {
            var result = Validate();
            _validationSubject.OnNext(result);
        }

		public bool IsValid
		{
			get { return _validationSubject.Value.Success; }
		}

		public bool AllowDirectEdit
		{
			get { return _allowDirectEdit; }
			set
			{
				if (_allowDirectEdit != value)
				{
					_allowDirectEdit = value;
					OnPropertyChanged("AllowDirectEdit");
				}
			}
		}

		public string Text
		{
			get { return _text; }
			set
			{
				if (_text != value )
				{
					_text = value;
					OnTextChanged(value);
					OnPropertyChanged("Text");
				}
			}
		}

		protected abstract void OnTextChanged(string textValue);

        /// <summary>
        /// Gets or sets the label that is shown in front of the value on the UI.
        /// </summary>
        /// <value>The label.</value>
		public string Label
		{
			get { return _labelText; }
			set
			{
				if (_labelText != value)
				{
					_labelText = value;
					OnPropertyChanged("Label");
				}
			}
		}

        /// <summary>
        /// Gets or sets the text that is shown in the cell if there is no value currently set.
        /// </summary>
        /// <value>The placeholder text.</value>
		public string PlaceholderText
		{ 
			get { return _placeholderText; }
			set 
			{
				if (_placeholderText != value)
				{
					_placeholderText = value;
					OnPropertyChanged("PlaceholderText");
				}
			}
		}

        public Icon PlaceholderIcon
        {
            get { return _placeholderIcon; }
            set
            {
                if (_placeholderIcon != value)
                {
                    _placeholderIcon = value;
                    OnPropertyChanged("PlaceholderIcon");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the field which is shown in validation messages.
        /// </summary>
        /// <value>The name of the field.</value>
		public string FieldName
		{
			get { return _fieldName; }
			set
			{
				if (_fieldName != value)
				{
					_fieldName = value;
					OnPropertyChanged("FieldName");
				}
			}
		}
	}
}
