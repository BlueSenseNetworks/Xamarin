using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProximitySenseSDK.Api.Model;
using ProximitySenseSDK.Api.Model.Deserialization;
using ProximitySenseSDK.Util;

namespace ProximitySenseSDK.Api
{
	public class ApiOperations : IApiOperations
	{
		private HttpClient client;
		private string os = "TestOS";

		public ApiOperations()
		{
			BaseUrl = "https://platform.proximitysense.com/api/v1/";
			ActionBase.RegisterCommonActionTypes();
#if !DEBUG
			os = Acr.DeviceInfo.DeviceInfo.Instance.OperatingSystem;
#endif
		}
		
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

		public const string HttpHeaderAuthorizationClientId = "X-Authorization-ClientId";
		public const string HttpHeaderAuthorizationSignature = "X-Authorization-Signature";
		public const string HttpHeaderProximitySenseSdkPlatformAndVersion = "X-ProximitySense-SdkPlatformAndVersion";
		public const string HttpHeaderProximitySenseAppUserId = "X-ProximitySense-AppUserId";

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

			request.Headers.Add(HttpHeaderAuthorizationClientId, clientId ?? Credentials.ApplicationId);
			request.Headers.Add(HttpHeaderAuthorizationSignature, token);
			request.Headers.Add(HttpHeaderProximitySenseSdkPlatformAndVersion, string.Format("{0} - {1} (Xamarin)", os, ProximitySenseSDK.SDKVersion));
			request.Headers.Add(HttpHeaderProximitySenseAppUserId, ProximitySenseSDK.UserProfile.AppSpecificId);

			return request;
		}

		public async Task UpdateAppUserAsync(AppUser userProfile)
		{
			var request = await PrepareSignedRequestAsync("appUser", userProfile);
			var response = await Client.SendAsync(request);
		}

		public async Task ReportBeaconSightingsAsync(IEnumerable<Sighting> sightings)
		{
			var request = await PrepareSignedRequestAsync("ranging", sightings.ToArray());
			var response = await Client.SendAsync(request);
		}

		public async Task PollForAvailableActionResultsAsync(Action<ActionBase> onResult)
		{
			var request = await PrepareSignedRequestAsync("decision", (object)null, "GET");
			var response = await Client.SendAsync(request);

			if (response.IsSuccessStatusCode)
			{
				await ProcessTriggerResults(response, onResult);
			}
		}

		private async Task ProcessTriggerResults(HttpResponseMessage response, Action<ActionBase> onResult)
		{
			var actionsContent = await response.Content.ReadAsStringAsync();

			var actions = JsonConvert.DeserializeObject<IEnumerable<ActionWrapper>>(actionsContent, new ActionWrapperJsonConverter());
			
			foreach (var action in actions)
			{
				onResult(action.Result);
			}
		}
	}
}
