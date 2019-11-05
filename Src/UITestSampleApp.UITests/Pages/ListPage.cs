using System;
using System.Linq;
using System.Threading;
using UITestSampleApp.Shared;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;

namespace UITestSampleApp.UITests
{
    class ListPage : BasePage
    {
        public ListPage(IApp app) : base(app, PageTitleConstants.ListPage)
        {
        }

        public bool IsRefreshActivityIndicatorDisplayed => App switch
        {
            AndroidApp androidApp => (bool)androidApp.Query(x => x.Class("RefreshViewRenderer").Invoke("isRefreshing")).First(),
            iOSApp iosApp => iosApp.Query(x => x.Class("UIRefreshControl")).Any(),
            _ => throw new NotSupportedException(),
        };

        public override void WaitForPageToLoad()
        {
            base.WaitForPageToLoad();

            App.WaitForElement("Number", "List View Elements Did Not Load", TimeSpan.FromSeconds(60));
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

        public string GetAlertText(int numberSelected) =>
            App.Query($"You Selected Number {numberSelected}").First().Text;

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

