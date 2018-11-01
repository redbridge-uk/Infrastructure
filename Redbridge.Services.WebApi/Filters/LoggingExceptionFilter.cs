using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Redbridge.Diagnostics;

namespace Redbridge.Services.WebApi.Filters
{
    public class LoggingExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public LoggingExceptionFilter(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                _logger.WriteInfo($"Logging that an exception has occurred in LoggingExceptionFilter: {actionExecutedContext.Exception.Message}...");
                var messagePhrase = actionExecutedContext.Exception.Message ?? "Internal server error - no additional detail supplied";
                messagePhrase = string.Join(",", messagePhrase.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Where(s => !string.IsNullOrWhiteSpace(s)) ); // Cariage returns are not permitted in reason phrases.

                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    RequestMessage = actionExecutedContext.Request,
                    ReasonPhrase = messagePhrase
                };

                actionExecutedContext.Response = response;
                _logger.WriteException(actionExecutedContext.Exception);
            }
        }
    }
}
