using System;
namespace Redbridge.SDK
{
	public static class BearerTokenFormatter
	{
		public static string CreateToken(string value)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));
			return $"Bearer {value}";
		}
	}
}
