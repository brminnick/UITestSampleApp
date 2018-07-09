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
        public string GetOperatingSystemVersion() =>Build.VERSION.Release;

        public bool IsOperatingSystemSupported(int majorVersion, int minorVersion)
        {
            try
            {
                double.TryParse(Build.VERSION.Release, out double sdkInt);

                return sdkInt >= (majorVersion + minorVersion * .1);

            }
            catch (Exception e)
            {
                AnalyticsHelpers.LogException(e);
                return false;
            }
        }
    }
}

