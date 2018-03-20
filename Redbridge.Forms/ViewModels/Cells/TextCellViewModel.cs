using System;
using System.Linq.Expressions;

namespace Redbridge.Forms
{
    public class TextCellViewModel<TTarget> : TextCellViewModel
        where TTarget : class
    {
        private readonly PropertyValueManager<TTarget, string> _propertyValueSaver;

        public TextCellViewModel(TTarget data, Expression<Func<TTarget, string>> propertySetter)
        {
            _propertyValueSaver = new PropertyValueManager<TTarget, string>(data, propertySetter);
            Text = _propertyValueSaver.GetValue();
        }
    }

	public class TextCellViewModel : TableCellViewModel, ICellTextProvider
	{
		public const string CellTypeName = "text";

		private string _text;
		private string _detail;

		public TextCellViewModel() { }

		public TextCellViewModel(string text)
		{
			Text = text;
		}

		public TextCellViewModel(string text, CellIndicators indicators = CellIndicators.None) : base(indicators)
		{
			Text = text;
		}

		public TextCellViewModel(string text, string detail = "", CellIndicators indicators = CellIndicators.None) : base(indicators)
		{
			Text = text;
			Detail = detail;
		}

		public override string CellType
		{
			get { return CellTypeName; }
		}

		public string Text
		{
			get { return _text; }
			set 
			{
				if (_text != value)
				{
					_text = value;
					OnPropertyChanged("Text");
				}
			}
		}

		public string Detail
		{
			get { return _detail; }
			set
			{
				if (_detail != value)
				{
					_detail = value;
					OnPropertyChanged("Detail");
				}
			}
		}
	}
}
