namespace StackExchange.Redis.Extensions.LegacyConfiguration.Configuration
{
    using System;
    using System.Configuration;

    /// <summary>Configuration element for Redis Host.</summary>
    /// <seealso cref="System.Configuration.ConfigurationElement"/>
    public class RedisHost : ConfigurationElement
    {
        /// <summary>Gets the Redis host.</summary>
        /// <value>The host.</value>
        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get
            {
                return this["host"] as string;
            }
        }

        /// <summary>Gets the port.</summary>
        /// <value>The port.</value>
        [ConfigurationProperty("cachePort", IsRequired = true)]
        public int CachePort
        {
            get
            {
                var config = this["cachePort"];
                if (config != null)
                {
                    var value = config.ToString();
                    if (!string.IsNullOrEmpty(value) && int.TryParse(value, out var result))
                    {
                        return result;
                    }
                }
                throw new Exception("Redis Cache port must be number.");
            }
        }
    }
}
