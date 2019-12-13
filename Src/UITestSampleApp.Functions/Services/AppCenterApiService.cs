using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Refit;

namespace UITestSampleApp.Functions
{
    public class AppCenterApiService
    {
        const string _appCenterOwnerName = "CDA-Global-BETA";
        const string _appCenterAppName_iOS = "uitestsampleapp-1";
        const string _appCenterAppName_Android = "uitestsampleapp";
        const string _appCenterMasterBranchName = "master";

        public AppCenterApiService(AppCenterServiceClient appCenterServiceClient) => AppServiceApiClient = appCenterServiceClient.Client;

        IAppCenterAPI AppServiceApiClient { get; }

        public Task<HttpResponseMessage> BuildiOSApp() =>
            AttemptAndRetry(() => AppServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_iOS, _appCenterMasterBranchName, new BuildParameters(true)));

        public Task<HttpResponseMessage> BuildAndroidApp() =>
            AttemptAndRetry(() => AppServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_Android, _appCenterMasterBranchName, new BuildParameters(true)));

        static Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 3)
        {
            return Policy.Handle<Exception>().WaitAndRetryAsync(numRetries, pollyRetryAttempt).ExecuteAsync(action);

            static TimeSpan pollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }
    }

    public class AppCenterServiceClient : HttpClient
    {
        const string _appCenterBaseUrl = "https://api.appcenter.ms";
        readonly static string _appCenterApiToken = Environment.GetEnvironmentVariable("AppCenterAPIToken") ?? string.Empty;

        public AppCenterServiceClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-API-Token", _appCenterApiToken);
            client.BaseAddress = new Uri(_appCenterBaseUrl);
            client.DefaultRequestVersion = new Version(2, 0);

            Client = RestService.For<IAppCenterAPI>(client);
        }

        public IAppCenterAPI Client { get; }
    }
}
