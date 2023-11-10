// <copyright file="RedisCacheProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Redis cache provider class</summary>
#if SERedis550
namespace Clarity.Ecommerce.Providers.Caching.Redis
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Threading.Tasks;
    using JSConfigs;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core.Abstractions;
    using StackExchange.Redis.Extensions.Core.Configuration;
    using StackExchange.Redis.Extensions.Core.Implementations;
    using StackExchange.Redis.Extensions.Newtonsoft;
    using Utilities;

    /// <summary>A Redis-based CEF cache.</summary>
    /// <seealso cref="CacheProviderBase"/>
    public class RedisCacheProvider : CacheProviderBase
    {
        /// <summary>The cache client.</summary>
        private RedisCacheClient cacheClient;

        /// <summary>Manager for connection pool.</summary>
        private RedisCacheConnectionPoolManager connectionPoolManager;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => RedisCacheProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        private static string ParsedPrefix { get; set; }

        /// <inheritdoc/>
        public override Task InitAsync(string? contextProfileName)
        {
            var redisConfiguration = RedisConnectionOptionsFromAppSettings();
            connectionPoolManager = new RedisCacheConnectionPoolManager(redisConfiguration);
            cacheClient = new RedisCacheClient(
                connectionPoolManager,
                new NewtonsoftSerializer(new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore, // Get rid of NULLs
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, // Get rid of default values
                    Formatting = Formatting.None, // no whitespace, keeps the size down
                    DateFormatHandling = DateFormatHandling.IsoDateFormat, // Use a legible format
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new SkipEmptyContractResolver(),
                }),
                redisConfiguration);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task<bool> ExistsAsync(string key, bool usePrefix = true)
        {
            return RetryHelper.RetryOnExceptionAsync<RedisTimeoutException, bool>(
                () => cacheClient.Db0.ExistsAsync(usePrefix ? PrefixKey(key) : key));
        }

        /// <inheritdoc/>
        public override async Task<T> GetAsync<T>(string key, bool usePrefix = true)
        {
            var tryCount = 0;
            while (tryCount < 3)
            {
                try
                {
                    if (!await ExistsAsync(key, usePrefix).ConfigureAwait(false))
                    {
                        return default;
                    }
                    return await cacheClient.Db0.GetAsync<T>(usePrefix ? PrefixKey(key) : key).ConfigureAwait(false);
                }
                catch (RedisTimeoutException)
                {
                    tryCount++;
                }
            }
            throw new TimeoutException(
                $"Unable to load key from Redis due to timeouts after multiple attempts [{key}]");
        }

        /// <inheritdoc/>
        public override Task AddAsync<T>(string key, T obj, bool usePrefix = true, TimeSpan? timeToLive = null)
        {
            return cacheClient.Db0.AddAsync(usePrefix ? PrefixKey(key) : key, obj, TimespanToDateTimeOffset(timeToLive));
        }

        /// <inheritdoc/>
        public override Task RemoveAsync(string key, bool usePrefix = true)
        {
            return cacheClient.Db0.RemoveAsync(usePrefix ? PrefixKey(key) : key);
        }

        /// <inheritdoc/>
        public override async Task RemoveByPatternAsync(string pattern, bool usePrefix = true)
        {
            await cacheClient.Db0.RemoveAllAsync(
                    await cacheClient.Db0.SearchKeysAsync(
                            usePrefix ? PrefixKey(pattern) : pattern)
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <summary>Prefix key.</summary>
        /// <param name="key">The key.</param>
        /// <returns>A string.</returns>
        protected string PrefixKey(string key)
        {
            if (ParsedPrefix == null)
            {
                ParsedPrefix = $"CEFCache:{CEFConfigDictionary.SiteRouteHostUrl?.Replace("http://", string.Empty).Replace("https://", string.Empty)}";
            }
            return $"{ParsedPrefix}:{key}";
        }

        private static RedisConfiguration RedisConnectionOptionsFromAppSettings()
        {
            var retVal = new RedisConfiguration();
            var host = new RedisHost
            {
                Host = CEFConfigDictionary.CachingRedisHostUri,
            };
            if (Contract.CheckValidID(CEFConfigDictionary.CachingRedisHostPort))
            {
                host.Port = CEFConfigDictionary.CachingRedisHostPort.Value;
            }
            retVal.Hosts = new[] { host };
            // if (Contract.CheckValidKey(CEFConfigDictionary.CachingRedisUsername))
            // {
            //     connectionString.Append(",username=").Append(CEFConfigDictionary.CachingRedisUsername);
            // }
            if (Contract.CheckValidKey(CEFConfigDictionary.CachingRedisPassword))
            {
                retVal.Password = CEFConfigDictionary.CachingRedisPassword;
            }
            if (CEFConfigDictionary.CachingRedisRequiredSSL)
            {
                retVal.Ssl = true;
            }
            if (CEFConfigDictionary.CachingRedisAbortConnect)
            {
                retVal.AbortOnConnectFail = true;
            }
            retVal.KeyPrefix = $"CEFCache:{CEFConfigDictionary.SiteRouteHostUrl?.Replace("http://", string.Empty).Replace("https://", string.Empty)}";
            retVal.ConnectTimeout = 50_000; // Default is 5_000
            retVal.SyncTimeout = 10_000; // Default is 1_000
            retVal.AllowAdmin = true;
            return retVal;
        }

        /// <summary>Timespan to date time offset.</summary>
        /// <remarks>time to live value:<br/>
        /// null or 0 = No Expiration<br/>
        /// negative = Expires Immediately<br/>
        /// positive = Expires based on TimeSpan.</remarks>
        /// <param name="timeToLive">The time to live.</param>
        /// <returns>A DateTimeOffset.</returns>
        private DateTimeOffset TimespanToDateTimeOffset(TimeSpan? timeToLive)
        {
            if (timeToLive == null || timeToLive == TimeSpan.Zero)
            {
                return DateTimeOffset.MaxValue;
            }
            if (timeToLive < TimeSpan.Zero)
            {
                return new DateTimeOffset(DateTime.UtcNow);
            }
            return new DateTimeOffset(DateTime.UtcNow.Add(timeToLive.Value));
        }

        /// <summary>A skip empty contract resolver.</summary>
        /// <seealso cref="DefaultContractResolver"/>
        public class SkipEmptyContractResolver : DefaultContractResolver
        {
            /// <inheritdoc/>
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);
                var isDefaultValueIgnored = ((property.DefaultValueHandling ?? DefaultValueHandling.Ignore)
                                             & DefaultValueHandling.Ignore) != 0;
                if (!isDefaultValueIgnored
                    || typeof(string).GetTypeInfo().IsAssignableFrom(property.PropertyType.GetTypeInfo())
                    || !typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(property.PropertyType.GetTypeInfo()))
                {
                    return property;
                }
                bool NewShouldSerialize(object obj)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    return !(property.ValueProvider.GetValue(obj) is ICollection collection)
                        || collection.Count != 0;
                }
                var oldShouldSerialize = property.ShouldSerialize;
                property.ShouldSerialize = oldShouldSerialize != null
                    ? o => oldShouldSerialize(o) && NewShouldSerialize(o)
                    : (Predicate<object>)NewShouldSerialize;
                return property;
            }
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Caching.Redis
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using JSConfigs;
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core;
    using StackExchange.Redis.Extensions.Newtonsoft;
    using Utilities;

    /// <summary>The redis cache provider.</summary>
    /// <seealso cref="CacheProviderBase"/>
    public class RedisCacheProvider : CacheProviderBase
    {
        /// <summary>The prefix.</summary>
        private string prefix = null!;

        /// <summary>The connection.</summary>
        private IConnectionMultiplexer? connection;

        /// <summary>The cache client.</summary>
        private StackExchangeRedisCacheClient cacheClient = null!;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => RedisCacheProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task InitAsync(string? contextProfileName)
        {
            Init(contextProfileName);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override void Init(string? contextProfileName)
        {
            CEFConfigDictionary.Load(typeof(CEFConfigDictionary));
            _ = Contract.RequiresValidKey<ConfigurationErrorsException>(
                CEFConfigDictionary.CachingRedisHostUri,
                "Redis is enabled but does not have the Host setting set up.");
#if SERedis550
            Instance = new CEFCacheRedis();
#else
            prefix = $"CEFCache:{CEFConfigDictionary.SiteRouteHostUrl.Replace("http://", string.Empty).Replace("https://", string.Empty)}";
            var tryCount = 0;
            var failures = new List<Exception>();
            while (tryCount < 3 && connection == null)
            {
                try
                {
                    connection = ConnectionMultiplexer.Connect(
                        ConfigurationOptions.Parse(
                            ConnectionStringFromAppSettings()));
                }
                catch (RedisConnectionException ex)
                {
                    failures.Add(ex);
                    connection = null;
                    Thread.Sleep(1_000);
                    tryCount++;
                }
            }
            if (connection == null)
            {
                throw failures[0];
            }
            cacheClient = new(
                connection,
                new NewtonsoftSerializer(/*SerializableAttributesDictionaryExtensions.JsonSettings*/));
            IsInitialized = true;
#endif
        }

        /// <inheritdoc/>
        public override Task<bool> ExistsAsync(string key, bool usePrefix = true)
        {
            return RetryHelper.RetryOnExceptionAsync<RedisConnectionException, bool>(
                () => cacheClient.ExistsAsync(usePrefix ? PrefixKey(key) : key));
        }

        /// <inheritdoc/>
        public override bool Exists(string key, bool usePrefix = true)
        {
            return cacheClient.Exists(usePrefix ? PrefixKey(key) : key);
        }

        /// <inheritdoc/>
        public override async Task<T> GetAsync<T>(string key, bool usePrefix = true)
        {
            var tryCount = 0;
            while (tryCount < 3)
            {
                try
                {
                    if (!await ExistsAsync(key, usePrefix).ConfigureAwait(false))
                    {
                        return default!;
                    }
                    return await cacheClient.GetAsync<T>(usePrefix ? PrefixKey(key) : key).ConfigureAwait(false);
                }
                catch (RedisConnectionException)
                {
                    tryCount++;
                }
                catch (RedisTimeoutException)
                {
                    tryCount++;
                }
            }
            throw new TimeoutException(
                $"Unable to load key from Redis due to timeouts after multiple attempts [{key}]");
        }

        /// <inheritdoc/>
        public override T Get<T>(string key, bool usePrefix = true)
        {
            var tryCount = 0;
            while (tryCount < 3)
            {
                try
                {
                    if (!Exists(key, usePrefix))
                    {
                        return default!;
                    }
                    return cacheClient.Get<T>(usePrefix ? PrefixKey(key) : key);
                }
                catch (RedisTimeoutException)
                {
                    tryCount++;
                }
            }
            throw new TimeoutException(
                $"Unable to load key from Redis due to timeouts after multiple attempts [{key}]");
        }

        /// <inheritdoc/>
        public override Task AddAsync<T>(string key, T obj, bool usePrefix = true, TimeSpan? timeToLive = null)
        {
            return RetryHelper.RetryOnExceptionAsync<RedisConnectionException>(
                () => cacheClient.AddAsync(usePrefix ? PrefixKey(key) : key, obj, TimespanToDateTimeOffset(timeToLive)));
        }

        /// <inheritdoc/>
        public override void Add<T>(string key, T obj, bool usePrefix = true, TimeSpan? timeToLive = null)
        {
            cacheClient.Add(usePrefix ? PrefixKey(key) : key, obj, TimespanToDateTimeOffset(timeToLive));
        }

        /// <inheritdoc/>
        public override Task RemoveAsync(string key, bool usePrefix = true)
        {
            return RetryHelper.RetryOnExceptionAsync<RedisConnectionException>(
                () => cacheClient.RemoveAsync(usePrefix ? PrefixKey(key) : key));
        }

        /// <inheritdoc/>
        public override void Remove(string key, bool usePrefix = true)
        {
            cacheClient.Remove(usePrefix ? PrefixKey(key) : key);
        }

        /// <inheritdoc/>
        public override Task RemoveByPatternAsync(string pattern, bool usePrefix = true)
        {
            return Task.WhenAll(connection!
                .GetServer(connection.GetEndPoints()[0])
                .Keys(pattern: usePrefix ? PrefixKey(pattern) : pattern, database: 0, pageSize: 5000)
                .Select(key => RetryHelper.RetryOnExceptionAsync<RedisConnectionException>(
                    () => connection.GetDatabase().KeyDeleteAsync(key))));
        }

        /// <summary>Connection string from application settings.</summary>
        /// <returns>A string.</returns>
        private static string ConnectionStringFromAppSettings()
        {
            var connectionString = new StringBuilder();
            connectionString.Append(CEFConfigDictionary.CachingRedisHostUri);
            if (Contract.CheckValidID(CEFConfigDictionary.CachingRedisHostPort))
            {
                connectionString.Append(':').Append(CEFConfigDictionary.CachingRedisHostPort);
            }
            if (Contract.CheckValidKey(CEFConfigDictionary.CachingRedisUsername))
            {
                connectionString.Append(",username=").Append(CEFConfigDictionary.CachingRedisUsername);
            }
            if (Contract.CheckValidKey(CEFConfigDictionary.CachingRedisPassword))
            {
                connectionString.Append(",password=").Append(CEFConfigDictionary.CachingRedisPassword);
            }
            if (CEFConfigDictionary.CachingRedisRequiredSSL)
            {
                connectionString.Append(",ssl=true");
            }
            if (CEFConfigDictionary.CachingRedisAbortConnect)
            {
                connectionString.Append(",abortConnect=true");
            }
            return connectionString.ToString();
        }

        /// <summary>Timespan to date time offset.</summary>
        /// <remarks>time to live value:<br/>
        /// null or 0 = No Expiration<br/>
        /// positive = Expires based on TimeSpan.</remarks>
        /// <param name="timeToLive">The time to live.</param>
        /// <returns>A DateTimeOffset.</returns>
        private static DateTimeOffset TimespanToDateTimeOffset(TimeSpan? timeToLive)
        {
            if (timeToLive == null || timeToLive == TimeSpan.Zero)
            {
                return DateTimeOffset.MaxValue;
            }
            if (timeToLive < TimeSpan.Zero)
            {
                return new(DateTime.UtcNow);
            }
            return new(DateTime.UtcNow.Add(timeToLive.Value));
        }

        /// <summary>Prefix key.</summary>
        /// <param name="key">The key.</param>
        /// <returns>A string.</returns>
        private string PrefixKey(string key)
        {
            return $"{prefix}:{key}";
        }
    }
}
#endif
