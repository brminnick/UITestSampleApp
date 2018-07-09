using System;
using System.Diagnostics;
using System.Collections.Generic;
using SharpRaven;

namespace UITestSampleApp
{
    public static class AnalyticsHelpers
    {
        #region Constant Fields
        readonly static Lazy<RavenClient> _ravenClientHolder = new Lazy<RavenClient>(() => new RavenClient("https://cd4cb9b3afe34881ae3bf8057d6b54e6@sentry.io/1240065"));
        #endregion

        #region Properties
        static RavenClient RavenClient => _ravenClientHolder.Value;
        #endregion

        public static void Start()
        {
         
        }

        [Conditional("DEBUG")]
        public static void CrashApp() => throw new Exception("Auto-Generated Exception");

        public static void TrackEvent(string trackIdentifier, IDictionary<string, string> table = null) =>
            RavenClient.AddTrail(new SharpRaven.Data.Breadcrumb(trackIdentifier) { Data = table });

        public static void TrackEvent(string trackIdentifier, string key, string value)
        {
            IDictionary<string, string> table = new Dictionary<string, string> { { key, value } };

            if (string.IsNullOrWhiteSpace(key) && string.IsNullOrWhiteSpace(value))
                table = null;

            TrackEvent(trackIdentifier, table);
        }

        public static void LogException(Exception exception, IDictionary<string, string> properties = null)
        {
            var exceptionType = exception.GetType().ToString();
            var message = exception.Message;

            Debug.WriteLine(exceptionType);
            Debug.WriteLine($"Error: {message}");

            RavenClient.Capture(new SharpRaven.Data.SentryEvent(exception));
        }
    }
}
