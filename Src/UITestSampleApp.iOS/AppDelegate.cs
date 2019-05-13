using UIKit;
using Foundation;

namespace UITestSampleApp.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();

#if DEBUG
            Xamarin.Calabash.Start();
#endif

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

#if DEBUG
        #region Xamarin Test Cloud Back Door Methods
        [Preserve, Export("clearKeyChain:")]
        public void ClearKeychain(NSString noValue) => Xamarin.Essentials.SecureStorage.RemoveAll();

        [Preserve, Export("bypassLoginScreen:")]
        public void BypassLoginScreen(NSString noValue) => BackdoorMethodHelpers.BypassLoginScreen();

        [Preserve, Export("openListViewPage:")]
        public void OpenListViewPage(NSString noValue) => BackdoorMethodHelpers.OpenListViewPage();

        [Preserve, Export("getSerializedListViewPageData:")]
        public NSString GetSerializedListViewPageData(NSString noValue) => new NSString(BackdoorMethodHelpers.GetSerializedListViewPageData());
        #endregion
#endif
    }
}

