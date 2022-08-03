using MamisSolidarias.HttpClient.TEMPLATE.Models;
using MamisSolidarias.HttpClient.TEMPLATE.Services;
using Microsoft.AspNetCore.Http;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public partial class TEMPLATEClient : ITEMPLATEClient
{
    private readonly HeaderService _headerService;
    private readonly IHttpClientFactory _httpClientFactory;
    
    public TEMPLATEClient(IHttpContextAccessor? contextAccessor,IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _headerService = new HeaderService(contextAccessor);
    }
    
    private ReadyRequest<TResponse> CreateRequest<TResponse>(HttpMethod httpMethod,params string[] urlParams)
    {
        var client = _httpClientFactory.CreateClient("TEMPLATE");
        var request = new HttpRequestMessage(httpMethod, string.Join('/', urlParams));
        
        var authHeader = _headerService.GetAuthorization();
        if (authHeader is not null)
            request.Headers.Add("Authorization",authHeader);
        
        return new ReadyRequest<TResponse>(client,request);
    }
}