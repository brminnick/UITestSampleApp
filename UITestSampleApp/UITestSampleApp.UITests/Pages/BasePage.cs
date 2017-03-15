using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
	public abstract class BasePage
	{
		protected readonly IApp App;
		protected readonly bool OnAndroid;
		protected readonly bool OniOS;

		protected BasePage(IApp app, Platform platform)
		{
			App = app;

			OnAndroid = platform == Platform.Android;
			OniOS = platform == Platform.iOS;
		}
	}
}

