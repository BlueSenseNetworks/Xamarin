using System;
using ProximitySenseSDK.Api;

namespace ProximitySenseSDK
{
	public static class ProximitySenseSDK
	{
		public const string SDKVersion = "1.0";
		private static readonly IApiOperations apiOperations = new ApiOperations();

		public static IApiOperations Api 
		{
			get { return apiOperations; }
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

