using System;
using System.Net.Http;
using System.Threading.Tasks;
using ProximitySenseSDK.Util;
using System.Net.Http.Formatting;

namespace ProximitySenseSDK.Api
{
	public class ApiOperations : IApiOperations
	{
		public ApiOperations()
		{
			AppUser = new AppUser ();
			BaseUrl = "https://platform.proximitysense.com/api/v1/";
		}
		
		public AppUser AppUser { get; set; }
		public string BaseUrl { get; set; }
		public ApiCredentials Credentials { get; set; }

		public const string ApplicationIdHeaderName = "X-Authorization-ClientId";
		public const string SignatureHeaderName = "X-Authorization-Signature";
		public const string PlatformAndSdkVersionHeaderName = "X-ProximitySense-SdkPlatformAndVersion";

		protected HttpRequestMessage PrepareSignedRequest<T>(T objectContent, string endpoint, string clientId, string privateKey)
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(string.Format("{0}/{1}", BaseUrl, endpoint)),
				Method = HttpMethod.Post,
				Content = new ObjectContent<T>(objectContent, new JsonMediaTypeFormatter())
			};

			var contentString = request.Content.ReadAsStringAsync().Result;
			var token = new Sha256Hasher().GenerateHash(request.RequestUri.AbsoluteUri + contentString + privateKey);

			request.Headers.Add(ApplicationIdHeaderName, clientId);
			request.Headers.Add(SignatureHeaderName, token);
			request.Headers.Add(PlatformAndSdkVersionHeaderName, string.Format("{0} - {1} (Xamarin)", Acr.DeviceInfo.DeviceInfo.Instance.OperatingSystem, ProximitySenseSDK.SDKVersion));

			return request;
		}

		public async Task UpdateAppUserAsync()
		{
			var client = new HttpClient ();
			client.BaseAddress = new Uri(BaseUrl);

			await client.PostAsJsonAsync ("appUser", AppUser);
		}
	}
}

