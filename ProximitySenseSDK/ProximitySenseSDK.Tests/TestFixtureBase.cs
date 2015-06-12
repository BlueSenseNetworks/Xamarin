using NUnit.Framework;

namespace ProximitySenseSDK.Tests
{
	[TestFixture]
	public abstract class TestFixtureBase
	{
		private const string ApplicationId = "19ed29fefb0043a1a319cc664e57c1e3";
		private const string PrivateKey = "K8fRofHy2so3Q01RR8MwupE3Sd0ZMgL2pWdZxGp4";

		[TestFixtureSetUp]
		public virtual void FixtureSetup()
		{
			ProximitySenseSDK.Initialize(ApplicationId, PrivateKey);
			ProximitySenseSDK.Api.BaseUrl = "http://localhost/BSN.Platform/api/v1";
		}
	}
}