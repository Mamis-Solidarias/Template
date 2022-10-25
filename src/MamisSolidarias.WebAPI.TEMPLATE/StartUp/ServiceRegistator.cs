using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MamisSolidarias.Infrastructure.TEMPLATE;
using MamisSolidarias.WebAPI.TEMPLATE.Extensions;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MamisSolidarias.WebAPI.TEMPLATE.StartUp;

internal static class ServiceRegistrator
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddDataProtection(builder.Configuration);
        builder.Services.AddOpenTelemetry(builder.Configuration,builder.Logging);
        var connectionString = builder.Environment.EnvironmentName.ToLower() switch
        {
            "production" => builder.Configuration.GetConnectionString("Production"),
            _ => builder.Configuration.GetConnectionString("Development")
        };
        
        builder.Services.AddFastEndpoints();
        builder.Services.AddAuthenticationJWTBearer(builder.Configuration["JWT:Key"]);
        builder.Services.AddDbContext<TEMPLATEDbContext>(
            t =>
                t.UseNpgsql(connectionString, r => r.MigrationsAssembly("MamisSolidarias.WebAPI.TEMPLATE"))
                    .EnableSensitiveDataLogging(!builder.Environment.IsProduction())
        );

        if (!builder.Environment.IsProduction())
            builder.Services.AddSwaggerDoc();
    }
}