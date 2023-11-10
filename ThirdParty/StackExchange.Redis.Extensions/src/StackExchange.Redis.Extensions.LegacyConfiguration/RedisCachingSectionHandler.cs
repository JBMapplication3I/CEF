namespace StackExchange.Redis.Extensions.LegacyConfiguration
{
    using System.Collections.Generic;
    using System.Configuration;
    using StackExchange.Redis.Extensions.Core.Configuration;
    using StackExchange.Redis.Extensions.LegacyConfiguration.Configuration;
    using RedisHost = Configuration.RedisHost;

    /// <summary>The implementation of <see cref="IRedisCachingConfiguration"/></summary>
    /// <seealso cref="System.Configuration.ConfigurationSection"/>
    /// <seealso cref="StackExchange.Redis.Extensions.LegacyConfiguration.Configuration.IRedisCachingConfiguration"/>
    public class RedisCachingSectionHandler : ConfigurationSection, IRedisCachingConfiguration
    {
        /// <summary>The host of Redis Server.</summary>
        /// <value>The ip or name.</value>
        [ConfigurationProperty("hosts")]
        public RedisHostCollection RedisHosts
            => this["hosts"] as RedisHostCollection;

        /// <summary>The strategy to use when executing server wide commands.</summary>
        /// <value>The server enumeration strategy.</value>
        [ConfigurationProperty("serverEnumerationStrategy")]
        public ServerEnumerationStrategyConfiguration ServerEnumerationStrategy
            => this["serverEnumerationStrategy"] as ServerEnumerationStrategyConfiguration;

        /// <summary>Specify if the connection can use Admin commands like flush database.</summary>
        /// <value><c>true</c> if can use admin commands; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("allowAdmin")]
        public bool AllowAdmin
        {
            get
            {
                var value = this["allowAdmin"]?.ToString();

                return !string.IsNullOrEmpty(value) && bool.TryParse(value, out var result) && result;
            }
        }

        /// <summary>Specify if the connection is a secure connection or not.</summary>
        /// <value><c>true</c> if is secure; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("ssl")]
        public bool Ssl
        {
            get
            {
                var value = this["ssl"]?.ToString();

                return !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out var result) && result;
            }
        }

        /// <summary>The connection timeout.</summary>
        /// <value>The connect timeout.</value>
        [ConfigurationProperty("connectTimeout")]
        public int ConnectTimeout
        {
            get
            {
                var value = this["connectTimeout"]?.ToString();

                return !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out var result) ? result : 5000;
            }
        }

        /// <summary>If true, Connect will not create a connection while no servers are available.</summary>
        /// <value>True if abort on connect fail, false if not.</value>
        [ConfigurationProperty("abortOnConnectFail")]
        public bool AbortOnConnectFail
        {
            get
            {
                var value = this["abortOnConnectFail"]?.ToString();

                return !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out var result) && result;
            }
        }

        /// <summary>Database Id.</summary>
        /// <value>The database id, the default value is 0.</value>
        [ConfigurationProperty("database")]
        public int Database
        {
            get
            {
                var value = this["database"]?.ToString();

                return !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out var result) ? result : 0;
            }
        }

        /// <summary>The password or access key.</summary>
        /// <value>The password.</value>
        [ConfigurationProperty("password", IsRequired = false)]
        public string Password => this["password"] as string;

        /// <summary>The key separation prefix used for all cache entries.</summary>
        /// <value>The key prefix.</value>
        [ConfigurationProperty("keyprefix", IsRequired = false)]
        public string KeyPrefix => this["keyprefix"] as string;

        /// <summary>Gets the configuration.</summary>
        /// <returns>The configuration.</returns>
        public static RedisConfiguration GetConfig()
        {
            if (ConfigurationManager.GetSection("redisCacheClient") is not RedisCachingSectionHandler cfg)
            {
                throw new ConfigurationErrorsException("Unable to locate <redisCacheClient> section into your configuration file. Take a look https://github.com/imperugo/StackExchange.Redis.Extensions");
            }
            RedisConfiguration result = new RedisConfiguration();
            result.AbortOnConnectFail = cfg.AbortOnConnectFail;
            result.AllowAdmin = cfg.AllowAdmin;
            result.ConnectTimeout = cfg.ConnectTimeout;
            result.Database = cfg.Database;
            result.KeyPrefix = cfg.KeyPrefix;
            result.Password = cfg.Password;
            result.Ssl = cfg.Ssl;
            List<Core.Configuration.RedisHost> hosts = new List<Core.Configuration.RedisHost>();
            foreach (RedisHost host in cfg.RedisHosts)
            {
                hosts.Add(new Core.Configuration.RedisHost()
                {
                    Host = host.Host,
                    Port = host.CachePort
                });
            }
            result.Hosts = hosts.ToArray();
            result.ServerEnumerationStrategy = new ServerEnumerationStrategy()
            {
                UnreachableServerAction = cfg.ServerEnumerationStrategy.UnreachableServerAction,
                TargetRole = cfg.ServerEnumerationStrategy.TargetRole,
                Mode = cfg.ServerEnumerationStrategy.Mode
            };
            return result;
        }
    }
}
