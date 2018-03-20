using System;

namespace Redbridge.Diagnostics
{
	public class iOSLogger : ILogger
	{
		public void WriteDebug(string message)
		{
            Console.WriteLine($"DEBUG {message}");
		}

		public void WriteError(string message)
		{
			Console.WriteLine($"ERROR: {message}");
		}

		public void WriteException(Exception exception)
		{
			Console.WriteLine($"EXCEPTION: {exception.Message}");
		}

		public void WriteInfo(string message)
		{
			Console.WriteLine($"INFO: {message}");
		}

		public void WriteWarning(string message)
		{
			Console.WriteLine($"WARNING: {message}");
		}
	}
}
