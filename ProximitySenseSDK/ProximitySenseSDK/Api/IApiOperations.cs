using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProximitySense.Api.Model;

namespace ProximitySense.Api
{
	public interface IApiOperations
	{
		string BaseUrl { get; set; }
		ApiCredentials Credentials { get; set; }

		Task UpdateAppUserAsync (AppUser userProfile);
		Task ReportBeaconSightingsAsync(IEnumerable<Sighting> sightings);
		Task PollForAvailableActionResultsAsync(Action<ActionBase> onResult);
		Task ReportDeviceCapabilitiesAsync(DeviceCapabilities deviceCaps);
	}

}

