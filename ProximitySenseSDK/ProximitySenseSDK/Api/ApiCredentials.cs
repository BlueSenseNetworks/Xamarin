using System;

namespace ProximitySenseSDK.Api
{
	public class ApiCredentials
	{
		public ApiCredentials(string applicationId, string privateKey)
		{
			this.ApplicationId = applicationId;
			this.PrivateKey = privateKey;
		}

		public string ApplicationId { get; set;}
		public string PrivateKey { get; set;}
	}
}

