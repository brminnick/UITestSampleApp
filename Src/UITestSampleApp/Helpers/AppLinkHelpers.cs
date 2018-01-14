using System;

using Xamarin.Forms;

namespace UITestSampleApp
{
    public static class AppLinkHelpers
    {
        #region Properties
        public static string BaseUrl => "http://simplueuitestapp.minnick.com/session/";
        public static bool IsDeepLinkingSupported => GetIsDeepLinkingSupported();
        #endregion

        public static AppLinkEntry CreateAppLink(string title, string description, string id, string iconName = "")
        {
            var url = $"{BaseUrl}{id}";

            var entry = new AppLinkEntry
            {
                Title = title,
                Description = description,
                AppLinkUri = new Uri(url, UriKind.RelativeOrAbsolute),
                IsLinkActive = true
            };

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    if (!string.IsNullOrEmpty(iconName))
                        entry.Thumbnail = ImageSource.FromFile(iconName);
                    break;
            }

            entry.KeyValues.Add("contentType", "Session");
            entry.KeyValues.Add("appName", "SimpleUITestApp");
            entry.KeyValues.Add("companyName", "Minnick");

            return entry;
        }

        static bool GetIsDeepLinkingSupported()
        {
            int majorVersion, minorVersion;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    majorVersion = 9;
                    minorVersion = 0;
                    break;

                case Device.Android:
                    majorVersion = 4;
                    minorVersion = 2;
                    break;

                default:
                    throw new NotSupportedException("Runtime Platform Not Supported");
            }

            return DependencyService.Get<IEnvironment>().IsOperatingSystemSupported(majorVersion, minorVersion);
        }
    }
}

