#if DEBUG
using Foundation;
using UITestSampleApp.Shared;

namespace UITestSampleApp.iOS
{
    public partial class AppDelegate
    {
        public AppDelegate() => Xamarin.Calabash.Start();

        [Preserve, Export(BackdoorMethodConstants.ClearKeychain + ":")]
        public void ClearKeychain(NSString noValue) => Xamarin.Essentials.SecureStorage.RemoveAll();

        [Preserve, Export(BackdoorMethodConstants.BypassLoginScreen + ":")]
        public void BypassLoginScreen(NSString noValue) => BackdoorMethodHelpers.BypassLoginScreen();

        [Preserve, Export(BackdoorMethodConstants.OpenListViewPage + ":")]
        public async void OpenListViewPage(NSString noValue) => await BackdoorMethodHelpers.OpenListViewPage().ConfigureAwait(false);

        [Preserve, Export(BackdoorMethodConstants.GetListViewPageData + ":")]
        public NSString GetListViewPageData(NSString noValue) => SerializeObject(BackdoorMethodHelpers.GetListViewPageData());

        static NSString SerializeObject<T>(T value) => new NSString(Newtonsoft.Json.JsonConvert.SerializeObject(value));
    }
}
#endif