using ProximitySenseSDK.Api.Model;

namespace ProximitySenseSDK.Ranging
{
	public class ActionReceivedEventArgs : System.EventArgs
	{
		public ActionBase Action { get; set; }
	}

	public delegate void ActionReceivedDelegate (object sender, ActionReceivedEventArgs e);

	public interface IRangingManager
	{
		event ActionReceivedDelegate ActionReceived;

		void StartForUuid(string uuid);
		void Stop();
	}
}