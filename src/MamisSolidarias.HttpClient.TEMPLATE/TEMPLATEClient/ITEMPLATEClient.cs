using MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public interface ITEMPLATEClient
{
    Task<Response?> GetTestAsync(Request requestParameters, CancellationToken token = default);
}