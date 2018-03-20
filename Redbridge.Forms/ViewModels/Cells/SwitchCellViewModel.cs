using System;
using System.Linq.Expressions;
using System.Reactive.Subjects;
using Redbridge.SDK;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class SwitchCellViewModel<TTarget> : SwitchCellViewModel, ISavable
		where TTarget: class
	{
		private readonly PropertyValueManager<TTarget, bool> _propertyValueSaver;

		public SwitchCellViewModel(TTarget data, Expression<Func<TTarget, bool>> propertySetter)
		{
			_propertyValueSaver = new PropertyValueManager<TTarget, bool>(data, propertySetter);
			Value = _propertyValueSaver.GetValue();
		}

		public ValidationResultCollection TrySave()
		{
			return _propertyValueSaver.TrySave(Value);
		}
	}

	public class SwitchCellViewModel : TableCellViewModel
	{
		public const string CellTypeName = "switch";

		private string _label;
		private bool _value = false;
		private bool _clickAnywhereToToggle = false;
        private Color _switchColour;
		private readonly BehaviorSubject<bool> _valueSubject = new BehaviorSubject<bool>(false);

		public SwitchCellViewModel() 
		{
			AllowCellSelection = false;
			ClickAnywhereToToggle = true;
		}

		public SwitchCellViewModel(string label, bool initialValue) : this()
		{
			Label = label;
			Value = initialValue;
		}

		public override string CellType
		{
			get { return CellTypeName; }
		}

		public IObservable<bool> ValueChanged
		{
			get { return _valueSubject; }
		}

		public void Toggle()
		{
			Value = !Value;
		}

		protected override void OnCellClicked()
		{
			if (ClickAnywhereToToggle)
				Toggle();
			else
				base.OnCellClicked();
		}

		public Color SwitchColour
		{
			get { return _switchColour; }
			set
			{
				if (_switchColour != value)
				{
					_switchColour = value;
					OnPropertyChanged("SwitchColour");
				}
			}
		}

		public bool Value
		{
			get { return _value; }
			set
			{
				if (_value != value)
				{
					_value = value;
					_valueSubject.OnNext(value);
					OnPropertyChanged("Value");
				}
			}
		}

		public string Label
		{
			get { return _label; }
			set
			{
				if (_label != value)
				{
					_label = value;
					OnPropertyChanged("Label");
				}
			}
		}

		public bool ClickAnywhereToToggle
		{
			get { return _clickAnywhereToToggle; }
			set
			{
				if (_clickAnywhereToToggle != value)
				{
					_clickAnywhereToToggle = value;
					OnPropertyChanged("ClickAnywhereToToggle");
				}
			}
		}
	}
}
