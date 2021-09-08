using System;
using System.Diagnostics;

namespace Redbridge.Diagnostics
{
	public class DebugTraceListener : TraceListener
	{
		public event EventHandler<DebugEventArgs> MessageReceived;

		protected virtual void OnMessageReceived(string message)
		{
			var handler = MessageReceived;

            handler?.Invoke(this, new DebugEventArgs(message));
        }

		public override void Write(string message)
		{
			OnMessageReceived(message);
		}

		public override void WriteLine(string message)
		{
			OnMessageReceived(message);
		}
	}
	
}
