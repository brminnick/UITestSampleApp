using System.Linq;

using Xamarin.UITest;
using Xamarin.UITest.Queries;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
	public class FirstPage : BasePage
	{
		readonly Query _goButton;
		readonly Query _textEntry;
		readonly Query _textLabel;
		readonly Query _listViewButton;

		readonly Query _goButtonUsingID;
		readonly Query _textEntryUsingID;
		readonly Query _textLabelUsingID;
		readonly Query _listViewButtonUsingID;
		readonly Query _activityIndicatorUsingID;

		public FirstPage(IApp app, Platform platform) : base(app, platform)
		{
			//Always initialize the UITest queries using "x.Marked" and referencing the UI ID
			//In Xamarin.Forms, set the UI ID by setting the control's "AutomationId"
			//In Xamarin.Android, set the UI ID by setting the control's "ContentDescription"
			//In Xamarin.iOS, set the UI ID by setting the control's "AccessibilityIdentifiers"
			_goButtonUsingID = x => x.Marked(AutomationIdConstants.GoButton);
			_textEntryUsingID = x => x.Marked(AutomationIdConstants.TextEntry);
			_textLabelUsingID = x => x.Marked(AutomationIdConstants.TextLabel);
			_listViewButtonUsingID = x => x.Marked(AutomationIdConstants.ListViewButton);
			_activityIndicatorUsingID = x => x.Marked(AutomationIdConstants.BusyActivityIndicator);

			//Below shows the improper way to initalize queries.
			//This code would break if a developer added a third button...
			//...to the screen and placed it above the "Go Button", because...
			//...the Go Button Index would change.
			if (OnAndroid)
			{
				_goButton = x => x.Class("AppCompatButton").Index(0);
				_textEntry = x => x.Class("EntryEditText");
				_listViewButton = x => x.Class("AppCompatButton").Index(1);
			}
			else if (OniOS)
			{
				_goButton = x => x.Class("UIButton").Index(1);
				_textEntry = x => x.Class("UITextField");
				_listViewButton = x => x.Class("UIButton").Index(0);
			}
		}

		public void EnterText(string text)
		{
			app.Tap(_textEntryUsingID);
			app.ClearText();
			app.EnterText(text);
			app.DismissKeyboard();
			app.Screenshot($"Enter Text, \"{text}\"");
		}

		public void ClickGo()
		{
			app.Tap(_goButtonUsingID);
			app.Screenshot("Click Go Button");
		}

		public void ClickListViewButton()
		{
			app.Tap(_listViewButtonUsingID);
			app.Screenshot("Click ListView Button");
		}

		public void WaitForNoActivityIndicator()
		{
			app.WaitForNoElement(_activityIndicatorUsingID);
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
			var entryFieldQuery = app.Query(_textEntryUsingID);
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

