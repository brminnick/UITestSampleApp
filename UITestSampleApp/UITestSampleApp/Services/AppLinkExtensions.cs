using System;

using Xamarin.Forms;

namespace UITestSampleApp
{
	public static class AppLinkExtensions
	{
		public const string BaseUrl = "http://simplueuitestapp.minnick.com/session/";

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
	}
}

