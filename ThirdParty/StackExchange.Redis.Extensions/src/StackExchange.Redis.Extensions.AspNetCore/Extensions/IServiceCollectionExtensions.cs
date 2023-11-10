namespace Microsoft.Extensions.DependencyInjection
{
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core;
    using StackExchange.Redis.Extensions.Core.Configuration;

    /// <summary>A service collection extensions.</summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>An IServiceCollection extension method that adds a stack exchange redis extensions to
        /// 'connectionMultiplexer'.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="services">             The services to act on.</param>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddStackExchangeRedisExtensions<T>(
                this IServiceCollection services,
                IConnectionMultiplexer connectionMultiplexer)
            where T : ISerializer, new()
        {
            services.AddSingleton<ICacheClient>(new StackExchangeRedisCacheClient(connectionMultiplexer, new T()));
            return services;
        }

        /// <summary>An IServiceCollection extension method that adds a stack exchange redis extensions to
        /// 'connectionString'.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="services">        The services to act on.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddStackExchangeRedisExtensions<T>(
                this IServiceCollection services,
                string connectionString)
            where T : ISerializer, new()
        {
            services.AddSingleton<ICacheClient>(new StackExchangeRedisCacheClient(new T(), connectionString));
            return services;
        }

        /// <summary>An IServiceCollection extension method that adds a stack exchange redis extensions to
        /// 'redisConfiguration'.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="services">          The services to act on.</param>
        /// <param name="redisConfiguration">The redis configuration.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddStackExchangeRedisExtensions<T>(
                this IServiceCollection services,
                RedisConfiguration redisConfiguration)
            where T : ISerializer, new()
        {
            services.AddSingleton<ICacheClient>(new StackExchangeRedisCacheClient(new T(), redisConfiguration));
            return services;
        }
    }
}
