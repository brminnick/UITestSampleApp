using System;
using System.Linq;
using System.Threading;

using Xamarin.UITest;

using UITestSampleApp.Shared;

namespace UITestSampleApp.UITests
{
    public class ListPage : BasePage
    {
        public ListPage(IApp app, Platform platform) : base(app, platform, PageTitleConstants.ListPage)
        {
        }

        public bool IsRefreshActivityIndicatorDisplayed
        {
            get
            {
                if (OnAndroid)
                    return (bool)App.Query(x => x.Class("SwipeRefreshLayout").Invoke("isRefreshing")).FirstOrDefault();

                if (OniOS)
                    return App.Query(x => x.Class("UIRefreshControl")).Any();

                throw new Exception("Platform Not Recognized");
            }
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

        public void WaitForNoActivityIndicator(int timeoutInSeconds = 25)
        {
            int counter = 0;
            while (IsRefreshActivityIndicatorDisplayed && counter < timeoutInSeconds)
            {
                Thread.Sleep(1000);
                counter++;

                if (counter >= timeoutInSeconds)
                    throw new Exception($"Loading the list took longer than {timeoutInSeconds}");
            }
        }
    }
}

