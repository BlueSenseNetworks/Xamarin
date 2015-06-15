using ProximitySenseSDK.Api.Model;
using ProximitySenseSDK.Api;
using Android.Content;
using Android.App;
using Android.Content.PM;
using Android.Bluetooth;

namespace ProximitySenseSDK.Api
{
	public class DeviceCapabilitiesResolver : IDeviceCapabilitiesResolver
	{
		public void ResolveDeviceCapabilities(DeviceCapabilities deviceCapabilities)
		{
			deviceCapabilities.BleSupported = Application.Context.PackageManager.HasSystemFeature ("android.hardware.bluetooth_le");
			deviceCapabilities.BluetoothOn = BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled;
			deviceCapabilities.LocationServicesStatus = 0;
			deviceCapabilities.PushMessagesConsent = true;
		}
	}
}