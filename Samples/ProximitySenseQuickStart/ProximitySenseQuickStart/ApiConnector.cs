using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProximitySense.Api;
using ProximitySense.Api.Model;
using ProximitySense.Ranging;

namespace ProximitySenseQuickStart
{
	public class ApiConnector
	{
		// Overwrite with your ProximitySense Application Id and Private Key
		private static string APPLICATION_ID = "YOUR APP ID";
		private static string PRIVATE_KEY = "YOUR APP PRIVATE KEY";

		private static string BEACONS_UUID = "A0B13730-3A9A-11E3-AA6E-0800200C9A66"; // Blue Sense Networks' factory beacon UUID

		public static IApiOperations Api { get; private set; }
		public static IRangingManager RangingManager { get; private set; }

		public static async Task InitProximitySenseSDKAsync(Action<ActionBase> actionReceived)
		{
			ProximitySense.SDK.Initialize(APPLICATION_ID, PRIVATE_KEY);

			Api = ProximitySense.SDK.Api;
			RangingManager = ProximitySense.SDK.RangingManager;

			// UserProfile represents the person that is currently using the app
			AppUser userProfile = ProximitySense.SDK.UserProfile;

			// Set the id of the current user. Should be set to whatever you use to uniquely identify your users - can be facebook id, email, guid etc.
			userProfile.AppSpecificId = "My User Id";

			// Pass extra information about your user, such as name, email, photo url, date of birth etc.
			Dictionary<string, string> userMetadata = new Dictionary<string, string>();
			userMetadata.Add("name", "John Snow");
			userMetadata.Add("email", "captain@thewatch.org");
			userMetadata.Add("company", "The Watch");
			userMetadata.Add("position", "Captain");

			userProfile.UserMetadata = userMetadata;
			await userProfile.UpdateAsync(); // Don't forget to save the changes to the user profile

			// Start listening for beacons
			RangingManager.ActionReceived += (o, e) => actionReceived(e.Action);
			RangingManager.StartForUuid(BEACONS_UUID);
		}
	}
}
