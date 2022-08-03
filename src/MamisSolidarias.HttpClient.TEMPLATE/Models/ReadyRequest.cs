using System.Net.Http.Json;
using System.Text.Json;

namespace MamisSolidarias.HttpClient.TEMPLATE.Models;

/// <summary>
/// Wrapper object to apply policies to requests
/// </summary>
/// <typeparam name="TResponse">Type of the response object</typeparam>
internal class ReadyRequest<TResponse>
{
    private readonly System.Net.Http.HttpClient _client;
    private readonly HttpRequestMessage _requestMessage;

    public ReadyRequest(System.Net.Http.HttpClient client, HttpRequestMessage request)
    {
        _client = client;
        _requestMessage = request;
    }
    
    /// <summary>
    /// It loads the request body into the request
    /// </summary>
    /// <param name="body">Content to load into the request</param>
    /// <typeparam name="TRequest">Type of the expected response</typeparam>
    /// <returns>An http request</returns>
    public ReadyRequest<TResponse> WithContent<TRequest>(TRequest body)
    {
        var data = JsonSerializer.SerializeToUtf8Bytes(body);
        _requestMessage.Content = new ByteArrayContent(data);
        return this;
    }


    /// <summary>
    /// It executes the http request
    /// </summary>
    /// <param name="token"></param>
    /// <typeparam name="TResponse">Type of the expected response</typeparam>
    /// <returns>The expected response body</returns>
    /// <exception cref="HttpRequestException">The request has not been successful</exception>
    public async Task<TResponse?> ExecuteAsync(CancellationToken token)
    {
        var response = await _client.SendAsync(_requestMessage, token);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: token);
    }


}