using System;
using System.Collections.Generic;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Distribute;

namespace UITestSampleApp
{
    public enum MobileCenterLogType
    {
        Assert, Debug, Error, Info, Verbose, Warn
    }

    public static class AppCenterHelpers
    {
        public static void Start()
        {
            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.iOS:
                    Start(MobileCenterConstants.MobileCenteriOSApiKey);
                    break;
                case Xamarin.Forms.Device.Android:
                    Start(MobileCenterConstants.MobileCenterDroidApiKey);
                    break;
                default:
                    throw new NotSupportedException("Runtime Platform Not Supported");
            }
        }

        public static void TrackEvent(string trackIdentifier, IDictionary<string, string> table = null) =>
            Analytics.TrackEvent(trackIdentifier, table);

        public static void TrackEvent(string trackIdentifier, string key, string value)
        {
            IDictionary<string, string> table = new Dictionary<string, string> { { key, value } };

            if (string.IsNullOrWhiteSpace(key) && string.IsNullOrWhiteSpace(value))
                table = null;

            TrackEvent(trackIdentifier, table);
        }

        public static void Log(Exception exception, MobileCenterLogType type = MobileCenterLogType.Warn)
        {
            var exceptionType = exception.GetType().ToString();
            var message = exception.Message;

            System.Diagnostics.Debug.WriteLine(exceptionType);
            System.Diagnostics.Debug.WriteLine($"Error: {message}");

            switch (type)
            {
                case MobileCenterLogType.Info:
                    MobileCenterLog.Info(exceptionType, message, exception);
                    break;
                case MobileCenterLogType.Warn:
                    MobileCenterLog.Warn(exceptionType, message, exception);
                    break;
                case MobileCenterLogType.Error:
                    MobileCenterLog.Error(exceptionType, message, exception);
                    break;
                case MobileCenterLogType.Assert:
                    MobileCenterLog.Assert(exceptionType, message, exception);
                    break;
                case MobileCenterLogType.Verbose:
                    MobileCenterLog.Verbose(exceptionType, message, exception);
                    break;
                case MobileCenterLogType.Debug:
                    MobileCenterLog.Debug(exceptionType, message, exception);
                    break;
                default:
                    throw new Exception("MobileCenterLogType Does Not Exist");
            }
        }

        static void Start(string appSecret) =>
            MobileCenter.Start(appSecret, typeof(Analytics), typeof(Crashes), typeof(Distribute));
    }
}
