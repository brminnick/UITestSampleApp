using System;

using Xamarin.Forms;

namespace UITestSampleApp
{

    public class App : Application
    {
        public App()
        {
            var page = new LoginPage();
            NavigationPage.SetHasNavigationBar(page, false);

            MainPage = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#3498db"),
                BarTextColor = Color.White,
            };
        }
        protected override void OnStart()
        {
            AppCenterHelpers.Start();

            RegisterAppLinks();
        }

        protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            if (uri.ToString().Equals($"{AppLinkHelpers.BaseUrl}{DeepLinkingIdConstants.ListPageId}"))
                await AppLinkHelpers.NavigateToListViewPage();

            base.OnAppLinkRequestReceived(uri);
        }

        void RegisterAppLinks()
        {
            var listViewPageLink = AppLinkHelpers.CreateAppLink("List View Page", "Open the List View Page", DeepLinkingIdConstants.ListPageId, "icon");
            AppLinks.RegisterLink(listViewPageLink);
        }
    }
}


