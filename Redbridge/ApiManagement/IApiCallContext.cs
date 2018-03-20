namespace Redbridge.SDK
{
	public interface IApiCallContext
	{
		bool IsAuthenticated { get; }
        string FirstName { get; }
        string Surname { get; }
        string EmailAddress { get; }
	}
}
