// <copyright file="SharedShipping.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shared shipping class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using Utilities;

    /// <summary>A shared shipping.</summary>
    internal class SharedShipping
    {
        /// <summary>Generates a tracking link.</summary>
        /// <param name="trackingNumber">The tracking number.</param>
        /// <returns>The tracking link.</returns>
        internal static (string href, string tag) GenTrackingLink(string trackingNumber)
        {
            if (!Contract.CheckValidKey(trackingNumber))
            {
                return (string.Empty, string.Empty);
            }
            foreach (var matcher in TrackingMatcher.Matchers)
            {
                if (!matcher.Regex.IsMatch(trackingNumber))
                {
                    continue;
                }
                return (matcher.Link + trackingNumber, $"<a href=\"{matcher.Link + trackingNumber}\">{trackingNumber}</a>");
            }
            return (string.Empty, string.Empty);
        }
    }
}
