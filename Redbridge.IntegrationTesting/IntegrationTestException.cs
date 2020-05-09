using System;

namespace Redbridge.IntegrationTesting
{
    public class IntegrationTestException : RedbridgeException
    {
        public IntegrationTestException () : base() {}

        public IntegrationTestException (string message) : base(message) {}
    }
}
