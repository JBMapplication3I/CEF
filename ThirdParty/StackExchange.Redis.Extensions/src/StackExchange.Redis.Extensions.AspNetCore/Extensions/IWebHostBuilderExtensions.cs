namespace Microsoft.AspNetCore.Hosting
{
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using StackExchange.Redis.Extensions.Core;
    using StackExchange.Redis.Extensions.Core.Configuration;

    /// <summary>A web host builder extensions.</summary>
    public static class IWebHostBuilderExtensions
    {
        /// <summary>An IWebHostBuilder extension method that configure stack exchange redis extensions.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="hostBuilder"> The hostBuilder to act on.</param>
        /// <param name="jsonBasePath">Full pathname of the JSON base file.</param>
        /// <returns>An IWebHostBuilder.</returns>
        public static IWebHostBuilder ConfigureStackExchangeRedisExtensions<T>(
                this IWebHostBuilder hostBuilder,
                string jsonBasePath = "Configurations")
            where T : ISerializer, new()
        {
            hostBuilder.ConfigureServices(services =>
            {
                var srvs = services.BuildServiceProvider();
                var environment = srvs.GetRequiredService<IHostingEnvironment>();
                var path = Path.Combine(environment.ContentRootPath, jsonBasePath, "redis.json");
                var exists = File.Exists(path);
                var builder = new ConfigurationBuilder();
                IConfigurationRoot cfg = builder
                    .SetBasePath(environment.ContentRootPath)
                    .AddJsonFile("Configurations/redis.json", false, true)
                    .AddJsonFile($"Configurations/redis.{environment.EnvironmentName}.json", true)
                    .AddEnvironmentVariables()
                    .Build();
                var redisConfiguration = cfg.GetSection("redisConfiguration").Get<RedisConfiguration>();
                services.AddSingleton(redisConfiguration);
                services.AddStackExchangeRedisExtensions<T>(redisConfiguration);
            });
            return hostBuilder;
        }
    }
}
