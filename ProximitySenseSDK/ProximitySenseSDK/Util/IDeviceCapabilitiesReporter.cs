using System.Threading.Tasks;

namespace ProximitySenseSDK.Util
{
	public interface IDeviceCapabilitiesReporter
	{
		Task ReportDeviceCapabilitiesAsync();
	}
}