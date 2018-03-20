using System;
using System.Linq.Expressions;
using System.Reactive.Subjects;
using Redbridge.Validation;

namespace Redbridge.Forms
{
	public class DateTimeCellViewModel<TTarget> : DateTimeCellViewModel, ISavable
		where TTarget : class
	{
		private readonly PropertyValueManager<TTarget, DateTime?> _propertyValueSaver;

		public DateTimeCellViewModel(TTarget data, Expression<Func<TTarget, DateTime?>> propertySetter)
		{
			_propertyValueSaver = new PropertyValueManager<TTarget, DateTime?>(data, propertySetter);
			Value = _propertyValueSaver.GetValue();
		}

		public ValidationResultCollection TrySave()
		{
			return _propertyValueSaver.TrySave(ValueSubject.Value);
		}
	}


	public class DateTimeCellViewModel : EntryCellViewModel<DateTime?>, ICellValue<DateTime?>, ICellTextProvider, IEntryCellViewModel
	{
		private DateTime? _maximumDate;
		private DateTime? _minimumDate;
		private DatePickerStyle _pickerStyle = DatePickerStyle.Inline;
		private Subject<ValidationResult> _validationSubject = new Subject<ValidationResult>();
		private BehaviorSubject<bool> _pickerVisibility = new BehaviorSubject<bool>(false);
        private string _dateFormat = "dd/MM/yyyy";

		public DateTimeCellViewModel()
		{
            AllowCellSelection = true; // because it launches the picker.
            AllowDirectEdit = false;
			ClearButtonMode = TextClearButtonMode.Never;
		}

		public const string CellTypeName = "datetime";

		public override string CellType
		{
			get { return CellTypeName; }
		}

		public IObservable<bool> PickerVisibility
		{
			get { return _pickerVisibility; }
		}

        protected override void OnCellClicked()
        {
            base.OnCellClicked();
            ToggglePickerVisibility();
        }

		public void ToggglePickerVisibility()
		{
			var newValue = !PickerVisible;
			_pickerVisibility.OnNext(newValue);
		}

        protected override void OnTextChanged(string textValue) 
        {
            ValidateCell();
        }

        public bool PickerVisible
		{
			get { return _pickerVisibility.Value; }
		}

        protected override string ConvertToString(DateTime? value)
        {
            if ( value.HasValue )
            {
                return value.Value.ToString(_dateFormat);    
            }
            else
                return base.ConvertToString(value);
        }

        public string DateFormat
        {
            get { return _dateFormat; }
            set { _dateFormat = value; }
        }

		public DatePickerStyle PickerStyle
		{
			get { return _pickerStyle; }
			set
			{
				if (_pickerStyle != value)
				{
					_pickerStyle = value;
					OnPropertyChanged("PickerStyle");
				}
			}
		}

		public DateTime? MaximumDate
		{ 
			get { return _maximumDate; }
			set 
			{
				if (_maximumDate != value)
				{
					_maximumDate = value;
					OnPropertyChanged("MaximumDate");
				}
			}
		}

		public DateTime? MinimumDate
		{
			get { return _minimumDate; }
			set
			{
				if (_minimumDate != value)
				{
					_minimumDate = value;
					OnPropertyChanged("MinimumDate");
				}
			}
		}

        public override ValidationResult Validate()
        {
            var validator = new DateTimeValidator(MinimumDate, MaximumDate);
            return validator.Validate(Text);
        }
    }
}
