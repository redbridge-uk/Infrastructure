using System;

namespace Redbridge.SDK
{
	public interface IApiCallContext : IFormatProvider
	{
		bool IsAuthenticated { get; }
        string FirstName { get; }
        string Surname { get; }
        string EmailAddress { get; }
	}
}
