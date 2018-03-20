namespace Redbridge.ApiManagement
{
	public interface IUnitOfWorkFactory<out T>
	where T : IWorkUnit
	{
		T Create();
	}
}
