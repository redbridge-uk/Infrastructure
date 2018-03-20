using Redbridge.DependencyInjection;
using Redbridge.Forms.ViewModels.Cells;

namespace Redbridge.Forms
{
	public class TableCellRegistrationConfiguration
	{
		public void Configure(IContainer container)
		{
			OnConfigure(container);
		}

		protected virtual void OnConfigure(IContainer container)
		{
			container.RegisterType<ITableCellFactory, TableCellFactory>(LifeTime.Container);
			container.RegisterType<ICellFactory, CircledTextCellFactory>(CircledTextCellViewModel.CellTypeName);
            container.RegisterType<ICellFactory, IconTextCellFactory>(IconTextCellViewModel.CellTypeName);
            container.RegisterType<ICellFactory, BusyCellFactory>(BusyCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, TextCellFactory>(TextCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, SwitchCellFactory>(SwitchCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, NullableSwitchCellFactory>(NullableSwitchCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, TextEntryCellFactory>(TextEntryCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, DateTimeCellFactory>(DateTimeCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, DatePickerCellFactory>(DatePickerCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, ImageCellFactory>(ImageCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, NumericEntryCellFactory>(NumericEntryCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, CommandCellFactory>(CommandCellViewModel.CellTypeName);
			container.RegisterType<ICellFactory, ActionSheetCellFactory>(ActionSheetCellViewModel.CellTypeName);
		}
	}
}
