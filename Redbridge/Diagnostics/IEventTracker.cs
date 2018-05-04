using System.Collections.Generic;

namespace Redbridge.Diagnostics
{
    public interface IEventTracker
    {
        void WriteEvent(string eventName, IDictionary<string, string> metadata = null);
    }
}
