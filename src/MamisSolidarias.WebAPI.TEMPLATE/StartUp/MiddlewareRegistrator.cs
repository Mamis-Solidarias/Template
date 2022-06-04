using FastEndpoints;
using FastEndpoints.Swagger;

namespace MamisSolidarias.WebAPI.TEMPLATE.StartUp;

internal static class MiddlewareRegistrator
{
    public static void Register(WebApplication app)
    {
        app.UseDefaultExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseFastEndpoints();

        if (!app.Environment.IsProduction())
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(t => t.ConfigureDefaults());
        }
    }
}