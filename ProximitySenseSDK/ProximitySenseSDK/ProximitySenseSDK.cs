using ProximitySense.Api;
using ProximitySense.Api.Model;
using ProximitySense.Ranging;
using ProximitySense.Util;

namespace ProximitySense
{
	public static class SDK
	{
		public const string SdkVersion = "1.0";

		private static readonly IDeviceCapabilitiesReporter deviceCapabilitiesReporter = new DeviceCapabilitiesReporter();
		private static readonly IApiOperations apiOperations = new ApiOperations();
		private static readonly AppUser userProfile = new AppUser();
		private static readonly IRangingManager rangingManager = new RangingManager();

		public static AppUser UserProfile
		{
			get { return userProfile; }
		}
		
		public static IApiOperations Api 
		{
			get { return apiOperations; }
		}

		public static IRangingManager RangingManager
		{
			get { return rangingManager; }
		}

		public static void Initialize(ApiCredentials apiCredentials) 
		{
			Api.Credentials = apiCredentials;

			deviceCapabilitiesReporter.ReportDeviceCapabilitiesAsync();
		}

		public static void Initialize(string applicationId, string privateKey) 
		{
			Initialize(new ApiCredentials(applicationId, privateKey));
		}
	}
}
