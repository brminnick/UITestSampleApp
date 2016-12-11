using System;
using System.IO;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

using UITestSampleApp.iOS;

using Xamarin.Forms;

[assembly: Dependency(typeof(Environment_iOS))]
namespace UITestSampleApp.iOS
{
	public class Environment_iOS : IEnvironment
	{
		public string GetOperatingSystemVersion()
		{
			return UIDevice.CurrentDevice.SystemVersion;
		}

		public string GetFilePath(string fileName)
		{
			return Path.Combine(MobileServiceClient.DefaultDatabasePath, fileName);
		}

		public bool IsOperatingSystemSupported(int majorVersion, int minorVersion)
		{
			return UIDevice.CurrentDevice.CheckSystemVersion(majorVersion, minorVersion);
		}
	}
}

