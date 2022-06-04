using Flurl.Http;
using MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public partial class TEMPLATEClient
{
    public Task<(int, Response?)> GetTestAsync(Request requestParameters, CancellationToken token = default)
    {
        return CreateRequest<Response>("user", requestParameters.Name)
            .ExecuteAsync(
                (request, cancellationToken) => request.GetJsonAsync<Response>(cancellationToken),
                token
            );
    }
}