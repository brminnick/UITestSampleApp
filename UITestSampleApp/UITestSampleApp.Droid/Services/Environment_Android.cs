using System;
using System.IO;

using Android.OS;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;

using UITestSampleApp.Droid;

[assembly: Dependency(typeof(Environment_Android))]
namespace UITestSampleApp.Droid
{
	public class Environment_Android : IEnvironment
	{
		public string GetFilePath(string fileName)
		{
			return Path.Combine(MobileServiceClient.DefaultDatabasePath, fileName);
		}

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
				MobileCenterHelpers.Log("Operating System Check Failed", e.Message, e);
				return false;
			}
		}
	}
}

