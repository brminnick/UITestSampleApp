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
                .InstalledApp("com.minnick.uitestsampleapp")
                .StartApp();
        }
    }
}

