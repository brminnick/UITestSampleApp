using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
    public static class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .InstalledApp("com.minnick.uitestsampleapp")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .DeviceIdentifier("16448E96-55DE-4A80-A995-C133DF128704")
                .InstalledApp("com.minnick.uitestsampleapp")
                .StartApp();
        }
    }
}

