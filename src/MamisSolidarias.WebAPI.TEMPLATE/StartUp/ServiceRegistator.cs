using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MamisSolidarias.Infrastructure.TEMPLATE;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.TEMPLATE.StartUp;

internal static class ServiceRegistrator
{
    public static void Register(WebApplicationBuilder builder)
    {

        var connectionString = builder.Environment.EnvironmentName.ToLower() switch
        {
            "production" => builder.Configuration.GetConnectionString("Production"),
            _ => builder.Configuration.GetConnectionString("Development")
        };
        
        builder.Services.AddFastEndpoints();
        builder.Services.AddAuthenticationJWTBearer(builder.Configuration["JWT:Key"]);
        builder.Services.AddDbContext<TEMPLATEDbContext>(
            t => 
                t
                .UseNpgsql(connectionString)
                .EnableSensitiveDataLogging(!builder.Environment.IsProduction())
        );

        if (!builder.Environment.IsProduction())
            builder.Services.AddSwaggerDoc(tagIndex: 1, shortSchemaNames: true);
    }
}