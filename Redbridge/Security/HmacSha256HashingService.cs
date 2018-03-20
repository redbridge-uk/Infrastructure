using System;
using System.Security.Cryptography;

namespace Redbridge.Security
{
    public class HmacSha256HashingService : IHashingService
    {
		public string CreateHash (string text)
		{
			if (String.IsNullOrEmpty(text))
				return String.Empty;

            using (var sha = new HMACSHA256())
			{
				byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
				byte[] hash = sha.ComputeHash(textData);
				return BitConverter.ToString(hash).Replace("-", String.Empty);
			}
		}
    }
}
