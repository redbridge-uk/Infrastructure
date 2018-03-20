using System;
using System.Diagnostics;

namespace Redbridge.Diagnostics
{

	public class LogEventArgs : EventArgs
	{
		public LogEventArgs(string message, params object[] arguments)
		{
			Format = message;
			Arguments = arguments;
		}

		private string Format
		{
			get;
			set;
		}

		private object[] Arguments
		{
			get;
			set;
		}

		public string Message
		{
			get
			{
				return string.Format(Format, Arguments);
			}
		}
	}
}
