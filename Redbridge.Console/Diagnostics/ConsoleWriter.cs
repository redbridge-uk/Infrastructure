using System;

namespace Redbridge.Console.Diagnostics
{
	public class ConsoleWriter : IDisposable
	{
		private readonly ConsoleColor _previousForegroundColor;
		private readonly ConsoleColor _previousBackgroundColor;
		public ConsoleWriter(ConsoleColor foreground) : this(System.Console.BackgroundColor, foreground) { }
		public ConsoleWriter(ConsoleColor background, ConsoleColor foreground)
		{
			_previousForegroundColor = System.Console.ForegroundColor;
			_previousBackgroundColor = System.Console.BackgroundColor;
            System.Console.ForegroundColor = foreground;
            System.Console.BackgroundColor = background;
		}

		public static void WriteLine(ConsoleColor colour, string message, params object[] arguments)
		{
			using (var writer = new ConsoleWriter(colour))
			{
				writer.WriteLine(message, arguments);
			}
		}

		public void Write(string message, params object[] arguments)
		{
            System.Console.Write(message, arguments);
		}

		public void WriteLine(string message, params object[] arguments)
		{
            System.Console.WriteLine(message, arguments);
		}

		public void Dispose()
		{
            System.Console.ForegroundColor = _previousForegroundColor;
            System.Console.BackgroundColor = _previousBackgroundColor;
		}
	}
}
