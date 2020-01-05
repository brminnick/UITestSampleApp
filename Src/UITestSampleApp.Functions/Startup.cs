using System;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using UITestSampleApp.Functions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace UITestSampleApp.Functions
{
    public class Startup : FunctionsStartup
    {
        const string _appCenterBaseUrl = "https://api.appcenter.ms";
        readonly static string _appCenterApiToken = Environment.GetEnvironmentVariable("AppCenterAPIToken") ?? string.Empty;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddRefitClient<IAppCenterAPI>()
                .ConfigureHttpClient(client =>
                {
                    client.DefaultRequestHeaders.Add("X-API-Token", _appCenterApiToken);
                    client.BaseAddress = new Uri(_appCenterBaseUrl);
                    client.DefaultRequestVersion = new Version(2, 0);
                })
                .ConfigurePrimaryHttpMessageHandler(config => new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, sleepDurationProvider));

            builder.Services.AddSingleton<AppCenterApiService>();

            static TimeSpan sleepDurationProvider(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }
    }
}
