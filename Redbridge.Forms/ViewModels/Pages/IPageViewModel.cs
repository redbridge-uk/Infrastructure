using System;
using System.Threading.Tasks;
using Redbridge.Validation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface IPageViewModel: IViewModel, IDisposable
	{
		string Title { get; }
		ToolbarViewModel Toolbar { get; }
		SearchBarViewModel SearchBar { get; }
		ImageSource Icon { get; } // For tabbed pages e.t.c.
		bool ShowNavigationBar { get; }
		Color NavigationBarTextColour { get; }
		Color NavigationBarColour { get; }
        /// <summary>
        /// Return false to stop navigation
        /// </summary>
        /// <returns></returns>
        Task<bool> NavigateBack();
        ValidationResultCollection Validate();
	}
}
