using System.Collections.Generic;

namespace ProximitySense.Api.Model
{
	public class RichContentAction : ActionBase
	{
		public RichContentAction()
		{
			Metadata = new Dictionary<string, string>();
		}

		public bool SendNotification { get; set; }
		public string NotificationText { get; set; }

		public bool SendContent { get; set; }
		public string Content { get; set; }
		public string ContentUrl { get; set; }

		public Dictionary<string, string> Metadata { get; set; }
	}
}