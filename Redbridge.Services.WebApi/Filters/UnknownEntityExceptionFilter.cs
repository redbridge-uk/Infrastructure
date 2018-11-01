using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Redbridge.Diagnostics;
using Redbridge.SDK.Data;

namespace Redbridge.Services.WebApi.Filters
{
	public class UnknownEntityExceptionFilter : ExceptionFilterAttribute
	{
        private readonly ILogger _logger;

        public UnknownEntityExceptionFilter(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			var unknownEntityException = actionExecutedContext.Exception as UnknownEntityException;

			if (unknownEntityException != null)
			{
                _logger.WriteInfo($"Unknown entity exception processing with message {unknownEntityException.Message}");

				var errorMessageError = new HttpError(unknownEntityException.Message);
				// Only a single result issue.
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse((HttpStatusCode)422, errorMessageError);
                actionExecutedContext.Response.ReasonPhrase = string.Join(",", unknownEntityException.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
                actionExecutedContext.Exception = null;
			}
		}
	}
}
