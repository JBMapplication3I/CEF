// <copyright file="NavigationManagerExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the navigation manager extensions class</summary>
namespace Clarity.Ecommerce.UI.XPortal.Client
{
    using System;
    using System.Collections.Specialized;
    using System.Web;
    using Microsoft.AspNetCore.Components;

    /// <summary>https://jasonwatmore.com/post/2020/08/09/blazor-webassembly-get-query-string-parameters-with-navigation-manager.</summary>
    public static class NavigationManagerExtensions
    {
        /// <summary>A NavigationManager extension method that queries a string.</summary>
        /// <param name="navigationManager">The navigationManager to act on.</param>
        /// <returns>The string.</returns>
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            var query = new Uri(navigationManager.Uri).Query;
            return HttpUtility.ParseQueryString(query);
        }

        /// <summary>A NavigationManager extension method that queries a string.</summary>
        /// <param name="navigationManager">The navigationManager to act on.</param>
        /// <param name="key">              The key.</param>
        /// <returns>The string.</returns>
        public static string? QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }
    }
}
