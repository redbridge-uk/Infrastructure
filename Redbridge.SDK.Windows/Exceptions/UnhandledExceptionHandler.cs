using System;
using System.Text;
using System.Threading.Tasks;

namespace Redbridge.Windows.Exceptions
{
	public class UnhandledExceptionHandler
	{
		static UnhandledExceptionHandler()
		{
			TerminateProcessOnUnobservedTasks = false;
		}

		public static bool Installed { get; private set; }
		public static bool TerminateProcessOnUnobservedTasks { get; set; }
		public static string ComponentName { get; set; }

		public static void Install()
		{
			if (!Installed)
			{
				AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
				TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
				Installed = true;
			}
		}

		public static void Uninstall()
		{
			if (Installed)
			{
				AppDomain.CurrentDomain.UnhandledException -= OnCurrentDomainUnhandledException;
				TaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;
				Installed = false;
			}
		}

		private static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			// If we have been set to not escalate the exception, then manage it as observed.
			if (!TerminateProcessOnUnobservedTasks)
				e.SetObserved();

			ProcessException(e.Exception);
		}

		private static void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			ProcessException((Exception)e.ExceptionObject);
		}

		private static void ProcessException(Exception exception)
		{
			var builder = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(ComponentName))
				builder.AppendLine(
					$"Component {ComponentName} has errored, please report the following details:");
			else
				builder.AppendLine("A Easilog Component has errored, please report the following details:");

			builder.AppendLine($"Message: {exception.Message}");
			builder.AppendLine($"Source: {exception.Source}");
			builder.AppendLine($"StackTrace: {exception.StackTrace}");
			AppendInnerException(builder, exception.InnerException);
		}

		private static void AppendInnerException(StringBuilder builder, Exception exception)
		{
			if (exception != null)
			{
				builder.AppendFormat("Inner Exception: {0}", exception.Message);
				AppendInnerException(builder, exception.InnerException);
			}
		}
	}
}
