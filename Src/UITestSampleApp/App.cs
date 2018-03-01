using System;

using Xamarin.Forms;

namespace UITestSampleApp
{

    public class App : Application
    {
        #region Constructors
        public App()
        {
            var page = new LoginPage();
            NavigationPage.SetHasNavigationBar(page, false);

            MainPage = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#3498db"),
                BarTextColor = Color.White,
            }; ;
        }
        #endregion

        #region Methods
#if DEBUG
        #region Backdoor Methods
        public void OpenListViewPageUsingDeepLinking() =>
            OnAppLinkRequestReceived(new Uri($"{AppLinkHelpers.BaseUrl}{DeepLinkingIdConstants.ListPageId}"));
        #endregion
#endif
        #endregion
        protected override void OnStart()
        {
            AppCenterHelpers.Start();

            RegisterAppLinks();
        }

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            if (uri.ToString().Equals($"{AppLinkHelpers.BaseUrl}{DeepLinkingIdConstants.ListPageId}"))
                BackdoorMethodHelpers.NavigateToListViewPage();

            base.OnAppLinkRequestReceived(uri);
        }

        void RegisterAppLinks()
        {
            if (!AppLinkHelpers.IsDeepLinkingSupported)
                return;

            var listViewPageLink = AppLinkHelpers.CreateAppLink("List View Page", "Open the List View Page", DeepLinkingIdConstants.ListPageId, "icon");
            AppLinks.RegisterLink(listViewPageLink);
        }
    }
}


