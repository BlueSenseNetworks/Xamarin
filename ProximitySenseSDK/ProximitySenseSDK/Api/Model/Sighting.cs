namespace ProximitySenseSDK.Api.Model
{
	public class Sighting
	{
		public string Uuid { get; set; }
		public int Major { get; set; }
		public int Minor { get; set; }
		public int Rssi { get; set; }
		public string Proximity { get; set; }
	}
}
