using System;
using Redbridge.DependencyInjection;
using Redbridge.Diagnostics;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class TableCellFactory : ITableCellFactory
	{
		readonly IContainer _container;
		private readonly ILogger _logger;

		public TableCellFactory(IContainer container, ILogger logger)
		{
			if (logger == null) throw new ArgumentNullException(nameof(logger));
			if (container == null) throw new ArgumentNullException(nameof(container));
			_container = container;
			_logger = logger;
		}

		public Cell CreateCellView(ITableCellViewModel arg)
		{
			if (arg == null) throw new ArgumentNullException(nameof(arg));
			_logger.WriteDebug($"TableCellFactory: Creating cell for cell type '{arg.CellType}'...");
			var factory = _container.Resolve<ICellFactory>(arg.CellType);
			return factory.Create(arg);
		}
	}
}
