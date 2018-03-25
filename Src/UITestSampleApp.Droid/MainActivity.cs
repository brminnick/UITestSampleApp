using Android.OS;
using Android.App;
using Android.Views;
using Android.Content.PM;

using Akavache;

using Firebase;

using Java.Interop;

using Plugin.CurrentActivity;

using Xamarin.Forms.Platform.Android.AppLinks;

namespace UITestSampleApp.Droid
{
	[Activity(Theme = "@style/MyTheme", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		#region Methods
		#region Xamarin Test Cloud Back Door Methods
#if DEBUG
		[Export("BypassLoginScreen")]
		public void BypassLoginScreen() =>
			BackdoorMethodHelpers.BypassLoginScreen();

		[Export("OpenListViewPage")]
		public void OpenListViewPage() =>
			BackdoorMethodHelpers.OpenListViewPage();

		[Export("GetListViewPageDataAsBase64String")]
		public string GetListViewPageDataAsBase64String() =>
			BackdoorMethodHelpers.GetListViewPageDataAsBase64String();
#endif
		#endregion

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			FirebaseApp.InitializeApp((Android.Content.Context)CrossCurrentActivity.Current);

			TabLayoutResource = Resource.Layout.tabs;
			ToolbarResource = Resource.Layout.toolbar;

			global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			AndroidAppLinks.Init(this);

			BlobCache.ApplicationName = "SimpleUITestApp";
			BlobCache.EnsureInitialized();

			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			EntryCustomReturn.Forms.Plugin.Android.CustomReturnEntryRenderer.Init();

			LoadApplication(new App());
		}
		#endregion
	}
}

