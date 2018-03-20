using System;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface IPageViewModel : IViewModel
	{
		string Title { get; }
		ToolbarViewModel Toolbar { get; }
		SearchBarViewModel SearchBar { get; }
		ImageSource Icon { get; } // For tabbed pages e.t.c.
		bool ShowNavigationBar { get; }
		Color NavigationBarTextColour { get; }
		Color NavigationBarColour { get; }
        bool NavigateBack();
        ValidationResultCollection Validate();
	}
}
