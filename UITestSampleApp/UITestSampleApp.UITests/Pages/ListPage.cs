using System;
using System.Linq;
using System.Threading;

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
			App.ScrollDownTo(listItemNumber.ToString());
			App.Tap(x => x.Marked(listItemNumber.ToString()));
			App.WaitForElement("OK", "Ok Alert Did Not Appear", TimeSpan.FromSeconds(timeoutInSeconds));
			App.Screenshot($"Tap {listItemNumber} on List View Page");
		}

		public void TapOKOnAlert(int timeoutInSeconds = 60)
		{
			App.WaitForElement("OK", "Ok Alert Did Not Appear", TimeSpan.FromSeconds(timeoutInSeconds));
			App.Tap("OK");
			App.Screenshot("Tap OK On Alert");
		}

		public string GetAlertText(int numberSelected)
		{
			var alertTextQuery = App.Query($"You Selected Number {numberSelected}");
			return alertTextQuery?.FirstOrDefault()?.Text;
		}

		public void TapBackButton()
		{
			App.Back();
			App.Screenshot("Tap Back Button");
		}

		public void WaitForNoActivityIndicator(int timeoutInSeconds = 60)
		{
			Thread.Sleep(1000);
			App.WaitForNoElement(_loadingDataFromBackendActivityIndicator, "Activity Indicator Did Not Disappear", TimeSpan.FromSeconds(timeoutInSeconds));
		}
	}
}

