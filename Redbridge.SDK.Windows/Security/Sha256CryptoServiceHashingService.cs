using System;
using System.Security.Cryptography;

namespace Redbridge.Security
{
    public class Sha256CryptoServiceHashingService : IHashingService
    {
		public string CreateHash (string input)
		{
			var hashAlgorithm = new SHA256CryptoServiceProvider();
			byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);
			byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
			return Convert.ToBase64String(byteHash);
		}
    }
}
