using Xamarin.UITest;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
    abstract class BasePage
    {
        protected BasePage(IApp app, string pageTitle)
        {
            App = app;
            PageTitle = pageTitle;
        }

        public string PageTitle { get; }
        protected IApp App { get; }

        public virtual void WaitForPageToLoad() => App.WaitForElement(x => x.Marked(PageTitle));

        protected void EnterText(Query textEntryQuery, string text, bool shouldDismissKeyboard = true)
        {
            App.ClearText(textEntryQuery);
            App.EnterText(textEntryQuery, text);

            if (shouldDismissKeyboard)
                App.DismissKeyboard();
        }
    }
}

