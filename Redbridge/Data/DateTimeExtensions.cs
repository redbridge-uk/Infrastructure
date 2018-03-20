using System;
using System.Collections.Generic;
using System.Linq;

namespace Redbridge.SDK
{
public static class DateTimeExtensions
{
	private static readonly Dictionary<long, string> RelativeTimeThresholds = new Dictionary<long, string>();

	static DateTimeExtensions()
	{
		const long minute = 60;
		const long hour = 60 * minute;
		const long day = 24 * hour;
		RelativeTimeThresholds.Add(long.MaxValue, "{0} centenaries ago");
		RelativeTimeThresholds.Add((day * 365L) * 200L, "a centenary ago");
		RelativeTimeThresholds.Add((day * 365L) * 100L, "{0} years ago");
		RelativeTimeThresholds.Add(day * 365 * 2, "a year ago");
		RelativeTimeThresholds.Add(day * 365, "{0} months ago");
		RelativeTimeThresholds.Add(day * 60, "a month ago");
		RelativeTimeThresholds.Add(day * 30, "{0} days ago");
		RelativeTimeThresholds.Add(day * 2, "yesterday");
		RelativeTimeThresholds.Add(day, "{0} hours ago");
		RelativeTimeThresholds.Add(120 * minute, "an hour ago");
		RelativeTimeThresholds.Add(45 * minute, "{0} minutes ago");
		RelativeTimeThresholds.Add(minute * 2, "a minute ago");
		RelativeTimeThresholds.Add(60, "{0} seconds ago");
		RelativeTimeThresholds.Add(2, "a second ago");
	}

	public static string RelativeDate(this DateTime date)
	{
		return RelativeDate(date, DateTime.Now);
	}

	public static string DayOrdinal(this DateTime date)
	{
		int dayNumber = date.Day;
		switch (dayNumber % 100)
		{
			case 11:
			case 12:
			case 13:
				return dayNumber + "th";
		}

		switch (dayNumber % 10)
		{
			case 1:
				return dayNumber + "st";
			case 2:
				return dayNumber + "nd";
			case 3:
				return dayNumber + "rd";
			default:
				return dayNumber + "th";
		}
	}

	public static string RelativeDate(DateTime start, DateTime end)
	{
		string description = null;
		var since = (end.Ticks - start.Ticks) / 10000000;
		foreach (var threshold in RelativeTimeThresholds.Keys.OrderBy(x => x))
		{
			if (since < threshold)
			{
				var t = new TimeSpan((end.Ticks - start.Ticks));
				description = string.Format(
									RelativeTimeThresholds[threshold],
									(t.Days > 365 * 100) ?
									t.Days / (365 * 100) :
									(t.Days > 365) ?
									t.Days / 365 :
									(t.Days > 31) ?
									t.Days / 31 :
									(t.Days > 0) ?
									t.Days :
									(t.Hours > 0) ?
									t.Hours :
									(t.Minutes > 0) ?
									t.Minutes :
									(t.Seconds > 0) ?
									t.Seconds : 0);
				break;
			}
		}

		return description;
	} }
}
