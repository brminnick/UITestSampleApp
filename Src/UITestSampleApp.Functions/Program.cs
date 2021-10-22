using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Refit;

namespace UITestSampleApp.Functions
{
    class Program
    {
        const string _appCenterBaseUrl = "https://api.appcenter.ms";
        readonly static string _appCenterApiToken = Environment.GetEnvironmentVariable("AppCenterAPIToken") ?? string.Empty;

        static Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddCommandLine(args))
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddLogging();

                    services.AddSingleton<AppCenterApiService>();

                    services.AddRefitClient<IAppCenterAPI>()
                        .ConfigureHttpClient(client =>
                         {
                             client.DefaultRequestHeaders.Add("X-API-Token", _appCenterApiToken);
                             client.BaseAddress = new Uri(_appCenterBaseUrl);
                             client.DefaultRequestVersion = new Version(2, 0);
                         })
                        .ConfigurePrimaryHttpMessageHandler(config => new HttpClientHandler { AutomaticDecompression = getDecompressionMethods() })
                        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, sleepDurationProvider));

                    static TimeSpan sleepDurationProvider(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
                    static DecompressionMethods getDecompressionMethods() => DecompressionMethods.Deflate | DecompressionMethods.GZip;
                })
                .Build();

            return host.RunAsync();
        }
    }
}
