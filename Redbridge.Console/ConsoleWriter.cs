using System;

namespace Redbridge.Console
{
    /// <summary>
    /// Console writer class for managing writing out to the console.
    /// </summary>
    public class ConsoleWriter : IDisposable
    {
        private ConsoleColor _previousForegroundColor;
        private ConsoleColor _previousBackgroundColor;

        #region Constructors

        /// <summary>
        /// Constructor for the console writer that allows it to change the foreground color
        /// </summary>
        /// <param name="foreground"></param>
        public ConsoleWriter(ConsoleColor foreground) : this(System.Console.BackgroundColor, foreground) {}

        /// <summary>
        /// Constructor for the console writer.
        /// </summary>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        public ConsoleWriter(ConsoleColor background, ConsoleColor foreground)
        {
            _previousForegroundColor = System.Console.ForegroundColor;
            _previousBackgroundColor = System.Console.BackgroundColor;
            System.Console.ForegroundColor = foreground;
            System.Console.BackgroundColor = background;
        }

        #endregion Constructors

        /// <summary>
        /// Static method to write a line using a console writer and maintain previous colouring.
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="message"></param>
        /// <param name="arguments"></param>
        public static void WriteLine (ConsoleColor colour, string message, params string[] arguments)
        {
            using ( ConsoleWriter writer = new ConsoleWriter(colour) )
            {
                writer.WriteLine(message, arguments);
            }
        }

        /// <summary>
        /// Method that writes to the console.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arguments"></param>
        public void Write(string message, params string[] arguments) 
        {
            System.Console.Write(message, arguments);
        }

        /// <summary>
        /// Method that writes a text line to the console.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arguments"></param>
        public void WriteLine(string message, params string[] arguments)
        {
            System.Console.WriteLine(message, arguments);
        }

        /// <summary>
        /// Restores the console foreground/background color settings.
        /// </summary>
        public void Dispose()
        {
            System.Console.ForegroundColor = _previousForegroundColor;
            System.Console.BackgroundColor = _previousBackgroundColor;
        }
    }
}
