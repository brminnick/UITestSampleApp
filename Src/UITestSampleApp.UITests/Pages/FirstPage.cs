using System;
using System.Linq;

using Xamarin.UITest;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;

namespace UITestSampleApp.UITests
{
    class FirstPage : BasePage
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

        public FirstPage(IApp app) : base(app, PageTitleConstants.FirstPage)
        {
            //Always initialize the UITest queries using "x.Marked" and referencing the UI ID
            //In Xamarin.Forms, set the UI ID by setting the control's "AutomationId"
            //In Xamarin.Android, set the UI ID by setting the control's "ContentDescription"
            //In Xamarin.iOS, set the UI ID by setting the control's "AccessibilityIdentifiers"
            _goButtonUsingID = x => x.Marked(AutomationIdConstants.FirstPage_GoButton);
            _textEntryUsingID = x => x.Marked(AutomationIdConstants.FirstPage_TextEntry);
            _textLabelUsingID = x => x.Marked(AutomationIdConstants.FirstPage_TextLabel);
            _listViewButtonUsingID = x => x.Marked(AutomationIdConstants.FirstPage_ListViewButton);
            _activityIndicatorUsingID = x => x.Marked(AutomationIdConstants.FirstPage_BusyActivityIndicator);

            //Below shows the improper way to initalize queries.
            //This code would break if a developer added a third button...
            //...to the screen and placed it above the "Go Button", because...
            //...the Go Button Index would change.
            switch (app)
            {
                case AndroidApp androidApp:
                    _goButton = x => x.Class("AppCompatButton").Index(0);
                    _textEntry = x => x.Class("EntryEditText");
                    _listViewButton = x => x.Class("AppCompatButton").Index(1);
                    _textLabel = x => x.Class("AppCompatTextView").Index(0);
                    break;

                case iOSApp iosApp:
                    _goButton = x => x.Class("UIButton").Index(1);
                    _textEntry = x => x.Class("UITextField");
                    _listViewButton = x => x.Class("UIButton").Index(0);
					_textLabel = x => x.Class("UILabel").Index(0);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        public void EnterTextAndPressEnter(string text)
        {
            App.Tap(_textEntryUsingID);
            App.ClearText();
            App.EnterText(text);
            App.PressEnter();
            App.Screenshot($"Entered Text: {text}");
        }

        public void EnterText(string text)
        {
            App.Tap(_textEntryUsingID);
            App.ClearText();
            App.EnterText(text);
            App.DismissKeyboard();
            App.Screenshot($"Entered Text: {text}");
        }

        public void ClickGo()
        {
            App.Tap(_goButtonUsingID);
            App.Screenshot("Click Go Button");
        }

        public void ClickListViewButton()
        {
            App.Tap(_listViewButtonUsingID);
            App.Screenshot("Click ListView Button");
        }

        public void WaitForNoActivityIndicator(int timeoutInSeconds = 60)
        {
            App.WaitForNoElement(_activityIndicatorUsingID, "Activity Indicator Did Not Disappear", TimeSpan.FromSeconds(timeoutInSeconds));
            App.Screenshot("Activity Indicator Stopped Spinning");
        }

        public void RotateScreenToLandscape()
        {
            App.SetOrientationLandscape();
            App.Screenshot("Rotate Device to Landscape");
        }

        public void RotateScreenToPortrait()
        {
            App.SetOrientationPortrait();
            App.Screenshot("Rotate Device to Portrait");
        }

        public string GetEntryFieldText()
        {
            var entryFieldQuery = App.Query(_textEntryUsingID);
            return entryFieldQuery?.FirstOrDefault()?.Text;
        }


    }
}

