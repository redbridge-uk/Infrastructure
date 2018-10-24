using System;
using System.Net;

namespace Redbridge.SDK
{
    public class UnhandledWebException : RedbridgeException
    {
        public UnhandledWebException(HttpStatusCode code, string reasonMessage) : this($"Unable to process web message, response code {code} reason given: {reasonMessage}")
        {
            Code = code;
        }

        public UnhandledWebException(string message) : base(message) { }

        public UnhandledWebException(string message, Exception inner) : base(message, inner) { }

        public HttpStatusCode Code 
        {
            private set;
            get;
        }
    }
}
