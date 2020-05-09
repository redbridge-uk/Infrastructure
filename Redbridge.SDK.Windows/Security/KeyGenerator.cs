using System.Security.Cryptography;
using System.Text;

namespace Redbridge.Windows.Security
{
	public class KeyGenerator
	{
		private static readonly char[] Chars;

		static KeyGenerator()
		{
			Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
		}

		public string GetUniqueKey(int maxSize)
		{
			var data = new byte[1];
			using (var crypto = new RNGCryptoServiceProvider())
			{
				crypto.GetNonZeroBytes(data);
				data = new byte[maxSize];
				crypto.GetNonZeroBytes(data);
			}

			var result = new StringBuilder(maxSize);
			foreach (var b in data)
			{
				result.Append(Chars[b % (Chars.Length)]);
			}
			return result.ToString();
		}
	}
}
