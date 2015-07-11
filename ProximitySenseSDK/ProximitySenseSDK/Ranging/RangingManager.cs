using System;

namespace ProximitySense.Ranging
{
	internal class RangingManager : IRangingManager
	{
		private readonly static Lazy<IRangingManagerImpl> regionManagerImpl = new Lazy<IRangingManagerImpl>(() =>
			{
#if __PLATFORM__
				return new RegionManagerImpl();
#else
				throw new Exception("Platform implementation not found.  Have you referenced your platform project?");
#endif
			}, false);

		public event ActionReceivedDelegate ActionReceived;

		public void StartForUuid(string uuid)
		{
			Stop ();

			regionManagerImpl.Value.ActionReceived += RegionManagerImpl_Value_ActionReceived;;
			regionManagerImpl.Value.StartForUuid (uuid);
		}

		void RegionManagerImpl_Value_ActionReceived (object sender, ActionReceivedEventArgs e)
		{
			var handler = ActionReceived;
			if (handler != null)
				handler (this, e);
		}

		public void Stop()
		{
			regionManagerImpl.Value.Stop ();
			regionManagerImpl.Value.ActionReceived -= RegionManagerImpl_Value_ActionReceived;
		}
	}

}