using System.Linq;

using Xamarin.UITest;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using UITestSampleApp.Shared;

namespace UITestSampleApp.UITests
{
	public class ListPage : BasePage
	{
		readonly Query _loadingDataFromBackendActivityIndicator;

		public ListPage(IApp app, Platform platform) : base(app, platform)
		{
			_loadingDataFromBackendActivityIndicator = x => x.Marked(AutomationIdConstants.LoadingDataFromBackendActivityIndicator);
		}

		public void TapListItemNumber(int listItemNumber)
		{
			app.ScrollDownTo(listItemNumber.ToString());
			app.Tap(x => x.Marked(listItemNumber.ToString()));
			app.WaitForElement("OK");
			app.Screenshot($"Tap {listItemNumber} on List View Page");
		}

		public void TapOKOnAlert()
		{
			app.WaitForElement("OK");
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

		public void WaitForNoActivityIndicator()
		{
			app.WaitForNoElement(_loadingDataFromBackendActivityIndicator);
		}
	}
}

