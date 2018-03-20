using System;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Redbridge.Validation;

namespace Redbridge.Forms
{
	public class ActionSheetCellViewModel<TTarget, TEnum> : ActionSheetCellViewModel<TEnum>, ISavable
		where TTarget : class
	{
		private readonly PropertyValueManager<TTarget, TEnum> _propertyValueSaver;

		public ActionSheetCellViewModel (TTarget data, 
		                                 Expression<Func<TTarget, TEnum>> propertySetter,
		                                 IActionSheetController sheetController) : this(data, propertySetter, sheetController, (arg) => 
		{
			if (arg.Result == ActionSheetButtonResponse.Option)
				return (TEnum)arg.Option.Value;

			return default(TEnum);
		}) 
		{
			
		}

		public ActionSheetCellViewModel(TTarget data, Expression<Func<TTarget, TEnum>> propertySetter, 
		                                IActionSheetController sheetController, 
		                                Func<ActionSheetResponse, TEnum> converter)
			: base(sheetController, converter)
		{
			_propertyValueSaver = new PropertyValueManager<TTarget, TEnum>(data, propertySetter);
			SetValue(_propertyValueSaver.GetValue());
		}

		public ValidationResultCollection TrySave()
		{
			return _propertyValueSaver.TrySave(Value);
		}
	}

	public abstract class ActionSheetCellViewModel : EntryCellViewModel
	{
		public override string CellType
		{
			get { return CellTypeName; }
		}

		public const string CellTypeName = "actionsheet";
	}

	public class ActionSheetCellViewModel<TEnum> : ActionSheetCellViewModel
	{
		readonly IActionSheetController _sheetController;
		private ActionSheetResponse _value;
		private readonly Func<ActionSheetResponse, TEnum> _converter;
		private readonly BehaviorSubject<TEnum> _valueSubject= new BehaviorSubject<TEnum>(default(TEnum));

		public ActionSheetCellViewModel(IActionSheetController sheetController, CellIndicators indicators = CellIndicators.Disclosure)
			: this(sheetController, (arg) => 
		{
			if (arg.Result == ActionSheetButtonResponse.Option)
				return (TEnum)arg.Option.Value;
			
			return default(TEnum);
		}, indicators){}

		public ActionSheetCellViewModel(IActionSheetController sheetController, 
		                                Func<ActionSheetResponse, TEnum> converter, 
		                                CellIndicators indicators = CellIndicators.Disclosure)
		{
			if (converter == null) throw new ArgumentNullException(nameof(converter));
			if (sheetController == null) throw new ArgumentNullException(nameof(sheetController));
			_sheetController = sheetController;
			_converter = converter;
			_sheetController.Response.Where(r => r.Owner == this).Subscribe(sc =>
			{
				SetValue(sc);
			});
			Accessories = indicators;
			AllowDirectEdit = false;
		}

		public ActionSheetCellViewModel(IActionSheetController sheetController, 
		                                Func<ActionSheetResponse, TEnum> converter,
										string label, 
		                                CellIndicators indicators = CellIndicators.Disclosure) : this(sheetController, converter, indicators)
		{
			Label = label;
		}

		protected void SetValue(TEnum value)
		{
			SetValue(new ActionSheetResponse(ActionSheetButtonResponse.Option, this) 
			{
				Option = ActionSheetOption.FromOption(value),
				Owner = this,
			});
		}

		private void SetValue(ActionSheetResponse response)
		{
			_value = response;

			// In the case of cancel. We do nothing with the result.
			if (response.Result != ActionSheetButtonResponse.Cancel)
			{
				if (response != null)
				{
					if (response.Result == ActionSheetButtonResponse.Option)
						Text = response.Option.Title;
					else
						Text = null;
				}
				else
					Text = null;
			}

			_valueSubject.OnNext(_converter(_value));
		}

		protected override void OnCellClicked()
		{
			var sheetModel = new ActionSheetViewModel() { CancelMessage = CancelText ?? "Cancel", DestructionMessage = DestructiveText, Options = Options, Title = TitleText, Owner = this };
			_sheetController.ShowActionSheet(sheetModel);
		}

		protected override void OnTextChanged(string textValue)
		{
			throw new NotImplementedException();
		}

        public override ValidationResult Validate()
        {
            return ValidationResult.Pass();
        }

		public TEnum Value
		{
			get { return _converter(_value); }
		}

		public ActionSheetOption[] Options { get; set; }
		public string DestructiveText { get; set; }
		public string CancelText { get; set; }
		public string TitleText { get; set; }
	}
}
