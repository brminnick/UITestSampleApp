using System.Linq;

using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
	public class ListViewPage : BasePage
	{
		public ListViewPage(IApp app, Platform platform) : base(app, platform)
		{
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
	}
}

