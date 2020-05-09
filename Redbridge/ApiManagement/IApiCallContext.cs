using System;

namespace Redbridge.ApiManagement
{
	public interface IApiCallContext : IFormatProvider
	{
		bool IsAuthenticated { get; }
        string FirstName { get; }
        string Surname { get; }
        string EmailAddress { get; }
	}
}
