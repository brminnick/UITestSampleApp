using UIKit;

using UITestSampleApp.iOS;

using Xamarin.Forms;

[assembly: Dependency(typeof(Environment_iOS))]
namespace UITestSampleApp.iOS
{
	public class Environment_iOS : IEnvironment
	{
		public string GetOperatingSystemVersion() => 
            UIDevice.CurrentDevice.SystemVersion;

		public bool IsOperatingSystemSupported(int majorVersion, int minorVersion)=>
            UIDevice.CurrentDevice.CheckSystemVersion(majorVersion, minorVersion);
	}
}

