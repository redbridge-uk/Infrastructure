using System;

namespace Redbridge.Console
{
    [Serializable]
    public class UnknownOptionException : CommandLineParseException
    {
        public UnknownOptionException() { }
        public UnknownOptionException(string message) : base(message) { }
        public UnknownOptionException(string message, Exception inner) : base(message, inner) { }

    }
}
