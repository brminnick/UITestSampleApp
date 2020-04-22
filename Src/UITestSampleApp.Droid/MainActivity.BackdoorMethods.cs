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

        [Preserve, Export(BackdoorMethodConstants.OpenListViewPage)]
        public async void OpenListViewPage() => await BackdoorMethodHelpers.OpenListViewPage().ConfigureAwait(false);

        [Preserve, Export(BackdoorMethodConstants.GetListViewPageData)]
        public string GetListViewPageData() => SerializeObject(BackdoorMethodHelpers.GetListViewPageData());

        static string SerializeObject<T>(T value) => Newtonsoft.Json.JsonConvert.SerializeObject(value);
    }
}
#endif
