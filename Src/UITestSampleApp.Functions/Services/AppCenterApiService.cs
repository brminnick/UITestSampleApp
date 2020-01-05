using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;

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

        public Task<HttpResponseMessage> BuildiOSApp() =>
            AttemptAndRetry(() => _appServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_iOS, _appCenterMasterBranchName, new BuildParameters(true)));

        public Task<HttpResponseMessage> BuildAndroidApp() =>
            AttemptAndRetry(() => _appServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_Android, _appCenterMasterBranchName, new BuildParameters(true)));

        static Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 3)
        {
            return Policy.Handle<Exception>().WaitAndRetryAsync(numRetries, pollyRetryAttempt).ExecuteAsync(action);

            static TimeSpan pollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }
    }
}
