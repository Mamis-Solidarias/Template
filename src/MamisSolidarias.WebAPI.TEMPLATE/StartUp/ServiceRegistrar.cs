using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MamisSolidarias.WebAPI.TEMPLATE.Extensions;

namespace MamisSolidarias.WebAPI.TEMPLATE.StartUp;

internal static class ServiceRegistrar
{
    private static ILoggerFactory CreateLoggerFactory(IConfiguration configuration)
    {
        return LoggerFactory.Create(loggingBuilder => loggingBuilder
            .AddConfiguration(configuration)
            .AddConsole()
        );
    }

    public static void Register(WebApplicationBuilder builder)
    {
        using var loggerFactory = CreateLoggerFactory(builder.Configuration);

        builder.Services.AddDataProtection(builder.Configuration);
        builder.Services.AddOpenTelemetry(builder.Configuration, builder.Logging);
        builder.Services.AddRedis(builder.Configuration, loggerFactory);

        builder.Services.AddDbContext(builder.Configuration, builder.Environment, loggerFactory);

        builder.Services.AddGraphQl(builder.Configuration,loggerFactory);
        builder.Services.AddFastEndpoints();
        builder.Services.AddAuthenticationJWTBearer(builder.Configuration["JWT:Key"]);

        if (!builder.Environment.IsProduction())
            builder.Services.AddSwaggerDoc();
    }
}