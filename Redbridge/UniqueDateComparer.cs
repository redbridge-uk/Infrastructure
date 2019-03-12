using System;
using System.Collections.Generic;

namespace Redbridge
{
    public class UniqueDateComparer : IEqualityComparer<DateTime>
    {
        public bool Equals(DateTime x, DateTime y)
        {
            return x.Date == y.Date;
        }

        public int GetHashCode(DateTime obj)
        {
            return obj.GetHashCode() ^ 365;
        }
    }
}