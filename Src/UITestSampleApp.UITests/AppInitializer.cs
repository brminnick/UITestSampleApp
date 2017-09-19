using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
    public static class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform is Platform.Android)
                return ConfigureApp.Android.StartApp();

            return ConfigureApp.iOS.StartApp();
        }
    }
}

