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

			Microsoft.AppCenter.Distribute.Distribute.DontCheckForUpdatesInDebug();

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(uiApplication, launchOptions);
		}

#if DEBUG
		#region Xamarin Test Cloud Back Door Methods
		[Preserve, Export("clearKeyChain:")]
		public NSString ClearKeychain(NSString username)
		{
            Xamarin.Essentials.SecureStorage.RemoveAll();
			return new NSString();
		}

        [Preserve, Export("bypassLoginScreen:")]
		public NSString BypassLoginScreen(NSString noValue)
		{
			BackdoorMethodHelpers.BypassLoginScreen();
			return new NSString();
		}

        [Preserve, Export("openListViewPage:")]
		public NSString OpenListViewPage(NSString noValue)
		{
			BackdoorMethodHelpers.OpenListViewPage();
			return new NSString();
		}

        [Preserve, Export("getSerializedListViewPageData:")]
		public NSString GetSerializedListViewPageData(NSString noValue) =>
			new NSString(BackdoorMethodHelpers.GetSerializedListViewPageData());
		#endregion
#endif
	}
}

