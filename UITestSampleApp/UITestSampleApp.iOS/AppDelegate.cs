using UIKit;
using Foundation;

using Microsoft.Azure.Mobile.Distribute;

using EntryCustomReturn.Forms.Plugin.iOS;

using MyLoginUI;

namespace UITestSampleApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		App App;

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

			LoadApplication(App = new App());

			return base.FinishedLaunching(app, options);
		}

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
			Distribute.OpenUrl(url);
            return base.OpenUrl(app, url, options);
        }

		#region Xamarin Test Cloud Back Door Methods
#if ENABLE_TEST_CLOUD
		[Export("xtcAgent:")]
		public NSString TurnOffTouchId(NSString noValue)
		{
			App.XTCAgent = true;
			return new NSString();
		}

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
			App.Navigation.PopAsync();
			return new NSString();
		}

		[Export("openListViewPage:")]
		public NSString OpenListViewPage(NSString noValue)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
				App.OpenListViewPageUsingDeepLinking();
			else
				App.OpenListViewPageUsingNavigation();
			return new NSString();
		}

		[Export("getListViewPageDataAsBase64String:")]
		public NSString GetListViewPageDataAsBase64String(NSString noValue)
		{
			var listPageData = App.GetListPageData();

			var listPageDataAsBase64String = ConverterHelpers.ConvertSerializableObjectToBase64String(listPageData);

			return new NSString(listPageDataAsBase64String);
		}
#endif
		#endregion
	}
}

