using Flurl.Http.Configuration;
using MamisSolidarias.HttpClient.TEMPLATE.Models;
using MamisSolidarias.HttpClient.TEMPLATE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public partial class TEMPLATEClient : ITEMPLATEClient
{
    private readonly TEMPLATEConfiguration _configuration;
    private readonly IFlurlClientFactory _flurlClientFactory;

    public TEMPLATEClient(IHttpContextAccessor ctxa, IConfiguration configuration)
    {
        _configuration = new TEMPLATEConfiguration();
        configuration.GetSection("TEMPLATEHttpClient").Bind(_configuration);

        IHeaderService headerService = new HeaderService(ctxa);
        _flurlClientFactory = new WithAuthFlurlClientFactory(headerService);
    }

    private ReadyRequest<T> CreateRequest<T>()
    {
        return new ReadyRequest<T>(_flurlClientFactory.Get(_configuration.BaseUrl), _configuration);
    }
}