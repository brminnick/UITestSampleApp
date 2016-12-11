using System.Linq;

using Xamarin.UITest;
using Xamarin.UITest.Queries;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
	public class FirstPage : BasePage
	{
		protected readonly Query GoButton;
		protected readonly Query TextEntry;
		protected readonly Query TextLabel;
		protected readonly Query ListViewButton;

		protected readonly Query GoButtonUsingID;
		protected readonly Query TextEntryUsingID;
		protected readonly Query TextLabelUsingID;
		protected readonly Query ListViewButtonUsingID;
		protected readonly Query ActivityIndicatorUsingID;

		public FirstPage(IApp app, Platform platform) : base(app, platform)
		{
			//Always initialize the UITest queries using "x.Marked" and referencing the UI ID
			//In Xamarin.Forms, set the UI ID by setting the control's "AutomationId"
			//In Xamarin.Android, set the UI ID by setting the control's "ContentDescription"
			//In Xamarin.iOS, set the UI ID by setting the control's "AccessibilityIdentifiers"
			GoButtonUsingID = x => x.Marked(AutomationIdConstants.GoButton);
			TextEntryUsingID = x => x.Marked(AutomationIdConstants.TextEntry);
			TextLabelUsingID = x => x.Marked(AutomationIdConstants.TextLabel);
			ListViewButtonUsingID = x => x.Marked(AutomationIdConstants.ListViewButton);
			ActivityIndicatorUsingID = x => x.Marked(AutomationIdConstants.BusyActivityIndicator);

			//Below shows the improper way to initalize queries.
			//This code would break if a developer added a third button...
			//...to the screen and placed it above the "Go Button", because...
			//...the Go Button Index would change.
			if (OnAndroid)
			{
				GoButton = x => x.Class("AppCompatButton").Index(0);
				TextEntry = x => x.Class("EntryEditText");
				ListViewButton = x => x.Class("AppCompatButton").Index(1);
			}
			else if (OniOS)
			{
				GoButton = x => x.Class("UIButton").Index(1);
				TextEntry = x => x.Class("UITextField");
				ListViewButton = x => x.Class("UIButton").Index(0);
			}
		}

		public void EnterText(string text)
		{
			app.Tap(TextEntryUsingID);
			app.ClearText();
			app.EnterText(text);
			app.DismissKeyboard();
			app.Screenshot($"Enter Text, \"{text}\"");
		}

		public void ClickGo()
		{
			app.Tap(GoButtonUsingID);
			app.Screenshot("Click Go Button");
		}

		public void ClickListViewButton()
		{
			app.Tap(ListViewButtonUsingID);
			app.Screenshot("Click ListView Button");
		}

		public void WaitForNoActivityIndicator()
		{
			app.WaitForNoElement(ActivityIndicatorUsingID);
			app.Screenshot("Activity Indicator Stopped Spinning");
		}

		public void RotateScreenToLandscape()
		{
			app.SetOrientationLandscape();
			app.Screenshot("Rotate Device to Landscape");
		}

		public void RotateScreenToPortrait()
		{
			app.SetOrientationPortrait();
			app.Screenshot("Rotate Device to Portrait");
		}

		public string GetEntryFieldText()
		{
			var entryFieldQuery = app.Query(TextEntryUsingID);
			return entryFieldQuery?.FirstOrDefault()?.Text;
		}

		public string GetTitle()
		{
			var title = "First Page";
			AppResult[] titleQuery;

			app.WaitForElement(title);

			if(OniOS)
				titleQuery = app.Query(x=>x.Class("UILabel").Marked("First Page"));
			else
				titleQuery = app.Query(x => x.Class("TextView").Marked("First Page"));

			return titleQuery?.FirstOrDefault()?.Text;
		}
	}
}

