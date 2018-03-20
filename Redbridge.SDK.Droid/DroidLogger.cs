using System;
using System.Diagnostics;
using Redbridge.Diagnostics;

namespace Redbridge.SDK.Droid
{
	public class DroidLogger : ILogger
	{
		public void WriteDebug(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteError(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteException(Exception exception)
		{
			Debug.WriteLine(exception.Message);
		}

		public void WriteInfo(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteWarning(string message)
		{
			Debug.WriteLine(message);
		}
	}
}
