using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface ICellFactory
	{
		Cell Create (ITableCellViewModel model);
	}
}
