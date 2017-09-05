using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{

    public class App : Application
    {
        #region Constructors
        public App()
        {
            DependencyService.Register<IDataService, AzureService>();

            var page = new LoginPage { LogoFileImageSource = "xamarin_logo" };
            Navigation = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#3498db"),
                BarTextColor = Color.White,
            };

            NavigationPage.SetHasNavigationBar(page, false);
            MainPage = Navigation;
        }
        #endregion

        #region Properties
        public static bool IsLoggedIn { get; set; }
        public static string UserName { get; set; }

        public static NavigationPage Navigation { get; set; }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            MobileCenterHelpers.Start();

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

#if DEBUG
        #region Backdoor Methods
        public void OpenListViewPageUsingDeepLinking()
        {
            OnAppLinkRequestReceived(new Uri($"{AppLinkHelpers.BaseUrl}{DeepLinkingIdConstants.ListPageId}"));
        }
        #endregion
#endif
        #endregion
    }
}


