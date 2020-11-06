using System;
using System.Collections.Generic;
using System.Linq;
using Redbridge.Diagnostics;

namespace Redbridge.ApiManagement
{
	public abstract class ApiCall : IApiCall
	{
		protected ILogger Logger { get; private set; }

		protected ApiCall(ILogger logger)
		{
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public virtual string ApiName => "Unspecified";

		public virtual bool RequiresAuthentication => true;

		public virtual IEnumerable<string> RequiredActions => Enumerable.Empty<string>();

		public virtual IEnumerable<string> RequiredRoles => Enumerable.Empty<string>();

        public virtual IEnumerable<string> PermittedClients => null;
	}
}
