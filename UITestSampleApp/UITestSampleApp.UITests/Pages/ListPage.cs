using System;
using System.Linq;

using Xamarin.UITest;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
	public class ListPage : BasePage
	{
		readonly Query _loadingDataFromBackendActivityIndicator;

		public ListPage(IApp app, Platform platform) : base(app, platform)
		{
			_loadingDataFromBackendActivityIndicator = x => x.Marked(AutomationIdConstants.LoadingDataFromBackendActivityIndicator);
		}

		public void TapListItemNumber(int listItemNumber, int timeoutInSeconds = 60)
		{
			app.ScrollDownTo(listItemNumber.ToString());
			app.Tap(x => x.Marked(listItemNumber.ToString()));
			app.WaitForElement("OK", "Ok Alert Did Not Appear", TimeSpan.FromSeconds(timeoutInSeconds));
			app.Screenshot($"Tap {listItemNumber} on List View Page");
		}

		public void TapOKOnAlert(int timeoutInSeconds = 60)
		{
			app.WaitForElement("OK", "Ok Alert Did Not Appear", TimeSpan.FromSeconds(timeoutInSeconds));
			app.Tap("OK");
			app.Screenshot("Tap OK On Alert");
		}

		public string GetAlertText(int numberSelected)
		{
			var alertTextQuery = app.Query($"You Selected Number {numberSelected}");
			return alertTextQuery?.FirstOrDefault()?.Text;
		}

		public void TapBackButton()
		{
			app.Back();
			app.Screenshot("Tap Back Button");
		}

		public void WaitForNoActivityIndicator(int timeoutInSeconds = 60)
		{
			app.WaitForNoElement(_loadingDataFromBackendActivityIndicator, "Activity Indicator Did Not Disappear", TimeSpan.FromSeconds(timeoutInSeconds));
		}
	}
}

