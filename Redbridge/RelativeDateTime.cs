using System;

namespace Redbridge
{
    public static class RelativeDateTime
    {
        public static DateTime Today => DateTime.UtcNow.Date;

        public static DateTime Tomorrow
        {
            get
            {
                var now = DateTime.UtcNow;
                return new DateTime(now.Year, now.Month, now.Day).AddDays(1).AddMilliseconds(-1);
            }
        }

        public static DateTime EndOfToday => Tomorrow.Subtract(TimeSpan.FromTicks(1));

        public static DateTime ToNearestStartOfWeek(this DateTime relativeTo)
        {
            var diff = relativeTo.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }

            return relativeTo.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfWeek
        {
            get
            {
                var now = DateTime.UtcNow;
                var diff = now.DayOfWeek - DayOfWeek.Monday;
                if (diff < 0)
                {
                    diff += 7;
                }

                return now.AddDays(-1 * diff).Date;
            }
        }

        public static DateTime EndOfThisWeek => StartOfWeek.AddDays(7);

        public static DateTime ToNearestEndOfWeek(this DateTime relativeTo)
        {
            return relativeTo.ToNearestStartOfWeek().AddDays(7).AddMilliseconds(-1);
        }

        public static DateTime ToStartOfDay(this DateTime relativeTo)
        {
            return relativeTo.Date;
        }

        public static DateTime ToEndOfDay(this DateTime relativeTo)
        {
            return relativeTo.Date.AddDays(1).AddMilliseconds(-1);
        }

        public static DateTime StartOfLastWeek => StartOfWeek.AddDays(-7);

        public static DateTime StartOfThisMonth
        {
            get
            {
                var now = DateTime.UtcNow;
                return new DateTime(now.Year, now.Month, 1);
            }
        }

        public static DateTime EndOfThisMonth => StartOfThisMonth.AddMonths(1).AddMilliseconds(-1);

        public static DateTime StartOfLastMonth => StartOfThisMonth.AddMonths(-1);
    }
}
