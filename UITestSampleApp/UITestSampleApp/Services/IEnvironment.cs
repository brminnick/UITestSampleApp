using System;
namespace UITestSampleApp
{
	public interface IEnvironment
	{
		string GetOperatingSystemVersion();
		bool IsOperatingSystemSupported(int majorVersion, int minorVersion);
		string GetFilePath(string fileName);
	}
}

