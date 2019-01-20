using System;
using System.Collections.Generic;

using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Android;

using UITestSampleApp.Shared;

namespace UITestSampleApp.UITests
{
    static class BackdoorHelpers
    {
        internal static void CleariOSKeyChain(IApp app, string username)
        {
            switch (app)
            {
                case iOSApp iosApp:
                    iosApp.Invoke("clearKeyChain:", username);
                    break;
            }
        }

        internal static void BypassLoginScreen(IApp app)
        {
            switch (app)
            {
                case iOSApp iosApp:
                    iosApp.Invoke("bypassLoginScreen:", "");
                    break;
                case AndroidApp androidApp:
                    androidApp.Invoke("BypassLoginScreen");
                    break;
                default:
                    throw new NotSupportedException("Platform Not Supported");
            }

            app.Screenshot("Backdoor Bypass Login Screen");
        }

        internal static void OpenListViewPage(IApp app)
        {
            switch (app)
            {
                case iOSApp iosApp:
                    iosApp.Invoke("openListViewPage:", "");
                    break;
                case AndroidApp androidApp:
                    androidApp.Invoke("OpenListViewPage");
                    break;
                default:
                    throw new NotSupportedException("Platform Not Supported");
            }

            app.Screenshot("Backdoor to List View Page");
        }

        internal static List<ListPageDataModel> GetListPageData(IApp app)
        {
            string listPageDataAsBase64String;

            switch (app)
            {
                case iOSApp iosApp:
                    listPageDataAsBase64String = iosApp.Invoke("getSerializedListViewPageData:", "").ToString();
                    break;
                case AndroidApp androidApp:
                    listPageDataAsBase64String = androidApp.Invoke("GetSerializedListViewPageData").ToString();
                    break;
                default:
                    throw new NotSupportedException("Platform Not Supported");
            }

            return ConverterHelpers.DeserializeObject<List<ListPageDataModel>>(listPageDataAsBase64String);
        }
    }
}
