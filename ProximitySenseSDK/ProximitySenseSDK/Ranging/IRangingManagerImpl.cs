using System;

namespace ProximitySenseSDK.Ranging
{
	public interface IRangingManagerImpl
	{
		event ActionReceivedDelegate ActionReceived;

		void StartForUuid(string uuid);
		void Stop();
	}
}