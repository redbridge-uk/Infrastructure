using System.Collections.Generic;

namespace Redbridge.Diagnostics
{
    public class BlackholeEventTracker : IEventTracker
    {
        public void WriteEvent(string eventName, IDictionary<string, string> metadata = null)
        {
        }
    }
}
