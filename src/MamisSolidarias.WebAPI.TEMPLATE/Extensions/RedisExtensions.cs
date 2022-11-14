using StackExchange.Redis;

namespace MamisSolidarias.WebAPI.TEMPLATE.Extensions;

internal static class RedisExtensions
{
    private sealed record RedisOptions(string Host, int Port);
    
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(typeof(RedisExtensions));
        var redisOptions = configuration.GetSection("Redis").Get<RedisOptions>();

        if (redisOptions is null)
        {
            logger.LogError("Redis configuration not found.");
            throw new ArgumentNullException(nameof(redisOptions),"Redis configuration not found.");
        }
        
        var connectionString = $"{redisOptions.Host}:{redisOptions.Port}";
        services.AddSingleton(ConnectionMultiplexer.Connect(connectionString));
    }
}