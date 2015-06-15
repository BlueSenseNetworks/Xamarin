using System;
using ProximitySenseSDK.Ranging;

namespace ProximitySenseSDK.Ranging
{
	public class RangingManagerImpl : IRangingManagerImpl
	{
		public event ActionReceivedDelegate ActionReceived;

		public RangingManagerImpl ()
		{
		}

		public void StartForUuid(string uuid)
		{
			
		}

		public void Stop()
		{
		}
	}
}

