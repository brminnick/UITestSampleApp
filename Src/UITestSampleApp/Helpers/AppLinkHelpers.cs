using System;

using Xamarin.Forms;

namespace UITestSampleApp
{
    public static class AppLinkHelpers
    {
        #region Properties
        public static string BaseUrl => "http://uitestsampleapp.minnick.com/";
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

            if (Device.RuntimePlatform is Device.iOS 
                && !string.IsNullOrEmpty(iconName))
                entry.Thumbnail = ImageSource.FromFile(iconName);

            return entry;
        }
    }
}

