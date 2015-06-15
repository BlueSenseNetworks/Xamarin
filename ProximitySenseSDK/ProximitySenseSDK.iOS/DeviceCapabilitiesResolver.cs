using ProximitySenseSDK.Api.Model;
using ProximitySenseSDK.Api;
using CoreBluetooth;
using CoreLocation;
using UIKit;

namespace ProximitySenseSDK.Api
{
	public class DeviceCapabilitiesResolver : IDeviceCapabilitiesResolver
	{
		public void ResolveDeviceCapabilities(DeviceCapabilities deviceCapabilities)
		{
			var centralManager = new CBCentralManager ();

			deviceCapabilities.BleSupported = centralManager.State != CBCentralManagerState.Unsupported;
			deviceCapabilities.BluetoothOn = centralManager.State == CBCentralManagerState.PoweredOn;
			deviceCapabilities.LocationServicesStatus = (uint) CLLocationManager.Status;
			deviceCapabilities.PushMessagesConsent = UIApplication.SharedApplication.EnabledRemoteNotificationTypes == UIRemoteNotificationType.Alert;
		}
	}
}