namespace Redbridge.ApiManagement
{
	public interface IApiCall
	{
		bool RequiresAuthentication { get; }
		string RequiredAction { get; }
		string ApiName { get; }
	}
}
