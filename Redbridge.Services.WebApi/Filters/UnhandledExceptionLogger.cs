using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Redbridge.Diagnostics;

namespace Redbridge.Services.WebApi.Filters
{
	public class UnhandledExceptionLogger : IExceptionLogger
	{
		private readonly ILogger _logger;

		public UnhandledExceptionLogger(ILogger logger)
		{
			if (logger == null) throw new ArgumentNullException(nameof(logger));
			_logger = logger;
		}

		public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
            _logger.WriteDebug("Handling unhandled exceptions...");

            if (context.Exception is DbEntityValidationException exception)
            {
                _logger.WriteDebug("Unhandled exception turns out to be a DB entity validation error...");
                var ve = exception;
                var errors = ve.EntityValidationErrors?.Where(eve => !eve.IsValid).SelectMany(eve => eve.ValidationErrors).Select(err => $"{err.PropertyName}: {err.ErrorMessage}");
                var errorMessage = string.Join(",", errors);
                _logger.WriteError(errorMessage);
            }
            else
            {
                if (context.Exception != null)
                    _logger.WriteException(context.Exception);
            }

            return Task.CompletedTask;
		}
	}
}
