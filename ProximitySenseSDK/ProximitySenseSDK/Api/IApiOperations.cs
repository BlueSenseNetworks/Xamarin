using System.Collections.Generic;
using System.Threading.Tasks;
using ProximitySenseSDK.Api.Model;

namespace ProximitySenseSDK.Api
{
	public interface IApiOperations
	{
		AppUser AppUser { get; }
		string BaseUrl { get; set; }
		ApiCredentials Credentials { get; set; }

		Task UpdateAppUserAsync ();
		Task ReportBeaconSightingsAsync(IEnumerable<Sighting> sightings);
		Task PollForAvailableActionResultsAsync();
	}

}

