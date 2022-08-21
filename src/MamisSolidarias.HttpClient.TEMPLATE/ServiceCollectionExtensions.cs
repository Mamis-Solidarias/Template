using MamisSolidarias.HttpClient.TEMPLATE.Models;
using MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace MamisSolidarias.HttpClient.TEMPLATE;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// It registers the TEMPLATEHttpClient using dependency injection
    /// </summary>
    /// <param name="builder"></param>
    public static void AddTEMPLATEHttpClient(this WebApplicationBuilder builder)
    {
        var configuration = new TEMPLATEConfiguration();
        builder.Configuration.GetSection("TEMPLATEHttpClient").Bind(configuration);
        ArgumentNullException.ThrowIfNull(configuration.BaseUrl);
        ArgumentNullException.ThrowIfNull(configuration.Timeout);
        ArgumentNullException.ThrowIfNull(configuration.Retries);

        builder.Services.AddSingleton<ITEMPLATEClient, TEMPLATEClient.TEMPLATEClient>();
        builder.Services.AddHttpClient("TEMPLATE", client =>
        {
            client.BaseAddress = new Uri(configuration.BaseUrl);
            client.Timeout = TimeSpan.FromMilliseconds(configuration.Timeout);
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        })
            .AddTransientHttpErrorPolicy(t =>
            t.WaitAndRetryAsync(configuration.Retries,
                retryAttempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, retryAttempt)))
        );
    }
}