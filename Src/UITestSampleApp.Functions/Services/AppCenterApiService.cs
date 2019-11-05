using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Refit;

namespace UITestSampleApp.Functions
{
    public static class AppCenterApiService
    {
        const string _appCenterOwnerName = "CDA-Global-BETA";
        const string _appCenterAppName_iOS = "uitestsampleapp-1";
        const string _appCenterAppName_Android = "uitestsampleapp";
        const string _appCenterMasterBranchName = "master";
        const string _appCenterBaseUrl = "https://api.appcenter.ms";

        readonly static string _appCenterApiToken = Environment.GetEnvironmentVariable("AppCenterAPIToken");
        readonly static Lazy<IAppServiceAPI> _appServiceApiClientHolder = new Lazy<IAppServiceAPI>(() => RestService.For<IAppServiceAPI>(CreateHttpClient(_appCenterBaseUrl)));

        static IAppServiceAPI AppServiceApiClient => _appServiceApiClientHolder.Value;

        public static Task<HttpResponseMessage> BuildiOSApp() =>
            AttemptAndRetry(() => AppServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_iOS, _appCenterMasterBranchName, new BuildParameters(true)));

        public static Task<HttpResponseMessage> BuildAndroidApp() =>
            AttemptAndRetry(() => AppServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_Android, _appCenterMasterBranchName, new BuildParameters(true)));

        static HttpClient CreateHttpClient(in string baseUrl)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-Token", _appCenterApiToken);
            client.BaseAddress = new Uri(baseUrl);

            return client;
        }

        static Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 3)
        {
            return Policy.Handle<Exception>().WaitAndRetryAsync(numRetries, pollyRetryAttempt).ExecuteAsync(action);

            static TimeSpan pollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }
    }
}
