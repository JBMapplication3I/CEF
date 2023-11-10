// <copyright file="Constants.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the constants class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    /// <summary>A constants.</summary>
    internal static class Constants
    {
        /// <summary>The correlation prefix.</summary>
        internal const string CorrelationPrefix = ".AspNet.Correlation.";

        /// <summary>The security authenticate.</summary>
        public static string SecurityAuthenticate;

        /// <summary>Initializes static members of the Microsoft.Owin.Security.Infrastructure.Constants class.</summary>
        static Constants()
        {
            SecurityAuthenticate = "security.Authenticate";
        }
    }
}
