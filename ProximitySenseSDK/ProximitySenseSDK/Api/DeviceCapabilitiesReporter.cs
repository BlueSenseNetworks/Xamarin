using System;
using System.Threading.Tasks;
using ProximitySenseSDK.Api.Model;

namespace ProximitySenseSDK.Api
{
	class DeviceCapabilitiesReporter : IDeviceCapabilitiesReporter
	{
		private readonly static Lazy<IDeviceCapabilitiesResolver> deviceCapabilitiesResolver = new Lazy<IDeviceCapabilitiesResolver>(() =>
		{
#if __PLATFORM__
            return new DeviceCapabilitiesResolver();
#else
			throw new Exception("Platform implementation not found.  Have you referenced your platform project?");
#endif
		}, false);

		public async Task ReportDeviceCapabilitiesAsync()
		{
			var deviceCaps = CollectDeviceCapabilities();

			await ProximitySenseSDK.Api.ReportDeviceCapabilitiesAsync(deviceCaps);
		}

		private DeviceCapabilities CollectDeviceCapabilities()
		{
			var deviceCaps = new DeviceCapabilities
			{
				ApiVersion = ProximitySenseSDK.SdkVersion,
#if __PLATFORM__
				OperatingSystem = DeviceInfo.Instance.OperatingSystem,
				ApplicationVersion = DeviceInfo.Instance.AppVersion,
				Manufacturer = DeviceInfo.Instance.Manufacturer,
				Model = DeviceInfo.Instance.Model,
				IsTablet = DeviceInfo.Instance.IsTablet,
#endif
			};

			deviceCapabilitiesResolver.Value.ResolveDeviceCapabilities(deviceCaps);

			return deviceCaps;
		}
	}

	public interface IDeviceCapabilitiesResolver
	{
		void ResolveDeviceCapabilities(DeviceCapabilities deviceCapabilities);
	}
}