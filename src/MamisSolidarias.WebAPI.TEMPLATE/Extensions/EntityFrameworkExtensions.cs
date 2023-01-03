using MamisSolidarias.Infrastructure.TEMPLATE;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.TEMPLATE.Extensions;

internal static class EntityFrameworkExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(typeof(EntityFrameworkExtensions));
        
        var connectionString = configuration.GetConnectionString("TEMPLATEDb");

        if (connectionString is null)
        {
            logger.LogError("Connection string for TEMPLATEDb not found");
            throw new ArgumentException("Connection string not found");
        }
        
        services.AddDbContext<TEMPLATEDbContext>(
            t =>
                t.UseNpgsql(connectionString, r =>
                        r.MigrationsAssembly("MamisSolidarias.WebAPI.TEMPLATE"))
                    .EnableSensitiveDataLogging(!env.IsProduction())
        );
    }

    public static void RunMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TEMPLATEDbContext>();
        db.Database.Migrate();
    }
}