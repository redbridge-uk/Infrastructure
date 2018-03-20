using System;
namespace Redbridge.Forms
{
	public interface IViewModelFactory
	{
		T CreateModel<T>() where T:IViewModel;

		IViewModel CreateModel(Type type);

		IViewModel CreateModel(Type type, string name);
	}
}
