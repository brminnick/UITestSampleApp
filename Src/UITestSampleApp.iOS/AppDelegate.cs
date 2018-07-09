using System.Diagnostics;

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

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(uiApplication, launchOptions);
		}

#if DEBUG
		#region Xamarin Test Cloud Back Door Methods
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
			BackdoorMethodHelpers.BypassLoginScreen();
			return new NSString();
		}

		[Export("openListViewPage:")]
		public NSString OpenListViewPage(NSString noValue)
		{
			BackdoorMethodHelpers.OpenListViewPage();
			return new NSString();
		}

		[Export("getListViewPageDataAsBase64String:")]
		public NSString GetListViewPageDataAsBase64String(NSString noValue) =>
			new NSString(BackdoorMethodHelpers.GetListViewPageDataAsBase64String());
		#endregion
#endif
	}
}

