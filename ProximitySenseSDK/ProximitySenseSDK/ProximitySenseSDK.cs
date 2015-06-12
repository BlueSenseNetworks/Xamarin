using ProximitySenseSDK.Api;
using ProximitySenseSDK.Api.Model;

namespace ProximitySenseSDK
{
	public static class ProximitySenseSDK
	{
		public const string SDKVersion = "1.0";
		private static readonly IApiOperations apiOperations = new ApiOperations();
		private static readonly AppUser userProfile = new AppUser();

		public static IApiOperations Api 
		{
			get { return apiOperations; }
		}

		public static AppUser UserProfile 
		{
			get { return userProfile; }
		}

		public static void Initialize(ApiCredentials apiCredentials) 
		{
			Api.Credentials = apiCredentials;
		}

		public static void Initialize(string applicationId, string privateKey) 
		{
			Initialize(new ApiCredentials(applicationId, privateKey));
		}
	}
}
