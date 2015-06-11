using System;
using System.Text;
using System.Security.Cryptography;

namespace ProximitySenseSDK.Util
{
	public class Sha256Hasher
	{
		public  String GenerateHash(String value)
		{
			var sb = new StringBuilder();

			using (var hash = SHA256Managed.Create())
			{
				Encoding enc = Encoding.UTF8;
				Byte[] result = hash.ComputeHash(enc.GetBytes(value));

				foreach (Byte b in result)
					sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}
	}
}