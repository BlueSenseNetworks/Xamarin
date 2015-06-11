using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProximitySenseSDK.Api
{
	public interface IApiOperations
	{
		AppUser AppUser { get; set; }
		string BaseUrl { get; set; }
		ApiCredentials Credentials { get; set; }

		Task UpdateAppUserAsync ();
	}

}

