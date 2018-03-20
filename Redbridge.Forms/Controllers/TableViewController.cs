using System;
namespace Redbridge.Forms
{
	public class TableViewController
	{
		private readonly TableViewModel _tableViewModel;

		public TableViewController(TableViewModel tableViewModel)
		{
			if (tableViewModel == null) throw new ArgumentNullException(nameof(tableViewModel));
			_tableViewModel = tableViewModel;
		}

		protected virtual void OnCreateSections() { }

		protected TableViewModel TableViewModel
		{
			get { return _tableViewModel; }
		}
	}
}
