namespace StackExchange.Redis.Extensions.LegacyConfiguration.Configuration
{
    /// <summary>Handle Redis Configuration.</summary>
    public interface IRedisCachingConfiguration
    {
        /// <summary>The host of Redis Server.</summary>
        /// <value>The ip or name.</value>
        RedisHostCollection RedisHosts { get; }

        /// <summary>The strategy to use when executing server wide commands.</summary>
        /// <value>The server enumeration strategy.</value>
        ServerEnumerationStrategyConfiguration ServerEnumerationStrategy { get; }

        /// <summary>Specify if the connection can use Admin commands like flush database.</summary>
        /// <value><c>true</c> if can use admin commands; otherwise, <c>false</c>.</value>
        bool AllowAdmin { get; }

        /// <summary>Specify if the connection is a secure connection or not.</summary>
        /// <value><c>true</c> if is secure; otherwise, <c>false</c>.</value>
        bool Ssl { get; }

        /// <summary>The connection timeout.</summary>
        /// <value>The connect timeout.</value>
        int ConnectTimeout { get; }

        /// <summary>If true, Connect will not create a connection while no servers are available.</summary>
        /// <value>True if abort on connect fail, false if not.</value>
        bool AbortOnConnectFail { get; }

        /// <summary>Database Id.</summary>
        /// <value>The database id, the default value is 0.</value>
        int Database { get; }

        /// <summary>The password or access key.</summary>
        /// <value>The password.</value>
        string Password { get; }

        /// <summary>The key separation prefix used for all cache entries.</summary>
        /// <value>The key prefix.</value>
        string KeyPrefix { get; }
    }
}