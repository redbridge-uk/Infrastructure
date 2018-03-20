using System;
using System.Linq.Expressions;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class NumericEntryCellViewModel<TTarget> : NumericEntryCellViewModel, ISavable
		where TTarget : class
	{
		private readonly PropertyValueManager<TTarget, decimal?> _propertyValueSaver;

		public NumericEntryCellViewModel(TTarget data, Expression<Func<TTarget, decimal?>> propertySetter)
		{
			_propertyValueSaver = new PropertyValueManager<TTarget, decimal?>(data, propertySetter);
			var nullableValue = _propertyValueSaver.GetValue();
			SetValue(nullableValue);
		}

		public ValidationResultCollection TrySave()
		{
			return _propertyValueSaver.TrySave(ValueSubject.Value);
		}
	}

	public class NumericEntryCellViewModel : EntryCellViewModel<decimal?>
	{
		private decimal? _maximumValue;
		private decimal? _minimumValue;
		private bool _isInteger = true;
		private bool _isSigned = false;

		public NumericEntryCellViewModel() : this(null) {}

		public NumericEntryCellViewModel(decimal? initialValue) : base(initialValue)
		{
			Keyboard = Keyboard.Numeric;
			ClearButtonMode = TextClearButtonMode.WhilstEditing;
			IsInteger = true;
			AllowCellSelection = false;
		}

		public const string CellTypeName = "numericentry"; 

		public override string CellType
		{
			get { return CellTypeName; }
		}

		public override ValidationResult Validate()
		{
            var validator = new DecimalValidator(MinimumValue, MaximumValue);
            return validator.Validate(Text);
		}

		public override bool AllowTextEntry (int position, int length, string content)
		{
			bool permit = true;
			bool alreadyHasDecimal = !string.IsNullOrWhiteSpace(Text) && Text.Contains(".");
			bool alreadyHasMinus = !string.IsNullOrWhiteSpace(Text) && Text.Contains("-");

			if (content != null)
			{
				foreach (var character in content)
				{
					if (!IsInteger && character == '.' && !alreadyHasDecimal)
						return true;

					if (IsSigned)
					{
						if (alreadyHasMinus && !char.IsDigit(character))
							return false;
						
						return character == '-' || char.IsDigit(character);
					}
					else
					{
						if (!char.IsDigit(character))
							return false;
					}
				}
			}

			return permit;
		}

		protected override string ConvertToString (decimal? value)
		{
			if (Value.HasValue)
			{
				return Value.ToString();
			}

			return string.Empty;
		}

		protected override void OnTextChanged (string textValue)
		{
			decimal decimalValue;
			if (decimal.TryParse(textValue, out decimalValue))
			{
				Value = decimalValue;
			}
			else
				Value = null;

            ValidateCell();
		}

		public bool IsInteger
		{
			get { return _isInteger; }
			set
			{
				if (_isInteger != value)
				{
					_isInteger = value;
					OnPropertyChanged("IsInteger");
				}
			}
		}

		public bool IsSigned
		{
			get { return _isSigned; }
			set
			{
				if (_isSigned != value)
				{
					_isSigned = value;
					OnPropertyChanged("IsSigned");
				}
			}
		}

		public decimal? MaximumValue
		{ 
			get { return _maximumValue; }
			set 
			{
				if (_maximumValue != value)
				{
					_maximumValue = value;
					OnPropertyChanged("MaximumValue");
				}
			}
		}

		public decimal? MinimumValue
		{
			get { return _minimumValue; }
			set
			{
				if (_minimumValue != value)
				{
					_minimumValue = value;
					OnPropertyChanged("MinimumValue");
				}
			}
		}
	}
}
