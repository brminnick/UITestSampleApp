using NUnit.Framework;

using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	abstract class BaseTest
	{
		readonly Platform _platform;

		protected BaseTest(Platform platform) => _platform = platform;

		protected IApp App { get; private set; }
		protected FirstPage FirstPage { get; private set; }
		protected ListPage ListPage { get; private set; }
		protected LoginPage LoginPage { get; private set; }
		protected NewUserSignUpPage NewUserSignUpPage { get; private set; }

		[SetUp]
		virtual public void BeforeEachTest()
		{
			App = AppInitializer.StartApp(_platform);
			App.Screenshot("App Initialized");

			FirstPage = new FirstPage(App);
			ListPage = new ListPage(App);
			LoginPage = new LoginPage(App);
			NewUserSignUpPage = new NewUserSignUpPage(App);

			LoginPage.WaitForPageToLoad();
		}
	}
}

