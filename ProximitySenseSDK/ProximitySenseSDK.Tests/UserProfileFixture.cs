using System.Collections.Generic;
using NUnit.Framework;

namespace ProximitySenseSDK.Tests
{
	[TestFixture]
	public class UserProfileFixture : TestFixtureBase
	{
		[Test]
		public void Should_update_app_user_profile()
		{
			ProximitySenseSDK.UserProfile.AppSpecificId = "Test 1";
			ProximitySenseSDK.UserProfile.UserMetadata = new Dictionary<string, string>
			{
				{"key1", "value1"}
			};

			ProximitySenseSDK.UserProfile.UpdateAsync().Wait();
		}
	}
}