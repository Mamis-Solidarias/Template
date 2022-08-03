using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace MamisSolidarias.HttpClient.TEMPLATE.Utils;

internal class ConfigurationFactory
{
    internal static IConfiguration GetTEMPLATEConfiguration(
        string baseUrl = "https://test.com", int retries = 3, int timeout = 500
    )
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"TEMPLATEHttpClient:BaseUrl", baseUrl},
            {"TEMPLATEHttpClient:Retries", retries.ToString()},
            {"TEMPLATEHttpClient:Timeout", timeout.ToString()}
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }
}