// <copyright file="WebHelpers.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the web helpers class</summary>
namespace Microsoft.Owin.Helpers
{
    using Infrastructure;

    /// <summary>Provides helper methods for processing requests.</summary>
    public static class WebHelpers
    {
        /// <summary>Parses an HTTP form body.</summary>
        /// <param name="text">The HTTP form body to parse.</param>
        /// <returns>The <see cref="IFormCollection" /> object containing the parsed HTTP form body.</returns>
        public static IFormCollection ParseForm(string text)
        {
            return OwinHelpers.GetForm(text);
        }
    }
}
