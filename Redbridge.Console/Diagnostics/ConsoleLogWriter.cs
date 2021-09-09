using System;
using System.Text;
using Redbridge.Diagnostics;

namespace Redbridge.Console.Diagnostics
{
    public enum LoggingLevel
    {
        Disabled = 0,
        Error = 2,
        Success = 3,
        Warning = 4,
        Info = 5,
        Debug = 6,
        Trace = 7,
    }

	public class ConsoleLogWriter : ILogger
	{
		public ConsoleLogWriter() : this(LoggingLevel.Info) { }

		public ConsoleLogWriter(LoggingLevel level)
		{
			LogLevel = level;
		}

		public LoggingLevel LogLevel
		{
			get;
			set;
		}

		public bool OutputErrors => LogLevel >= LoggingLevel.Error;

		public bool OutputWarning => LogLevel >= LoggingLevel.Warning;

		public bool OutputInfo => LogLevel >= LoggingLevel.Info;

		public bool OutputDebug => LogLevel >= LoggingLevel.Debug;

		private void WriteLog(string format, LoggingLevel level, params object[] arguments)
		{
			ConsoleColor logColor = GetLevelColor(level);

			using (ConsoleWriter writer = new ConsoleWriter(logColor))
			{
				string formattedEntry = string.Format(format, arguments);
				writer.WriteLine($"{DateTime.UtcNow}: {formattedEntry}");
			}
		}

		protected virtual ConsoleColor GetLevelColor(LoggingLevel level)
		{
			switch (level)
			{
				case LoggingLevel.Error:
					return ConsoleColor.Red;
				case LoggingLevel.Warning:
					return ConsoleColor.Yellow;
				case LoggingLevel.Info:
					return ConsoleColor.Gray;
				case LoggingLevel.Debug:
					return ConsoleColor.DarkGray;
				default:
					return ConsoleColor.Gray;
			}
		}


		public void WriteError(string format)
		{
			if (OutputErrors)
				WriteLog(format, LoggingLevel.Error);
		}

		public void WriteWarning(string format)
		{
			if (OutputWarning)
				WriteLog(format, LoggingLevel.Warning);
		}

		public void WriteInfo(string format)
		{
			if (OutputInfo)
				WriteLog(format, LoggingLevel.Info);
		}

		public void WriteDebug(string format)
		{
			if (OutputDebug)
				WriteLog(format, LoggingLevel.Debug);
		}


		public void WriteException(Exception exception)
		{
			if (exception != null)
			{
				int indentationLevel = 0;
				StringBuilder builder = new StringBuilder();
				builder.AppendLine("Exception Details:");
				builder.AppendLine(exception.Message);

				var aggregateException = exception as AggregateException;

				if (aggregateException != null)
				{
					WriteException(builder, aggregateException, ref indentationLevel);

					foreach (var innerException in aggregateException.InnerExceptions)
						WriteException(builder, innerException, ref indentationLevel);
				}
				else
					WriteException(builder, exception.InnerException, ref indentationLevel);

				// Write the exception details to the log
				WriteError(builder.ToString());
			}
		}

		private void WriteException(StringBuilder builder, Exception exception, ref int indentationLevel)
		{
			// If the inner exception isn't null, then log it.
			if (exception != null)
			{
				indentationLevel = indentationLevel + 1;
				string indentation = new string('-', indentationLevel);
				builder.AppendFormat("{0}{1}{2}", indentation, exception.Message, Environment.NewLine);

				// Recursively populate the inner exception...
				if (exception.InnerException != null)
					WriteException(builder, exception.InnerException, ref indentationLevel);
			}
		}
	}
}
