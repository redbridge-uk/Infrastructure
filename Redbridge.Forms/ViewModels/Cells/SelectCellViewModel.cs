using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Subjects;
using Redbridge.SDK;
using Redbridge.Validation;
using Redbridge.Forms.Markup;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class SelectCellViewModel<TTarget, TItem, TItemKey> : SelectCellViewModel<TItem, TItemKey>, ISavable
        where TTarget : class
        where TItem: class, IDisplayText, IUnique<TItemKey>
        where TItemKey: IEquatable<TItemKey>
    {
        private readonly PropertyValueManager<TTarget, string> _propertyValueSaver;

        public SelectCellViewModel(TTarget data, Expression<Func<TTarget, string>> propertySetter)
        {
            _propertyValueSaver = new PropertyValueManager<TTarget, string>(data, propertySetter);
            Text = _propertyValueSaver.GetValue();
        }

        public ValidationResultCollection TrySave()
        {
            return _propertyValueSaver.TrySave(ValueSubject.Value);
        }
    }

    public interface IDisplayText
    {
        string DisplayText { get; }
    }

    public class SelectCellViewModel<TItem, TItemKey> 
        : SelectCellViewModel
    where TItem : class, IDisplayText, IUnique<TItemKey>
        where TItemKey: IEquatable<TItemKey>
    {
        private BehaviorSubject<TItem> _selectedItem = new BehaviorSubject<TItem>(default(TItem));

        public SelectCellViewModel()
        {
            CancelText = "Cancel";
            _selectedItem.Subscribe((si) =>
            {
                if (si != null)
                {
                    Text = si.DisplayText;
                }
                else
                    Text = null;
            });
        }

        public IEnumerable<TItem> ItemSource
        {
            get;
            set;
        }

        public TItem SelectedItem
        {
            get { return _selectedItem.Value; }
            set
            {
                if (_selectedItem.Value == null)
                {
                    if (value != null)
                    {
                        _selectedItem.OnNext(value);
                    }
                }
                else
                {
                    if (!_selectedItem.Value.Equals(value))
                    {
                        _selectedItem.OnNext(value);
                    }
                }
            }
        }

        public string SelectionTitle
        {
            get; set;
        }

        protected ActionSheetOption[] CreateActionSheetOptions()
        {
            return ItemSource.Select(CreateActionSheetOption).ToArray();
        }

        protected virtual ActionSheetOption CreateActionSheetOption(TItem item)
        {
            return new ActionSheetOption()
            {
                 Title = item.DisplayText,
                 Value = item.Id,
            };
        }

		protected override void OnCellClicked()
		{
			var sheetModel = new ActionSheetViewModel() 
            { 
                CancelMessage = CancelText ?? "Cancel", 
                DestructionMessage = DestructiveText, 
                Options = CreateActionSheetOptions(), 
                Title = SelectionTitle, 
                Owner = this };
		}

		public override ValidationResult Validate()
		{
			if (IsMandatory)
				return _selectedItem.Value.Equals(default(TItem)) ? ValidationResult.Pass() : ValidationResult.Fail(FieldName, $"The field {FieldName} is mandatory, a selection is required.");

			return ValidationResult.Pass(FieldName);
		}
	}

    [CellFactory(typeof(SelectCellFactory), CellTypeName)]
	public class SelectCellViewModel : EntryCellViewModel<string>
	{
		public SelectCellViewModel()
		{
            Keyboard = Keyboard.Text;
			AllowCellSelection = true;
            AllowDirectEdit = false;
            Accessories = CellIndicators.Disclosure;
			ClearButtonMode = TextClearButtonMode.WhilstEditing;
            SelectionStyle = ItemSelectorStyle.ActionSheet;
		}

		public const string CellTypeName = "select";

        public bool IsMandatory { get; set; }

        public ItemSelectorStyle SelectionStyle 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets text used to cancel a picker selection on an action sheet or full picker page leaving the current value unchanged.
        /// </summary>
        /// <value>The cancel text.</value>
        public string CancelText { get; set; }

        /// <summary>
        /// Gets or sets the destructive text used to clear a picker selection or an action sheet page.
        /// </summary>
        /// <value>The destructive text.</value>
        public string DestructiveText { get; set; }

		protected override void OnTextChanged(string textValue)
		{
			Value = textValue;
		}

        public override ValidationResult Validate()
        {
            return ValidationResult.Pass(FieldName);
        }

        public override string CellType
		{
			get { return CellTypeName; }
		}
	}
}
