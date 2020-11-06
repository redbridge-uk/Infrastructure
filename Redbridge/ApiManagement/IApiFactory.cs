namespace Redbridge.ApiManagement
{
	public interface IApiFactory
	{
		T CreateApi<T>() where T : class, IApiCall;
	}
}
