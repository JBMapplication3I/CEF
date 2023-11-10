// <copyright file="RequestExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the clarity eCommerce service base class</summary>
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Threading.Tasks;
    using Core;

    /// <summary>A request extensions.</summary>
    public static class RequestExtensions
    {
        private static readonly object cacheLock = new object();
        /// <summary>A ServiceStack.Web.IRequest extension method that converts this result to an optimized result using
        /// cache.</summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="requestContext">Context for the request.</param>
        /// <param name="cacheClient">   The cache client.</param>
        /// <param name="cacheKey">      The cache key.</param>
        /// <param name="expireCacheIn"> The expire cache in.</param>
        /// <param name="factoryFn">     The factory function.</param>
        /// <param name="lastModified">  The last modified.</param>
        /// <returns>The given data converted to a Task{object}.</returns>
        public static async Task<object?> CEFToOptimizedResultUsingCacheAsync<TResult>(
            this ServiceStack.Web.IRequest requestContext,
            ServiceStack.Caching.ICacheClient cacheClient,
            string cacheKey,
            TimeSpan? expireCacheIn,
            Func<Task<TResult>> factoryFn,
            DateTime? lastModified)
        {
            object? cachedResult = null;
            lock (cacheLock)
            {
                cachedResult = cacheClient.CEFResolveFromCache(cacheKey, requestContext);
            }
            if (cachedResult is not null)
            {
                return cachedResult;
            }
            var responseDto = await factoryFn().ConfigureAwait(false);
            if (responseDto is null)
            {
                // Don't try to cache as this causes errors
                return null!;
            }
            lock (cacheLock)
            {
                return cacheClient.CEFCache(
                    cacheKey: cacheKey,
                    responseDto: responseDto,
                    request: requestContext,
                    expireCacheIn: expireCacheIn,
                    lastModifiedData: lastModified);
            }
        }

        /// <summary>A ServiceStack.Web.IRequest extension method that converts this result to an optimized result using
        /// cache.</summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="requestContext">Context for the request.</param>
        /// <param name="cacheClient">   The cache client.</param>
        /// <param name="cacheKey">      The cache key.</param>
        /// <param name="expireCacheIn"> The expire cache in.</param>
        /// <param name="factoryFn">     The factory function.</param>
        /// <param name="lastModified">  The last modified.</param>
        /// <returns>The given data converted to a Task{object}.</returns>
        public static object? CEFToOptimizedResultUsingCache<TResult>(
            this ServiceStack.Web.IRequest requestContext,
            ServiceStack.Caching.ICacheClient cacheClient,
            string cacheKey,
            TimeSpan? expireCacheIn,
            Func<TResult> factoryFn,
            DateTime? lastModified)
        {
            var cachedResult = cacheClient.CEFResolveFromCache(cacheKey, requestContext);
            if (cachedResult is not null)
            {
                return cachedResult;
            }
            var responseDto = factoryFn();
            if (responseDto is null)
            {
                // Don't try to cache as this causes errors
                return null!;
            }
            return cacheClient.CEFCache(
                cacheKey: cacheKey,
                responseDto: responseDto,
                request: requestContext,
                expireCacheIn: expireCacheIn,
                lastModifiedData: lastModified);
        }
    }
}
