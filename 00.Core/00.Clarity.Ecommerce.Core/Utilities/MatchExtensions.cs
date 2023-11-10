// <copyright file="MatchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the match extensions class</summary>
namespace Clarity.Ecommerce
{
    /// <summary>A match extensions.</summary>
    public static class MatchExtensions
    {
        /// <summary>A bool extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this bool source)
        {
            return (ulong)source.GetHashCode();
        }

        /// <summary>A bool? extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this bool? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }

        /// <summary>A decimal extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this decimal source)
        {
            return (ulong)source.GetHashCode();
        }

        /// <summary>A decimal? extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this decimal? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }

        /// <summary>A double extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this double source)
        {
            return (ulong)source.GetHashCode();
        }

        /// <summary>A double? extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this double? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }

        /// <summary>A float extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this float source)
        {
            return (ulong)source.GetHashCode();
        }

        /// <summary>A float? extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this float? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }

        /// <summary>An int extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this int source)
        {
            return (ulong)source.GetHashCode();
        }

        /// <summary>An int? extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this int? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }

        /// <summary>An uint extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this uint source)
        {
            return (ulong)source.GetHashCode();
        }

        /// <summary>An uint? extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this uint? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }

        /// <summary>A long extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this long source)
        {
            return (ulong)source.GetHashCode();
        }

        /// <summary>A long? extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this long? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }

        /// <summary>An ulong extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this ulong source)
        {
            return (ulong)source.GetHashCode();
        }

        /// <summary>An ulong? extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this ulong? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }

        /// <summary>A string extension method that gets match code.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The match code.</returns>
        public static ulong GetMatchCode(this string? source)
        {
            return source == null ? 0ul : (ulong)source.GetHashCode();
        }
    }
}
