using FastEndpoints;

namespace MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

public class Endpoint : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("user/{Name}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Logger.LogInformation("Hello");
        if (req.Name != "lucas")
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(new Response
        {
            Email = "mymail@mail.com",
            Id = new Random().Next(10),
            Name = "Lucassss"
        }, cancellation: ct);
    }
}