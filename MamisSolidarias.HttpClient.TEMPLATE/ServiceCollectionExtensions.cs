using MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;
using Microsoft.Extensions.DependencyInjection;

namespace MamisSolidarias.HttpClient.TEMPLATE;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// It registers the TEMPLATEHttpClient using dependency injection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddTEMPLATEHttpClient(this IServiceCollection services)
    {
        services.AddSingleton<ITEMPLATEClient, TEMPLATEClient.TEMPLATEClient>();
        return services;
    }
}