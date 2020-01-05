using System.Net.Http;
using System.Threading.Tasks;

namespace UITestSampleApp.Functions
{
    public class AppCenterApiService
    {
        const string _appCenterOwnerName = "CDA-Global-BETA";
        const string _appCenterAppName_iOS = "uitestsampleapp-1";
        const string _appCenterAppName_Android = "uitestsampleapp";
        const string _appCenterMasterBranchName = "master";

        readonly IAppCenterAPI _appServiceApiClient;

        public AppCenterApiService(IAppCenterAPI appCenterServiceClient) => _appServiceApiClient = appCenterServiceClient;

        public Task<HttpResponseMessage> BuildiOSApp() => _appServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_iOS, _appCenterMasterBranchName, new BuildParameters(true));

        public Task<HttpResponseMessage> BuildAndroidApp() => _appServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_Android, _appCenterMasterBranchName, new BuildParameters(true));
    }
}
