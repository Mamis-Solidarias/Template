using Flurl.Http;
using MamisSolidarias.HttpClient.TEMPLATE.Services;
using Polly;

namespace MamisSolidarias.HttpClient.TEMPLATE.Models;

/// <summary>
/// Wrapper object to apply policies to requests
/// </summary>
/// <typeparam name="TResponse">Type of the response object</typeparam>
internal class ReadyRequest<TResponse>
{
    private readonly IFlurlClient _client;
    private readonly AsyncPolicy _policy;

    internal ReadyRequest(IFlurlClient client, TEMPLATEConfiguration config, AsyncPolicy? policy = null)
    {
        _client = client;
        _policy = policy ?? PolicyManager.BuildRetryPolicy(config.Retries, config.Timeout);
    }

    /// <summary>
    /// It executes a request with the selected policy.
    /// </summary>
    /// <param name="action">Http Request to the URL</param>
    /// <param name="token">Cancellation token</param>
    /// <returns></returns>
    public Task<TResponse> ExecuteAsync(Func<IFlurlClient, CancellationToken, Task<TResponse>> action,
        CancellationToken token = default)
    {
        return _policy.ExecuteAsync(
            cancellationToken => action.Invoke(_client, cancellationToken), token);
    }
}