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

        static NSString SerializeObject<T>(T value) => new NSString(Newtonsoft.Json.JsonConvert.SerializeObject(value));
    }
}
#endif