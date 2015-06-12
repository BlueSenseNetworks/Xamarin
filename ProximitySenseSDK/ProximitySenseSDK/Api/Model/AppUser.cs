using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProximitySenseSDK.Api.Model
{
	public class AppUser
	{
		public AppUser()
		{
			AppSpecificId = "Default - Id not set";
		}

		public string AppSpecificId { get; set; }
		public Dictionary<string, string> UserMetadata { get; set; }

		public async Task UpdateAsync()
		{
			await ProximitySenseSDK.Api.UpdateAppUserAsync(this);
		}
	}
}

