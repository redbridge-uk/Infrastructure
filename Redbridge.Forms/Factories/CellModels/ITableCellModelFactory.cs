using System;
namespace Redbridge.Forms
{
	public interface ITableCellModelFactory
	{
		TModel CreateCellModel<TModel, TSource>(TSource source);
	}
}
