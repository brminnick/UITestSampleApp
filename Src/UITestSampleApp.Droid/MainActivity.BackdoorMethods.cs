#if DEBUG
using Android.Runtime;
using Java.Interop;
using UITestSampleApp.Shared;

namespace UITestSampleApp.Droid
{
    public partial class MainActivity
    {
        [Preserve, Export(BackdoorMethodConstants.BypassLoginScreen)]
        public void BypassLoginScreen() => BackdoorMethodHelpers.BypassLoginScreen();

        static string SerializeObject<T>(T value) => Newtonsoft.Json.JsonConvert.SerializeObject(value);
    }
}
#endif
