using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace ASignSdk;

public static class ASignServiceExtension
{
    /// <summary>
    /// 添加爱签SDK
    /// </summary>
    /// <param name="serviceBuilder"></param>
    /// <param name="configuration"></param>
    /// <param name="configName"></param>
    /// <returns></returns>
    public static IServiceCollection AddASignSdk(this IServiceCollection serviceBuilder,
        IConfiguration configuration, string configName)
    {
        serviceBuilder.Configure<ASignOptions>(configuration.GetSection(configName));
        serviceBuilder.TryAddSingleton<IASignSdk, ASignSdk>();
        serviceBuilder.AddHttpClient(nameof(ASignSdk), (services, httpClient) =>
        {
            var options = services.GetService<IOptions<ASignOptions>>();
            httpClient.Timeout = TimeSpan.FromSeconds(options.Value.Timeout);
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

        }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.All
        });
        return serviceBuilder;
    }
}