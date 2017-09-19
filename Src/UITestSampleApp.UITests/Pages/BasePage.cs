using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
    public abstract class BasePage
    {
        protected readonly IApp App;
        protected readonly bool OnAndroid, OniOS;

        string _pageTitle;

		protected BasePage(IApp app, Platform platform, string pageTitle)
        {
            App = app;

            OnAndroid = platform == Platform.Android;
            OniOS = platform == Platform.iOS;

            _pageTitle = pageTitle;
        }

        public virtual void WaitForPageToLoad() => App.WaitForElement(x => x.Marked(_pageTitle));
    }
}

