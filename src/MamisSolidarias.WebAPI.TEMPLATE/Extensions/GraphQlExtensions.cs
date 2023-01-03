using HotChocolate.Diagnostics;
using MamisSolidarias.Infrastructure.TEMPLATE;
using StackExchange.Redis;

namespace MamisSolidarias.WebAPI.TEMPLATE.Extensions;

internal static class GraphQlExtensions
{
    private sealed record GraphQlOptions(string GlobalSchemaName);

    public static void AddGraphQl(this IServiceCollection services, IConfiguration configuration,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger("GraphQL");

        var builder = services.AddGraphQLServer()
            .AddQueryType(t => t.Name("Query"))
            .AddInstrumentation(t =>
            {
                t.Scopes = ActivityScopes.All;
                t.IncludeDocument = true;
                t.RequestDetails = RequestDetails.All;
                t.IncludeDataLoaderKeys = true;
            })
            .AddAuthorization()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .RegisterDbContext<TEMPLATEDbContext>()
            .InitializeOnStartup();
        
        var options = configuration.GetSection("GraphQl").Get<GraphQlOptions>();

        if (options is null)
        {
            logger.LogWarning("GraphQl gateway options not found");
            return;
        }

        builder.PublishSchemaDefinition(t =>
        {
            // TODO: Set to new Service.TEMPLATE
            t.SetName("gql");
            t.PublishToRedis(options.GlobalSchemaName,
                sp => sp.GetRequiredService<ConnectionMultiplexer>()
            );
        });
    }
}