using MamisSolidarias.HttpClient.TEMPLATE.Models;
using MamisSolidarias.HttpClient.TEMPLATE.Services;
using MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace MamisSolidarias.HttpClient.TEMPLATE;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// It registers the TEMPLATEHttpClient using dependency injection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddTEMPLATEHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new TEMPLATEConfiguration();
        configuration.GetSection("TEMPLATEHttpClient").Bind(config);
        ArgumentNullException.ThrowIfNull(config.BaseUrl);
        ArgumentNullException.ThrowIfNull(config.Timeout);
        ArgumentNullException.ThrowIfNull(config.Retries);

        services.AddHttpContextAccessor();
        services.AddSingleton<ITEMPLATEClient, TEMPLATEClient.TEMPLATEClient>();
        services.AddHttpClient("TEMPLATE", (services,client) =>
        {
            client.BaseAddress = new Uri(config.BaseUrl);
            client.Timeout = TimeSpan.FromMilliseconds(config.Timeout);
            
            var contextAccessor = services.GetService<IHttpContextAccessor>();
            if (contextAccessor is not null)
            {
                var authHeader = new HeaderService(contextAccessor).GetAuthorization();
                if (authHeader is not null)
                    client.DefaultRequestHeaders.Add("Authorization", authHeader);
            }
            
        })
            .AddTransientHttpErrorPolicy(t =>
            t.WaitAndRetryAsync(config.Retries,
                retryAttempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, retryAttempt)))
        );
    }
}