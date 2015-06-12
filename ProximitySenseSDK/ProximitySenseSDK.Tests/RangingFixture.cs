using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using ProximitySenseSDK.Api.Model;

namespace ProximitySenseSDK.Tests
{
	public class RangingFixture : TestFixtureBase
	{
		private static IEnumerable<Sighting> CreateSightings()
		{
			var sightings = new[]
			{
				new Sighting
				{
					Uuid = "A0B13730-3A9A-11E3-AA6E-0800200C9A66",
					Major = 120,
					Minor = 99,
					Proximity = "Near",
					Rssi = -85
				},
				new Sighting
				{
					Uuid = "A0B13730-3A9A-11E3-AA6E-0800200C9A66",
					Major = 32881,
					Minor = 58918,
					Proximity = "Immediate",
					Rssi = -77
				},
			};
			return sightings;
		}

		[Test]
		public void Should_report_beacon_sightings()
		{
			var sightings = CreateSightings();

			ProximitySenseSDK.Api.ReportBeaconSightingsAsync(sightings).Wait();
		}

		[Test]
		public void Should_check_for_sighting_decision_results()
		{
			var sightings = CreateSightings();

			ProximitySenseSDK.Api.ReportBeaconSightingsAsync(sightings).Wait();

			Task.Delay(2000).Wait();

			ProximitySenseSDK.Api.PollForAvailableActionResultsAsync(a => Debug.WriteLine("Received decision for {0}: {1}ed '{2}'", a.AppSpecificId, a.ZoneEvent.EventType, a.ZoneEvent.ZoneName)).Wait();
		}
	}
}