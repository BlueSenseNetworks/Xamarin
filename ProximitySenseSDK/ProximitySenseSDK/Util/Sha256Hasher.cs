using System;
using System.Text;
using ProximitySense.Util.Crypt;

namespace ProximitySense.Util
{
	public class Sha256Hasher
	{
		public string GenerateHash(string value)
		{
			var sb = new StringBuilder();

			using (var hash = SHA256.Create())
			{
				Encoding enc = Encoding.UTF8;
				var result = hash.ComputeHash(enc.GetBytes(value));

				foreach (var b in result)
					sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}
	}
}