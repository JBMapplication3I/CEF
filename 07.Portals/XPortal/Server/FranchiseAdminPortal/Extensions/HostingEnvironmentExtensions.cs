// <copyright file="HostingEnvironmentExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hosting environment extensions class</summary>
namespace Portals.Shared.Environments
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>A hosting environment extensions.</summary>
    public static class HostingEnvironmentExtensions
    {
        /// <summary>(Immutable) The local environment.</summary>
        public const string LocalEnvironment = "Local";

        /// <summary>(Immutable) The qa environment.</summary>
        public const string QAEnvironment = "QA";

        /// <summary>(Immutable) The uat environment.</summary>
        public const string UATEnvironment = "UAT";

        /// <summary>An IWebHostEnvironment extension method that query if 'hostingEnvironment' is local.</summary>
        /// <param name="hostingEnvironment">The hostingEnvironment to act on.</param>
        /// <returns>True if local, false if not.</returns>
        public static bool IsLocal(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(LocalEnvironment);
        }

        /// <summary>An IWebHostEnvironment extension method that query if 'hostingEnvironment' is qa.</summary>
        /// <param name="hostingEnvironment">The hostingEnvironment to act on.</param>
        /// <returns>True if qa, false if not.</returns>
        public static bool IsQA(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(QAEnvironment);
        }

        /// <summary>An IWebHostEnvironment extension method that query if 'hostingEnvironment' is uat.</summary>
        /// <param name="hostingEnvironment">The hostingEnvironment to act on.</param>
        /// <returns>True if uat, false if not.</returns>
        public static bool IsUAT(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(UATEnvironment);
        }
    }
}
