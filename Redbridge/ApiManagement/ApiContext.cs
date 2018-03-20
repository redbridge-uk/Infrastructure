using System;
using System.Security.Principal;

namespace Redbridge.ApiManagement
{
	public abstract class ApiContext
	{
		protected ApiContext() : this(DateTime.UtcNow) { }

		protected ApiContext(DateTime systemTime)
		{
			SystemDateTime = systemTime;
		}

		public DateTime SystemDateTime { get; protected set; }

		public abstract bool IsAuthenticated { get; }

		public abstract bool CanPerformAction(string action);

		public abstract IPrincipal CurrentPrincipal { get; }
	}
}
