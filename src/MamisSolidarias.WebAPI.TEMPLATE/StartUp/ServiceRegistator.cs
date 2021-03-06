using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MamisSolidarias.Infrastructure.TEMPLATE;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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
        
        builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder =>
        {
            tracerProviderBuilder
                .AddConsoleExporter()
                // .AddOtlpExporter(opt =>
                // {
                //     opt.Endpoint = new Uri("https://otlp.nr-data.net");
                //     opt.Headers["api-key"] = "";
                //     opt.Protocol = OtlpExportProtocol.HttpProtobuf;
                // })
                .AddSource(builder.Configuration["Service:Name"])
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: builder.Configuration["Service:Name"], serviceVersion: builder.Configuration["Service:Version"]))
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation();
        });        
        builder.Services.AddFastEndpoints();
        builder.Services.AddAuthenticationJWTBearer(builder.Configuration["JWT:Key"]);
        builder.Services.AddDbContext<TEMPLATEDbContext>(
            t => 
                t.UseNpgsql(connectionString, r=> r.MigrationsAssembly("MamisSolidarias.WebAPI.TEMPLATE"))
                    .EnableSensitiveDataLogging(!builder.Environment.IsProduction())
        );

        if (!builder.Environment.IsProduction())
            builder.Services.AddSwaggerDoc(tagIndex: 1, shortSchemaNames: true);
    }
}