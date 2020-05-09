using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Redbridge.Diagnostics;
using Redbridge.Exceptions;

namespace Redbridge.Services.WebApi.Filters
{
	public class UserNotAuthenticatedExceptionFilter : ExceptionFilterAttribute
	{
        readonly ILogger _logger;

        public UserNotAuthenticatedExceptionFilter(ILogger logger)
        {
            if (logger == null) throw new System.ArgumentNullException(nameof(logger));
            _logger = logger;
        }

		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
            _logger.WriteInfo("Checking exception for user not authenticated exception filtering....");

			var exception = actionExecutedContext.Exception as UserNotAuthenticatedException;

			if (exception != null)
			{
                _logger.WriteDebug("Processing user not authenticated exception filtering....");
				var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
				actionExecutedContext.Response = responseMessage;
                actionExecutedContext.Exception = null;
			}
		}
	}
}
