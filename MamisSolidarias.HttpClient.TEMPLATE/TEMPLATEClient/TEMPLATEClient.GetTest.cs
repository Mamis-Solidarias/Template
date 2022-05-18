using Flurl.Http;
using MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public partial class TEMPLATEClient
{
    public Task<Response> GetTestAsync(string name, CancellationToken token = default)
    {
        return CreateRequest<Response>()
            .ExecuteAsync(
                (client, cancellationToken) => client
                    .Request("user", name)
                    .GetJsonAsync<Response>(cancellationToken),
                token
            );
    }
}