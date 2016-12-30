using System;
using System.IO;

using Android.App;
using Android.Runtime;

using UITestSampleApp.Shared;

namespace UITestSampleApp.Droid
{
	[Application]
	public class UITestSampleApp_Application : Application
	{
		public UITestSampleApp_Application(IntPtr handle, JniHandleOwnership transer)
		  : base(handle, transer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

            AnalyticsHelpers.Start(AnalyticsConstants.MobileCenterDroidApiKey);
		}
	}
}