using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using ProximitySenseSDK.Api.Model;
using ProximitySenseSDK.Util;

namespace ProximitySenseSDK.Api
{
	public class ApiOperations : IApiOperations
	{
		private HttpClient client;

		public ApiOperations()
		{
			AppUser = new AppUser ();
			BaseUrl = "https://platform.proximitysense.com/api/v1/";

			ActionBase.RegisterCommonActionTypes();
        }
		
		public AppUser AppUser { get; set; }
		public string BaseUrl { get; set; }
		public ApiCredentials Credentials { get; set; }
		public HttpClient Client
		{
			get
			{
				if (client == null)
					client = new HttpClient
					{
						BaseAddress = new Uri(BaseUrl)
					};

				return client;
			}
		}

		public const string HttpHeader_Authorization_ClientId = "X-Authorization-ClientId";
		public const string HttpHeader_Authorization_Signature = "X-Authorization-Signature";
		public const string HttpHeader_ProximitySense_SdkPlatformAndVersion = "X-ProximitySense-SdkPlatformAndVersion";
		public const string HttpHeader_ProximitySense_AppUserId = "X-ProximitySense-SdkPlatformAndVersion";

		protected async Task<HttpRequestMessage> PrepareSignedRequestAsync<T>(string endpoint, T objectContent, string method = "POST", string clientId = null, string privateKey = null)
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(string.Format("{0}/{1}", BaseUrl, endpoint)),
				Method = new HttpMethod(method),
			};

			string contentString = string.Empty;
			if (objectContent != null)
			{
				request.Content = new ObjectContent<T>(objectContent, new JsonMediaTypeFormatter());
				contentString = await request.Content.ReadAsStringAsync();
			}

			var token = new Sha256Hasher().GenerateHash(request.RequestUri.AbsoluteUri + contentString + (privateKey ?? Credentials.PrivateKey));

			request.Headers.Add(HttpHeader_Authorization_ClientId, clientId ?? Credentials.ApplicationId);
			request.Headers.Add(HttpHeader_Authorization_Signature, token);
			request.Headers.Add(HttpHeader_ProximitySense_SdkPlatformAndVersion, string.Format("{0} - {1} (Xamarin)", Acr.DeviceInfo.DeviceInfo.Instance.OperatingSystem, ProximitySenseSDK.SDKVersion));

			return request;
		}

		public async Task UpdateAppUserAsync()
		{
			var request = await PrepareSignedRequestAsync("appUser", AppUser);
			var response = await Client.SendAsync(request);
		}

		public async Task ReportBeaconSightingsAsync(IEnumerable<Sighting> sightings)
		{
			var request = await PrepareSignedRequestAsync("ranging", sightings.ToArray());
			var response = await Client.SendAsync(request);
		}

		public async Task PollForAvailableActionResultsAsync()
		{
			var request = await PrepareSignedRequestAsync("decision", (object)null, "GET");
			var response = await Client.SendAsync(request);

			if (response.IsSuccessStatusCode)
			{
				await ProcessTriggerResults(response);
			}
		}

		private async Task ProcessTriggerResults(HttpResponseMessage response)
		{
			var actions = await response.Content.ReadAsAsync<ActionWrapper[]>();

			foreach (var action in actions)
			{
				var resolved = ActionBase.ParseActionResponse(action.Type, action.Result);
				if (resolved != null)
				{
					// send it off - callback, notification etc.
				}
			}
		}
	}
}
