// <copyright file="CacheProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cache provider base class</summary>
namespace Clarity.Ecommerce.Providers.Caching
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers.Caching;

    /// <summary>A cache provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ICacheProviderBase"/>
    public abstract class CacheProviderBase : ProviderBase, ICacheProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Caching;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public bool IsInitialized { get; protected set; }

        /// <inheritdoc/>
        public abstract Task InitAsync(string? contextProfileName);

        public abstract void Init(string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<bool> ExistsAsync(string key, bool usePrefix = true);

        public abstract bool Exists(string key, bool usePrefix = true);

        /// <inheritdoc/>
        public abstract Task<T> GetAsync<T>(string key, bool usePrefix = true);

        public abstract T Get<T>(string key, bool usePrefix = true);

        /// <inheritdoc/>
        public abstract Task AddAsync<T>(string key, T obj, bool usePrefix = true, TimeSpan? timeToLive = null);

        public abstract void Add<T>(string key, T obj, bool usePrefix = true, TimeSpan? timeToLive = null);

        /// <inheritdoc/>
        public abstract Task RemoveAsync(string key, bool usePrefix = true);

        public abstract void Remove(string key, bool usePrefix = true);

        public abstract Task RemoveByPatternAsync(string pattern, bool usePrefix = true);
    }
}
