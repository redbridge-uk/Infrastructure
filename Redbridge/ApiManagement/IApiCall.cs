using System;
using System.Collections.Generic;

namespace Redbridge.ApiManagement
{
	public interface IApiCall
	{
		bool RequiresAuthentication { get; }
		IEnumerable<string> RequiredActions { get; }
		IEnumerable<string> RequiredRoles { get; }
        IEnumerable<string> PermittedClients { get; }
		string ApiName { get; }
	}
}
