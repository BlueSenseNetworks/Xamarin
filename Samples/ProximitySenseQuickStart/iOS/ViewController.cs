using System;
using ProximitySense.Api.Model;
using UIKit;

namespace ProximitySenseQuickStart.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;
		
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		private void ActionReceived(ActionBase action)
		{
			var richContentAction = action as RichContentAction;
			if (richContentAction != null)
			{
				UIAlertView message = new UIAlertView("Action received", richContentAction.NotificationText, null, "OK", null);

				message.Show();
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate
			{
				var title = string.Format("{0} clicks!", count++);
				Button.SetTitle(title, UIControlState.Normal);
			};

			ApiConnector.InitProximitySenseSDKAsync(ActionReceived).Wait();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
