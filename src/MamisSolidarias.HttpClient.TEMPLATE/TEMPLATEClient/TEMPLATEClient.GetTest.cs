using MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public partial class TEMPLATEClient
{
    public Task<Response?> GetTestAsync(Request requestParameters, CancellationToken token = default)
    {
        return CreateRequest(HttpMethod.Get, "user", requestParameters.Name)
            .ExecuteAsync<Response>(token);

    }
}