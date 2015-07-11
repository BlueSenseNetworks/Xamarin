using System;

namespace ProximitySense.Ranging
{
	public interface IRangingManagerImpl
	{
		event ActionReceivedDelegate ActionReceived;

		void StartForUuid(string uuid);
		void Stop();
	}
}