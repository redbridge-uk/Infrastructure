using System;
using Redbridge.SDK;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface IEntryCellViewModel : ITableCellViewModel
	{
		Keyboard Keyboard { get; }
		string Label { get; }
		string PlaceholderText { get; }
        Icon PlaceholderIcon { get; }
        bool IsSecure { get; } // Note to your future-self, don't forget dates and numbers could be secure too.
		IObservable<ValidationResult> Validation { get; }
		ValidationResult Validate();
		AutoCapitalisationMode AutoCapitalisationMode { get; }
		bool AllowDirectEdit { get; }
		TextClearButtonMode ClearButtonMode { get; }
        bool AllowTextEntry(int start, int length, string content);
	}
	
}
