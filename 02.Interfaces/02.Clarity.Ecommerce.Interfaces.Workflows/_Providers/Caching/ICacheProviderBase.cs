// <copyright file="ICacheProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICacheProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Caching
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Interface for cache provider base.</summary>
    public interface ICacheProviderBase : IProviderBase
    {
        /// <summary>Gets a value indicating whether this ICEFCache is initialized.</summary>
        /// <value>True if this ICEFCache is initialized, false if not.</value>
        bool IsInitialized { get; }

        /// <summary>Initialises the CEF Cache Client.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task InitAsync(string? contextProfileName);

        /// <summary>Check if the cache contains a value for a specific key.</summary>
        /// <param name="key">      The key used to store object.</param>
        /// <param name="usePrefix">True to use prefix.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> ExistsAsync(string key, bool usePrefix = true);

        /// <summary>Get an object stored in cache by key.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="key">      The key used to store object.</param>
        /// <param name="usePrefix">True to use prefix.</param>
        /// <returns>A Task{T}.</returns>
        Task<T> GetAsync<T>(string key, bool usePrefix = true);

        /// <summary>Save an object in the cache.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="key">       The key to store object against.</param>
        /// <param name="obj">       The object to store.</param>
        /// <param name="usePrefix"> True to use prefix.</param>
        /// <param name="timeToLive">The time to live.</param>
        /// <returns>A Task.</returns>
        Task AddAsync<T>(string key, T obj, bool usePrefix = true, TimeSpan? timeToLive = null);

        /// <summary>Delete an object from cache using a key.</summary>
        /// <param name="key">      The key the object is stored using.</param>
        /// <param name="usePrefix">True to use prefix.</param>
        /// <returns>A Task.</returns>
        Task RemoveAsync(string key, bool usePrefix = true);

        /// <summary>Delete an object from cache using a key pattern.</summary>
        /// <param name="pattern">  The key pattern matching the keys the objects are stored using.</param>
        /// <param name="usePrefix">True to use prefix.</param>
        /// <returns>A Task.</returns>
        Task RemoveByPatternAsync(string pattern, bool usePrefix = true);
    }
}
