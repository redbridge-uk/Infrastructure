using System;
using System.Linq.Expressions;
using System.Reactive.Subjects;
using System.Reflection;
using Redbridge.SDK;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class NullableSwitchCellViewModel<TTarget> : NullableSwitchCellViewModel, ISavable
		where TTarget: class
	{
		private readonly PropertyValueManager<TTarget, bool?> _propertyValueSaver;

		public NullableSwitchCellViewModel(TTarget data, Expression<Func<TTarget, bool?>> propertySetter)
		{
			_propertyValueSaver = new PropertyValueManager<TTarget, bool?>(data, propertySetter);
			var nullableValue = _propertyValueSaver.GetValue();

			if ( nullableValue.HasValue )
				Value = nullableValue.Value;
		}

		public ValidationResultCollection TrySave()
		{
			return _propertyValueSaver.TrySave(Value);
		}
	}

	public class NullableSwitchCellViewModel : TableCellViewModel
	{
		public const string CellTypeName = "nullableswitch";

		private string _label;
		private bool? _value = false;
		private bool _clickAnywhereToToggle = false;
		private Color _labelTextColour = Color.Black;
		private readonly BehaviorSubject<bool?> _valueSubject = new BehaviorSubject<bool?>(null);

		public NullableSwitchCellViewModel() 
		{
			AllowCellSelection = false;
			ClickAnywhereToToggle = true;
		}

		public NullableSwitchCellViewModel(string label, bool? initialValue = null) : this()
		{
			Label = label;
			Value = initialValue.HasValue ? initialValue.Value : false;
		}

		public override string CellType
		{
			get { return CellTypeName; }
		}

		public IObservable<bool?> ValueChanged
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

		public bool Value
		{
			get { return _value.HasValue ? _value.Value : false; }
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

		public Color LabelTextColour
		{
			get { return _labelTextColour; }
			set
			{
				if (_labelTextColour != value)
				{
					_labelTextColour = value;
					OnPropertyChanged("LabelTextColour");
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
