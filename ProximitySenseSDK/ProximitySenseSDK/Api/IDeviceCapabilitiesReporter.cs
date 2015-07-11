using System.Threading.Tasks;

namespace ProximitySense.Api
{
	public interface IDeviceCapabilitiesReporter
	{
		Task ReportDeviceCapabilitiesAsync();
	}
}