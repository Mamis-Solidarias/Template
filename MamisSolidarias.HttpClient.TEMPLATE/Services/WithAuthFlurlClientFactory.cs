using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace MamisSolidarias.HttpClient.TEMPLATE.Services;

/// <summary>
/// Flurl Client Factory that adds the current authorization header to the client
/// </summary>
internal class WithAuthFlurlClientFactory : PerBaseUrlFlurlClientFactory
{
    private readonly IHeaderService _headerService;

    public WithAuthFlurlClientFactory(IHeaderService headersService)
    {
        _headerService = headersService;
    }

    public IFlurlClient Get(Url url)
    {
        return base.Get(url).WithHeader("Authorization", _headerService.GetAuthorization());
    }
}