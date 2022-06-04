using MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

namespace MamisSolidarias.HttpClient.TEMPLATE.TEMPLATEClient;

public interface ITEMPLATEClient
{
    Task<(int, Response?)> GetTestAsync(Request requestParameters, CancellationToken token = default);
}