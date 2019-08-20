using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace UITestSampleApp.Functions
{
    public static class AppCenterScheduledBuildFunction
    {
        [FunctionName(nameof(AppCenterScheduledBuildFunction))]
        public static async Task Run([TimerTrigger("0 0 9 * * *")]TimerInfo myTimer, ILogger log)
        {
            var iOSBuildTask = AppCenterApiService.BuildiOSApp();
            var androidBuildTask = AppCenterApiService.BuildAndroidApp();

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

            iOSBuildResponse.EnsureSuccessStatusCode();
            androidBuildResponse.EnsureSuccessStatusCode();
        }
    }
}
