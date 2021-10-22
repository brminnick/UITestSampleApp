using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace UITestSampleApp.Functions
{
    class AppCenterScheduledBuildFunction
    {
        readonly AppCenterApiService _appCenterApiService;

        public AppCenterScheduledBuildFunction(AppCenterApiService appCenterApiService) => _appCenterApiService = appCenterApiService;

        [Function(nameof(AppCenterScheduledBuildFunction))]
        public async Task Run([TimerTrigger("0 0 9 * * *")] TimerInfo myTimer, FunctionContext context)
        {
            var log = context.GetLogger<AppCenterScheduledBuildFunction>();

            var iOSBuildTask = _appCenterApiService.BuildiOSApp();
            var androidBuildTask = _appCenterApiService.BuildAndroidApp();

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
