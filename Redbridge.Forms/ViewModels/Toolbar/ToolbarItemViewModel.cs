using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class TextToolbarItemViewModel : ToolbarItemViewModel
	{
		public TextToolbarItemViewModel() { }
		public TextToolbarItemViewModel(string text, ToolbarItemOrder position = ToolbarItemOrder.Default) : base(position)
		{
			Text = text;
		}

		public TextToolbarItemViewModel(string text, ICommand command, ToolbarItemOrder position = ToolbarItemOrder.Default) : base(command, position)
		{
			Text = text;
		}
	}

	public abstract class ToolbarItemViewModel : ViewModel, IToolbarItemViewModel
	{
		private string _text;

		protected ToolbarItemViewModel(ToolbarItemOrder position = ToolbarItemOrder.Default) 
		{
			Position = position;
		}

		protected ToolbarItemViewModel(ICommand command, ToolbarItemOrder position = ToolbarItemOrder.Default) : this(position)
		{
			Command = command;
		}

		public ICommand Command
		{
			get; 
			set;
		}

		public object CommandParameter
		{
			get; 
			set;
		}

		public FileImageSource Icon
		{
			get;
			set;
		}

		public ToolbarItemOrder Position
		{
			get;
			set;
		}

		public string Text
		{
			get
			{
				return _text;
			}

			set
			{
				if (_text != value)
				{
					_text = value;
					OnPropertyChanged("Text");
				}
			}
		}

		public void Clicked()
		{
			OnClicked();
		}

		protected virtual void OnClicked()
		{
		}
	}
}