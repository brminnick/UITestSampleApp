using System;

using Android.OS;

using UITestSampleApp.Droid;

using Xamarin.Forms;

[assembly: Dependency(typeof(Environment_Android))]
namespace UITestSampleApp.Droid
{
    public class Environment_Android : IEnvironment
    {
        public string GetOperatingSystemVersion() => Build.VERSION.Release;

        public bool IsOperatingSystemSupported(int majorVersion, int minorVersion)
        {
            try
            {
                double.TryParse(Build.VERSION.Release, out double sdkInt);

                return sdkInt >= (majorVersion + minorVersion * .1);

            }
            catch (Exception e)
            {
                AppCenterHelpers.LogException(e);
                return false;
            }
        }
    }
}

