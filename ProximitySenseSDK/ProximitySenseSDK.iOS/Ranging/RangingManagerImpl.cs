using System;
using ProximitySense.Ranging;
using CoreLocation;
using Foundation;
using System.Linq;
using ProximitySense.Api.Model;
using System.Threading.Tasks;

namespace ProximitySense.Ranging
{
	public class RangingManagerImpl : IRangingManagerImpl
	{
		private CLLocationManager locationManager;
		private CLBeaconRegion region;
		private NSUuid uuid;

		public event ActionReceivedDelegate ActionReceived;

		public RangingManagerImpl ()
		{
			locationManager = new CLLocationManager ();
			locationManager.RegionEntered += LocationManager_RegionEntered;
			locationManager.RegionLeft += LocationManager_RegionLeft;
			locationManager.DidRangeBeacons += async (object sender, CLRegionBeaconsRangedEventArgs e) => {
				await SDK.Api.ReportBeaconSightingsAsync (e.Beacons.Where(x => x != null).Select (x => new Sighting
					{
						Uuid = x.ProximityUuid.AsString(),
						Major = (int)x.Major,
						Minor = (int)x.Minor,
						Rssi = (int)x.Rssi,
						Proximity = proximityToString(x.Proximity)
					}));
			};
		}

		void LocationManager_RegionEntered (object sender, CLRegionEventArgs e)
		{
			locationManager.StartMonitoring (region);
		}

		void LocationManager_RegionLeft (object sender, CLRegionEventArgs e)
		{
		}

		string proximityToString (CLProximity proximity)
		{
			switch (proximity) 
			{
			case CLProximity.Unknown:
				return "Unknown";
			case CLProximity.Far:
				return "Far";
			case CLProximity.Near:
				return "Near";
			case CLProximity.Immediate:
				return "Immediate";
			default:
				return null;
			}
		}

		private void checkAuthorizationStatus ()
		{
			if (CLLocationManager.Status != CLAuthorizationStatus.AuthorizedAlways)
				locationManager.RequestAlwaysAuthorization ();
		}
			
		public void StartForUuid(string uuid)
		{
			checkAuthorizationStatus ();

			this.uuid = new NSUuid (uuid);

			region = new CLBeaconRegion(this.uuid, "BlueBar Beacon Region");
			region.NotifyEntryStateOnDisplay = true;
			region.NotifyOnEntry = true;
			region.NotifyOnExit = true;

			locationManager.StartMonitoring (region);
			locationManager.StartRangingBeacons (region);

			StartDecisionPolling ();
		}

		private async void StartDecisionPolling ()
		{
			while (true) 
			{
				await Task.Delay (new TimeSpan(800));
				await SDK.Api.PollForAvailableActionResultsAsync (action => {
					var handler = ActionReceived;
					if (handler != null)
						handler (this, new ActionReceivedEventArgs{
							Action = action
						});
				});
			}
		}

		public void Stop()
		{
			locationManager.StopMonitoring (region);
			locationManager.StopRangingBeacons (region);
		}
	}
}
