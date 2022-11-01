namespace Redbridge.DependencyInjection
{
	public interface IUnitOfWorkFactory<out T>
	where T : IWorkUnit
	{
		T Create();
	}
}
