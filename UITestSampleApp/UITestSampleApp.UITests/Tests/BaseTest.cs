using NUnit.Framework;

using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]

	public abstract class BaseTest
	{
		protected IApp app;
		protected Platform platform;

		protected FirstPage FirstPage;
		protected ListPage ListPage;
		protected LoginPage LoginPage;
		protected NewUserSignUpPage NewUserSignUpPage;

		protected BaseTest (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		virtual public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
			app.Screenshot("App Initialized");

			FirstPage = new FirstPage(app, platform);
			ListPage = new ListPage(app, platform);
			LoginPage = new LoginPage(app, platform);
			NewUserSignUpPage = new NewUserSignUpPage(app, platform);
		}
	}
}

