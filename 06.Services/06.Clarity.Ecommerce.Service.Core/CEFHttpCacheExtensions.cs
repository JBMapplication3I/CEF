// <copyright file="CEFHttpCacheExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef HTTP cache extensions class</summary>
namespace Clarity.Ecommerce.Service.Core
{
    using System;
    using System.Globalization;
    using ServiceStack;
    using ServiceStack.Text;
    using ServiceStack.Web;

    /// <summary>A cef HTTP cache extensions.</summary>
    public static class CEFHttpCacheExtensions
    {
        /// <summary>An IResponse extension method that cef end not modified.</summary>
        /// <param name="res">        The res to act on.</param>
        /// <param name="description">The description.</param>
        public static void CEFEndNotModified(this IResponse res, string? description = null)
        {
            res.StatusCode = 304;
            res.StatusDescription = description ?? HostContext.ResolveLocalizedString("Not Modified");
            res.EndRequest();
        }

        /// <summary>A CacheControl extension method that cef has.</summary>
        /// <param name="cache">The cache to act on.</param>
        /// <param name="flag"> The flag.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CEFHas(this CacheControl cache, CacheControl flag)
        {
            return (ulong)(flag & cache) > 0UL;
        }

        /// <summary>An IRequest extension method that cefe tag match.</summary>
        /// <param name="req"> The req to act on.</param>
        /// <param name="eTag">The tag.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CEFETagMatch(this IRequest req, string? eTag)
        {
            return !string.IsNullOrEmpty(eTag)
                && eTag.CEFStripWeakRef().Quoted() == req.Headers["If-None-Match"].CEFStripWeakRef().Quoted();
        }

        /// <summary>An IRequest extension method that cef not modified since.</summary>
        /// <param name="req">         The req to act on.</param>
        /// <param name="lastModified">The last modified.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CEFNotModifiedSince(this IRequest req, DateTime? lastModified)
        {
            if (!lastModified.HasValue)
            {
                return false;
            }
            var header = req.Headers["If-Modified-Since"];
            if (header == null
                || !DateTime.TryParse(header, new DateTimeFormatInfo(), DateTimeStyles.RoundtripKind, out var result))
            {
                return false;
            }
            return result >= lastModified.Value.Truncate(TimeSpan.FromSeconds(1.0)).ToUniversalTime();
        }

        /// <summary>An IRequest extension method that cef has valid cache.</summary>
        /// <param name="req"> The req to act on.</param>
        /// <param name="eTag">The tag.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CEFHasValidCache(this IRequest req, string eTag)
        {
            return req.CEFETagMatch(eTag);
        }

        /// <summary>An IRequest extension method that cef has valid cache.</summary>
        /// <param name="req">         The req to act on.</param>
        /// <param name="lastModified">The last modified.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CEFHasValidCache(this IRequest req, DateTime? lastModified)
        {
            return req.CEFNotModifiedSince(lastModified);
        }

        /// <summary>An IRequest extension method that cef has valid cache.</summary>
        /// <param name="req">         The req to act on.</param>
        /// <param name="eTag">        The tag.</param>
        /// <param name="lastModified">The last modified.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CEFHasValidCache(this IRequest req, string eTag, DateTime? lastModified)
        {
            return req.CEFETagMatch(eTag) || req.CEFNotModifiedSince(lastModified);
        }

        /// <summary>A CEFHttpCacheFeature extension method that cef should add last modified to optimized results.</summary>
        /// <param name="feature">The feature to act on.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CEFShouldAddLastModifiedToOptimizedResults(this CEFHttpCacheFeature feature)
        {
            return feature?.CacheControlForOptimizedResults != null;
        }

        /// <summary>A string extension method that cef strip weak reference.</summary>
        /// <param name="eTag">The eTag to act on.</param>
        /// <returns>A string.</returns>
        internal static string? CEFStripWeakRef(this string? eTag)
        {
            return eTag == null || !eTag.StartsWith("W/") ? eTag : eTag[2..];
        }
    }
}
