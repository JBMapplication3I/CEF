// <copyright file="Constants.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the constants class</summary>
namespace Microsoft.Owin.Infrastructure
{
    /// <summary>A constants.</summary>
    internal static class Constants
    {
        /// <summary>The HTTP date format.</summary>
        internal const string HttpDateFormat = "r";

        /// <summary>The HTTPS.</summary>
        internal const string Https = "HTTPS";

        /// <summary>A headers.</summary>
        internal static class Headers
        {
            /// <summary>The accept.</summary>
            internal const string Accept = "Accept";

            /// <summary>The cache control.</summary>
            internal const string CacheControl = "Cache-Control";

            /// <summary>Length of the content.</summary>
            internal const string ContentLength = "Content-Length";

            /// <summary>Type of the content.</summary>
            internal const string ContentType = "Content-Type";

            /// <summary>The tag.</summary>
            internal const string ETag = "ETag";

            /// <summary>The expires.</summary>
            internal const string Expires = "Expires";

            /// <summary>The host.</summary>
            internal const string Host = "Host";

            /// <summary>The location.</summary>
            internal const string Location = "Location";

            /// <summary>Type of the media.</summary>
            internal const string MediaType = "Media-Type";

            /// <summary>The set cookie.</summary>
            internal const string SetCookie = "Set-Cookie";
        }
    }
}
