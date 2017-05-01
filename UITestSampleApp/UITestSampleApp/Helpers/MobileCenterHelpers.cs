using System;
using System.Collections.Generic;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Distribute;

namespace UITestSampleApp
{
	public static class MobileCenterHelpers
	{
		public static void Start(string appSecret)
		{
			MobileCenter.Start(appSecret, typeof(Analytics), typeof(Crashes), typeof(Distribute));
#if DEBUG
			Distribute.Enabled = false;
#else
			Distribute.Enabled = true;
#endif	
			Analytics.Enabled = true;
		}

		public static void TrackEvent(string trackIdentifier, IDictionary<string, string> table = null)
		{
			if (MobileCenter.Enabled && Analytics.Enabled)
				Analytics.TrackEvent(trackIdentifier, table);
		}

		public static void Log(string tag, string message, Exception exception = null, MobileCenterLogType type = MobileCenterLogType.Warn)
		{
			System.Diagnostics.Debug.WriteLine(exception.GetType());
			System.Diagnostics.Debug.WriteLine($"Error: {exception.Message}");

			switch (type)
			{
				case MobileCenterLogType.Info:
					MobileCenterLog.Info(tag, message, exception);
					break;
				case MobileCenterLogType.Warn:
					MobileCenterLog.Warn(tag, message, exception);
					break;
				case MobileCenterLogType.Error:
					MobileCenterLog.Error(tag, message, exception);
					break;
				case MobileCenterLogType.Assert:
					MobileCenterLog.Assert(tag, message, exception);
					break;
				case MobileCenterLogType.Verbose:
					MobileCenterLog.Verbose(tag, message, exception);
					break;
				case MobileCenterLogType.Debug:
					MobileCenterLog.Debug(tag, message, exception);
					break;
				default:
					throw new Exception("MobileCenterLogType Does Not Exist");
			}
		}
	}

	public enum MobileCenterLogType
	{
		Assert, Debug, Error, Info, Verbose, Warn
	}

}
