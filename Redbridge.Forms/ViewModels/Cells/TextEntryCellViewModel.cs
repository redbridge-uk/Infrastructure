using System;
using System.Linq.Expressions;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class TextEntryCellViewModel<TTarget> : TextEntryCellViewModel, ISavable
		where TTarget : class
	{
		private readonly PropertyValueManager<TTarget, string> _propertyValueSaver;

		public TextEntryCellViewModel(TTarget data, Expression<Func<TTarget, string>> propertySetter)
		{
			_propertyValueSaver = new PropertyValueManager<TTarget, string>(data, propertySetter);
			Text = _propertyValueSaver.GetValue();
		}

		public ValidationResultCollection TrySave()
		{
			return _propertyValueSaver.TrySave(ValueSubject.Value);
		}
	}

	public class TextEntryCellViewModel : EntryCellViewModel<string>
	{
		private int? _maximumLength;
		private int? _minimumLength;
        //private bool _autoCapitalise = false;

		public TextEntryCellViewModel()
		{
			Keyboard = Keyboard.Text;
			AllowCellSelection = false;
			ClearButtonMode = TextClearButtonMode.WhilstEditing;
		}

		public const string CellTypeName = "textentry";

		protected override void OnTextChanged(string textValue)
		{
            //if (AutoCapitalise)
            //    Value = textValue?.ToUpperInvariant();
            //else
			    Value = textValue;
		}

		public override string CellType
		{
			get { return CellTypeName; }
		}

        public override ValidationResult Validate()
        {
            var stringValidator = new StringValidator(MinimumLength, MaximumLength);
            return stringValidator.Validate(Text);
        }

        //public bool AutoCapitalise
        //{
        //    get { return _autoCapitalise; }
        //    set
        //    {
        //        if (_autoCapitalise != value)
        //        {
        //            _autoCapitalise = value;
        //            OnPropertyChanged("AutoCapitalise");
        //        }
        //    }
        //}

		public int? MaximumLength
		{ 
			get { return _maximumLength; }
			set 
			{
				if (_maximumLength != value)
				{
					_maximumLength = value;
					OnPropertyChanged("MaximumLength");
				}
			}
		}

		public int? MinimumLength
		{
			get { return _minimumLength; }
			set
			{
				if (_minimumLength != value)
				{
					_minimumLength = value;
					OnPropertyChanged("MinimumLength");
				}
			}
		}
	}
}
