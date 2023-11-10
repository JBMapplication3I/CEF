// <copyright file="JSRuntimeExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the js runtime extensions class</summary>
namespace Clarity.Ecommerce.MVC.Interop
{
    using System;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;
    using Utilities;

    /// <summary>The js runtime extensions.</summary>
    public static class JSRuntimeExtensions
    {
        /// <summary>(Immutable) The cookie RegEx.</summary>
        private static readonly Regex CookieRegex = new(
            @"(?<name>[A-Za-z0-9\s_-]+)=(?<value>.+)?(;\s*expires=\d+\.\d+)?()",
            RegexOptions.IgnoreCase);

        /// <summary>An IJSRuntime extension method that reads browser cookies asynchronous.</summary>
        /// <param name="jsRuntime">     The jsRuntime to act on.</param>
        /// <param name="sourceFilePath">Full pathname of the source file.</param>
        /// <param name="memberName">    Name of the member.</param>
        /// <returns>The browser cookies asynchronous.</returns>
        public static async Task<CookieCollection> ReadBrowserCookiesAsync(
            this IJSRuntime jsRuntime,
            [CallerFilePath] string sourceFilePath = "",
            [CallerMemberName] string? memberName = "")
        {
            var debugPrefix = sourceFilePath + "." + memberName + "->JSRuntimeExtensions.ReadBrowserCookiesAsync: ";
            var currentCookies = await jsRuntime.InvokeAsync<string>(
                    "blazorExtensions.ReadCookies")
                .ConfigureAwait(false);
            CookieCollection retVal = new();
            if (!Contract.CheckValidKey(currentCookies))
            {
                ////Console.WriteLine(debugPrefix + "There were no cookies in the browser");
                return retVal;
            }
            var rawCookieArray = currentCookies.Split(";", StringSplitOptions.TrimEntries);
            foreach (var rawCookie in rawCookieArray)
            {
                var match = CookieRegex.Match(rawCookie);
                if (!match.Success)
                {
                    Console.WriteLine(debugPrefix + "Raw cookie didn't match regex:");
                    Console.WriteLine(rawCookie);
                    continue;
                }
                var name = match.Groups["name"].Value;
                var value = match.Groups["value"].Value;
                // TODO: Path & domain property parsing when set
                retVal.Add(new Cookie(name, value));
            }
            return retVal;
        }

        /// <summary>An IJSRuntime extension method that writes a cookie to browser.</summary>
        /// <param name="jsRuntime">        The jsRuntime to act on.</param>
        /// <param name="name">             The name.</param>
        /// <param name="value">            The value.</param>
        /// <param name="expiresAfterXDays">The cookie expires after X days (null to never expire).</param>
        /// <param name="path">             Full pathname of the file.</param>
        /// <returns>A Task.</returns>
        public static ValueTask<string> WriteCookieToBrowserAsync(
            this IJSRuntime jsRuntime,
            string name,
            string? value,
            int? expiresAfterXDays = null,
            string path = "/")
        {
            return jsRuntime.InvokeAsync<string>(
                "blazorExtensions.WriteCookie",
                name,
                value,
                expiresAfterXDays,
                path);
        }

        /// <summary>An IJSRuntime extension method that sets title in browser.</summary>
        /// <param name="jSRuntime">The jSRuntime to act on.</param>
        /// <param name="title">    The title.</param>
        /// <returns>A Task.</returns>
        public static async Task SetTitleInBrowserAsync(this IJSRuntime jSRuntime, string title)
        {
            _ = await jSRuntime.InvokeAsync<string>("blazorExtensions.SetTitle", title).ConfigureAwait(false);
        }
    }
}
