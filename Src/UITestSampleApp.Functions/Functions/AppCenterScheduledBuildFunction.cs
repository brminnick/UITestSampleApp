using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Polly;
using Refit;

namespace UITestSampleApp.Functions
{
    public static class AppCenterScheduledBuildFunction
    {
        #region Constant Fields
        const string _appCenterOwnerName = "bminnick";
        const string _appCenterAppName_iOS = "uitestsampleapp-1";
        const string _appCenterAppName_Android = "uitestsampleapp";
        const string _appCenterMasterBranchName = "master";
        const string _appCenterBaseUrl = "https://api.appcenter.ms";

        readonly static string _appCenterApiToken = Environment.GetEnvironmentVariable("AppCenterAPIToken");
        readonly static Lazy<IAppServiceAPI> _appServiceApiClientHolder = new Lazy<IAppServiceAPI>(() => RestService.For<IAppServiceAPI>(CreateHttpClient(TimeSpan.FromSeconds(5))));
        #endregion

        #region Properties
        static IAppServiceAPI AppServiceApiClient => _appServiceApiClientHolder.Value;
        #endregion

        #region Methods
        [FunctionName(nameof(AppCenterScheduledBuildFunction))]
        public static async Task Run([TimerTrigger("0 0 9 * * *")]TimerInfo myTimer, ILogger log)
        {
            var iOSBuildTask = ExecutePollyFunction(() => AppServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_iOS, _appCenterMasterBranchName, new BuildParameters(true)));
            var androidBuildTask = ExecutePollyFunction(() => AppServiceApiClient.QueueBuild(_appCenterOwnerName, _appCenterAppName_Android, _appCenterMasterBranchName, new BuildParameters(true)));

            try
            {
                await Task.WhenAll(iOSBuildTask, androidBuildTask).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
                throw;
            }

            var iOSBuildResponse = await iOSBuildTask.ConfigureAwait(false);
            var androidBuildResponse = await iOSBuildTask.ConfigureAwait(false);

            log.LogInformation($"{nameof(iOSBuildResponse)} {nameof(iOSBuildResponse.IsSuccessStatusCode)}: {iOSBuildResponse.IsSuccessStatusCode}");
            log.LogInformation($"{nameof(androidBuildResponse)} {nameof(androidBuildResponse.IsSuccessStatusCode)}: {androidBuildResponse.IsSuccessStatusCode}");
        }

        static HttpClient CreateHttpClient(TimeSpan timeout)
        {
            var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip });
            client.Timeout = timeout;
            client.DefaultRequestHeaders.Add("X-API-Token", _appCenterApiToken);
            client.BaseAddress = new Uri(_appCenterBaseUrl);

            return client;
        }

        static Task<T> ExecutePollyFunction<T>(Func<Task<T>> action, int numRetries = 5)
        {
            return Policy
                    .Handle<WebException>()
                    .Or<HttpRequestException>()
                    .Or<TimeoutException>()
                    .WaitAndRetryAsync
                    (
                        numRetries,
                        PollyRetryAttempt
                    ).ExecuteAsync(action);

            TimeSpan PollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }
        #endregion
    }
}
