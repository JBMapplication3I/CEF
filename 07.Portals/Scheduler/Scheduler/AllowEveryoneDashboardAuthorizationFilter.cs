// <copyright file="AllowEveryoneDashboardAuthorizationFilter.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the allow everyone dashboard authorization filter class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    using Hangfire.Dashboard;

    /// <summary>An allow everyone dashboard authorization filter.</summary>
    /// <seealso cref="Hangfire.Dashboard.IDashboardAuthorizationFilter"/>
    public class AllowEveryoneDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>Authorizes the given context.</summary>
        /// <param name="context">The context.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
