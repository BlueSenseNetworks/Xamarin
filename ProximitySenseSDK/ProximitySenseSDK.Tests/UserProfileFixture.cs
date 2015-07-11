using System.Collections.Generic;
using NUnit.Framework;

namespace ProximitySense.Tests
{
	[TestFixture]
	public class UserProfileFixture : TestFixtureBase
	{
		[Test]
		public void Should_update_app_user_profile()
		{
			SDK.UserProfile.AppSpecificId = "Test 1";
			SDK.UserProfile.UserMetadata = new Dictionary<string, string>
			{
				{"key1", "value1"}
			};

			SDK.UserProfile.UpdateAsync().Wait();
		}
	}
}