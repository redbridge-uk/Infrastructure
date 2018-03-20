using System;
using System.Windows.Input;

namespace Redbridge.Forms
{
	public class CommandCellViewModel : TextCellViewModel
	{
		public CommandCellViewModel(ICommand command, string text, string description = "", CellIndicators indicators = CellIndicators.Disclosure) :base(text, description, indicators)
		{
			if (command == null) throw new ArgumentNullException(nameof(command));
			Command = command;
			Command.CanExecuteChanged += (sender, e) => { IsEnabled = Command.CanExecute(CommandParameter); };
		}

		public new const string CellTypeName = "command";

		public override string CellType
		{
			get { return CellTypeName; }
		}
	}
}
