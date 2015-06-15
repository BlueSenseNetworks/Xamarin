using System.Threading.Tasks;

namespace ProximitySenseSDK.Api
{
	public interface IDeviceCapabilitiesReporter
	{
		Task ReportDeviceCapabilitiesAsync();
	}
}