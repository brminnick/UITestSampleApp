using NUnit.Framework;

using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
	public class ReplTests : BaseTest
	{

		public ReplTests (Platform platform) : base(platform)
		{
		}

		[Ignore ("REPL Tests only for Testing/Developing")]
		[Test]
		public void Repl()
		{
			App.Repl ();
		}
	}
}

