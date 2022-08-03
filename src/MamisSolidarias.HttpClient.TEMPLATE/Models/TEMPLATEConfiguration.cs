namespace MamisSolidarias.HttpClient.TEMPLATE.Models;

/// <summary>
/// Basic configuration to use this HttpClient. It must be stored in the app settings under TEMPLATEHttpClient
/// </summary>
internal class TEMPLATEConfiguration
{
    /// <summary>
    /// Base Url of the Users' service
    /// </summary>
    public string? BaseUrl { get; set; } = null;
    
    /// <summary>
    /// Number of time each call should be retried
    /// </summary>
    public int Retries { get; set; } = 3;
    
    /// <summary>
    /// Timeout in milliseconds for each http call
    /// </summary>
    public int Timeout { get; set; } = 500;
}