using UIKit;
using Foundation;

using Microsoft.Azure.Mobile.Distribute;

using EntryCustomReturn.Forms.Plugin.iOS;

namespace UITestSampleApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            Distribute.DontCheckForUpdatesInDebug();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            CustomReturnEntryRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            Distribute.OpenUrl(url);
            return base.OpenUrl(app, url, options);
        }

        #region Xamarin Test Cloud Back Door Methods
#if ENABLE_TEST_CLOUD
        [Export("clearKeyChain:")]
        public NSString ClearKeychain(NSString username)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject("username");
            KeychainHelpers.DeletePasswordForUsername(username, "XamarinExpenses", true);
            return new NSString();
        }

        [Export("bypassLoginScreen:")]
        public NSString BypassLoginScreen(NSString noValue)
        {
            BackdoorMethodHelpers.BypassLoginScreen().GetAwaiter().GetResult();
            return new NSString();
        }

        [Export("openListViewPage:")]
        public NSString OpenListViewPage(NSString noValue)
        {
            BackdoorMethodHelpers.OpenListViewPage().GetAwaiter().GetResult();
            return new NSString();
        }

        [Export("getListViewPageDataAsBase64String:")]
        public NSString GetListViewPageDataAsBase64String(NSString noValue) =>
            new NSString(BackdoorMethodHelpers.GetListViewPageDataAsBase64String());
#endif
        #endregion
    }
}

