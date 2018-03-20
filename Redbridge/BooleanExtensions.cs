using System;
namespace Redbridge.SDK
{
	public static class BooleanExtensions
	{
		public static string ToYesNo(this bool value)
		{
			return value ? "Yes" : "No";
		}
	}
}
