using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

namespace MamisSolidarias.WebAPI.TEMPLATE.StartUp;

internal static class ServiceRegistrator
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddFastEndpoints();
        builder.Services.AddAuthenticationJWTBearer(builder.Configuration["JWT:Key"]);

        if (!builder.Environment.IsProduction())
            builder.Services.AddSwaggerDoc(tagIndex: 1, shortSchemaNames: true);
    }
}