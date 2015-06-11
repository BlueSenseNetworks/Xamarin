namespace ProximitySenseSDK.Api
{
	public class ApiCredentials
	{
		public ApiCredentials(string applicationId, string privateKey)
		{
			ApplicationId = applicationId;
			PrivateKey = privateKey;
		}

		public string ApplicationId { get; set;}
		public string PrivateKey { get; set;}
	}
}

