using MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public partial class TEMPLATEClient
{
    public async Task<Response?> GetTestAsync(Request requestParameters, CancellationToken token = default)
    {
        return await CreateRequest<Response>(HttpMethod.Get, "user", requestParameters.Name)
            .ExecuteAsync(token);

    }
}