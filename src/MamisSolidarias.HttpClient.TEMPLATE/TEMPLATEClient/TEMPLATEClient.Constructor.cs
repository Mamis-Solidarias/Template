using Flurl;
using Flurl.Http;
using MamisSolidarias.HttpClient.TEMPLATE.Models;
using MamisSolidarias.HttpClient.TEMPLATE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public partial class TEMPLATEClient : ITEMPLATEClient
{
    private readonly TEMPLATEConfiguration _configuration;
    private readonly HeaderService _headerService;

    public TEMPLATEClient(IHttpContextAccessor ctxa, IConfiguration configuration)
    {
        _configuration = new TEMPLATEConfiguration();
        configuration.GetSection("TEMPLATEHttpClient").Bind(_configuration);
        _headerService = new HeaderService(ctxa);
    }

    private ReadyRequest<T> CreateRequest<T>(params string[] urlParams)
    {
        var url = new Url(_configuration.BaseUrl).AppendPathSegments(urlParams as object[]);
        var authHeader = _headerService.GetAuthorization();
        var request = authHeader switch
        {
            null => new FlurlRequest(url),
            _ => new FlurlRequest(url).WithHeader("Authorization", authHeader)
        };

        return new ReadyRequest<T>(request, _configuration);
    }
}