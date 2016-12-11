using System;

using Android.OS;

using UITestSampleApp.Droid;

using Xamarin.Forms;

[assembly: Dependency(typeof(Environment_Android))]
namespace UITestSampleApp.Droid
{
	public class Environment_Android : IEnvironment
	{
		public string GetOperatingSystemVersion()
		{
			return Build.VERSION.Release;
		}

		public bool IsOperatingSystemSupported(int majorVersion, int minorVersion)
		{
			try
			{
				double sdkInt;
				double.TryParse(Build.VERSION.Release, out sdkInt);

				return sdkInt >= (majorVersion + minorVersion * .1);

			}
			catch (Exception e)
			{
				AnalyticsHelpers.Log("Operating System Check Failed", e.Message, e);
				return false;
			}
		}
	}
}

