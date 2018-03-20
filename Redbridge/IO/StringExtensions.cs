using System;
using System.IO;
using System.Text;

namespace Redbridge.IO
{
	public static class StringExtensions
	{
		public static Stream ToStream(this string input)
		{
			return input.ToStream(Encoding.UTF8);
		}

		public static Stream ToStream(this string input, Encoding encoding)
		{
			byte[] bytes = encoding.GetBytes(input);
			var stream = new MemoryStream(bytes);
			return stream;
		}

		public static MemoryStream ToMemoryStream(this string input)
		{
			return input.ToMemoryStream(Encoding.UTF8);
		}

		public static MemoryStream ToMemoryStream(this string input, Encoding encoding)
		{
			var bytes = encoding.GetBytes(input);
			var stream = new MemoryStream(bytes);
			return stream;
		}

		public static string DoubleQuote(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;
			
			return $"\"{input}\"";
		}

		public static string SingleQuote(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			return $"'{input}'";
		}

		public static string ToCamelCase(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;
			else
				return $"{input.Substring(0, 1).ToLower()}{input.Substring(1)}";
		}

		public static byte[] GetBytes(this string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public static string ToBase64(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			var bytes = Encoding.UTF8.GetBytes(input);
			var base64 = Convert.ToBase64String(bytes);
			return base64.Replace('/', '_').Replace('+', '-'); // For url encoding.
		}

		public static string FromBase64(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			var safeInput = input.Replace('_', '/').Replace('-', '+'); // For url encoding.
			var base64 = Convert.FromBase64String(safeInput);
			return Encoding.UTF8.GetString(base64, 0, base64.Length);
		}

		public static string SurroundWith(this string input, string surroundText)
		{
			return $"{surroundText}{input}{surroundText}";
		}

		public static byte[] Base64UrlDecode(this string arg)
		{
			// Convert from base64url string to base64 string
			var s = arg;
			s = s.Replace('-', '+').Replace('_', '/');
			switch (s.Length % 4)
			{
				case 0:
					break;              // No pad chars in this case
				case 2:
					s += "==";
					break;              // Two pad chars
				case 3:
					s += "=";
					break;              // One pad char
				default:
					throw new Exception("Illegal base64url string!");
			}

			return Convert.FromBase64String(s);
		}
	}
}
