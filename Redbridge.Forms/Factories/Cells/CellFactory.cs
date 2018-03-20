using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public abstract class CellFactory<TModel> : CellFactory
	where TModel: class, ITableCellViewModel
	{
		protected override Cell OnCreate(ITableCellViewModel model)
		{
			var castModel = model as TModel;

			if (castModel == null)
				throw new NotSupportedException("Cannot cast to the correct view model type.");

			return OnCreateCell(castModel);
		}

		protected abstract Cell OnCreateCell(TModel model);
	}

	public abstract class CellFactory : ICellFactory
	{
		public Cell Create(ITableCellViewModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			var cell = OnCreate(model);
			cell.BindingContext = model;

			OnSetBindings(cell);
			OnSetEvents(cell, model);

			return cell;
		}

		protected virtual void OnSetEvents(Cell cell, ITableCellViewModel model)
		{
			cell.Tapped += (sender, e) => model.CellClicked();
		}

		protected virtual void OnSetBindings(Cell cell)
		{
			cell.SetBinding(Cell.IsEnabledProperty, "IsEnabled");
		}

		protected abstract Cell OnCreate(ITableCellViewModel model);
	}
}
