using System;
namespace Redbridge.Diagnostics
{
	public interface ILogger
	{
		void WriteDebug(string message);

		void WriteException(Exception exception);

		void WriteError(string message);

		void WriteInfo(string message);

		void WriteWarning(string message);
	}
}
