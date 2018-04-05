using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface ITableViewModel : IPageViewModel
	{
		TableCellSeparatorStyle SeparatorStyle { get; }
		ITableViewModel Table { get; }
		void RefreshTable();
		bool PullToRefreshEnabled { get; set; }
        bool Editable { get; }
        bool ShowSearchBar { get; set; }
        ObservableCollection<TableSectionViewModel> Sections { get; }
        TableIntent Intent { get; set; }
        bool HasUnevenRows { get; set; }
	}
}
