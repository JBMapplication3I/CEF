namespace StackExchange.Redis.Extensions.LegacyConfiguration.Configuration
{
    using System.Configuration;

    /// <summary>Configuration Element Collection for <see cref="RedisHost"/></summary>
    /// <seealso cref="System.Configuration.ConfigurationElementCollection"/>
    public class RedisHostCollection : ConfigurationElementCollection
    {
        /// <summary>Gets or sets the <see cref="RedisHost"/> at the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="RedisHost"/>.</returns>
        /// <returns>.</returns>
        public RedisHost this[int index]
        {
            get
            {
                return BaseGet(index) as RedisHost;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        /// <summary>Creates the new element.</summary>
        /// <returns>The new new element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new RedisHost();
        }

        /// <summary>Gets the element key.</summary>
        /// <param name="element">The element.</param>
        /// <returns>The element key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
            => $"{((RedisHost)element).Host}:{((RedisHost)element).CachePort}";
    }
}
