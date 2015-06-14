namespace ProximitySenseSDK.Ranging
{
	public interface IRangingManager
	{
		void StartForUuid(string uuid);
		void Stop();
	}
}