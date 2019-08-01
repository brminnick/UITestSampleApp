using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;

namespace UITestSampleApp
{
    public static class AppCenterHelpers
    {
        public static void Start()
        {
            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.iOS:
                    Start(AppCenterConstants.AppCenteriOSApiKey);
                    break;
                case Xamarin.Forms.Device.Android:
                    Start(AppCenterConstants.AppCenterDroidApiKey);
                    break;
                default:
                    throw new NotSupportedException("Runtime Platform Not Supported");
            }
        }

        [Conditional("DEBUG")]
        public static void CrashApp() => Crashes.GenerateTestCrash();

        public static void TrackEvent(string trackIdentifier, IDictionary<string, string> table = null) =>
            Analytics.TrackEvent(trackIdentifier, table);

        public static void TrackEvent(string trackIdentifier, string key, string value)
        {
            IDictionary<string, string> table = new Dictionary<string, string> { { key, value } };

            if (string.IsNullOrWhiteSpace(key) && string.IsNullOrWhiteSpace(value))
                table = null;

            TrackEvent(trackIdentifier, table);
        }

        public static void LogException(Exception exception, IDictionary<string, string> properties = null, [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            PrintException(exception, callerMemberName, lineNumber, filePath, properties);

            Crashes.TrackError(exception, properties);
        }

        [Conditional("DEBUG")]
        static void PrintException(Exception exception, string callerMemberName, int lineNumber, string filePath, IDictionary<string, string> properties = null)
        {
            var fileName = System.IO.Path.GetFileName(filePath);

            Debug.WriteLine(exception.GetType());
            Debug.WriteLine($"Error: {exception.Message}");
            Debug.WriteLine($"Line Number: {lineNumber}");
            Debug.WriteLine($"Caller Name: {callerMemberName}");
            Debug.WriteLine($"File Name: {fileName}");

            if (properties != null)
            {
                foreach (var property in properties)
                    Debug.WriteLine($"{property.Key}: {property.Value}");
            }
        }

        static void Start(string appSecret) =>
            AppCenter.Start(appSecret, typeof(Analytics), typeof(Crashes));
    }
}
