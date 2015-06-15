namespace ProximitySenseSDK.Api.Model
{
	public class DeviceCapabilities
	{
		public string OperatingSystem { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public bool IsTablet { get; set; }

		public string ApplicationVersion { get; set; }
		public string ApiVersion { get; set; }

		public bool BleSupported { get; set; }
		public bool BluetoothOn { get; set; }
		public uint LocationServicesStatus { get; set; }
		public bool PushMessagesConsent { get; set; }
	}
}