using System;
using Redbridge.SDK;

namespace Redbridge.Console
{
    public class CommandLineParseException : Exception
    {
        public CommandLineParseException() { }
        public CommandLineParseException(string message) : base(message) { }
        public CommandLineParseException(string message, Exception inner) : base(message, inner) { }
    }
}
