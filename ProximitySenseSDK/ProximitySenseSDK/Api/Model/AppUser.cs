using System.Collections.Generic;

namespace ProximitySenseSDK.Api.Model
{
	public class AppUser
	{
		public string AppSpecificId { get; set; }
		public Dictionary<string, string> UserMetadata { get; set; }
	}
}

