namespace MamisSolidarias.HttpClient.TEMPLATE.Models;

/// <summary>
/// Basic configuration to use this HttpClient. It must be stored in the app settings under TEMPLATEHttpClient
/// </summary>
internal class TEMPLATEConfiguration
{
    public string? BaseUrl { get; set; }
    public int Retries { get; set; } = 3;
    public int Timeout { get; set; } = 5;
}