// <copyright file="Globals.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the globals class</summary>
namespace Clarity.Ecommerce
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Utilities;

    /// <summary>A globals.</summary>
    public static class Globals
    {
        /// <summary>The has role cookie prefix.</summary>
        public const string HasRoleCookiePrefix = "cef_hr_";

        /// <summary>The has any role cookie prefix.</summary>
        public const string HasAnyRoleCookiePrefix = "cef_har_";

        /// <summary>The has permission cookie prefix.</summary>
        public const string HasPermissionCookiePrefix = "cef_hp_";

        /// <summary>The has any permission cookie prefix.</summary>
        public const string HasAnyPermissionCookiePrefix = "cef_hap_";

        /// <summary>Full pathname of the cef root file.</summary>
        private static string? cefRootPath;

        /// <summary>Gets the full pathname of the cef root file.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <value>The full pathname of the cef root file.</value>
        public static string CEFRootPath
        {
            get
            {
                if (cefRootPath != null)
                {
                    return cefRootPath;
                }
                const string AzurePattern = @"(^.*(?:site\\(?:api|ui)))\\";
                const string Pattern = @"(^.*)\\(?:(?:\d\d\.)?Clarity\.Ecommerce\.|\d\d\.T4\.ServiceStack\.CodeGenerator\.TypeScript|((Brand|Store|Vendor)?Admin|Storefront|Scheduler)\\(Service|Scheduler)\\bin)";
                const string ReSharperShadowCopyPattern = @"(^.*\\AppData\\Local\\Temp\\ReSharperTestRunner_[A-Za-z0-9]+\\[A-Za-z0-9]+)";
                var location = Assembly.GetExecutingAssembly().Location;
                var path = ExtractDirectoryFromPath(location);
                var match = Regex.Match(path, Pattern);
                if (!match.Success)
                {
                    match = Regex.Match(path, AzurePattern);
                }
                if (!match.Success)
                {
#if NET5_0_OR_GREATER
                    path = ExtractDirectoryFromPath(Assembly.GetExecutingAssembly().Location);
#else
                    path = ExtractDirectoryFromPath(Assembly.GetExecutingAssembly().CodeBase);
#endif
                    match = Regex.Match(path, Pattern);
                    if (!match.Success)
                    {
                        match = Regex.Match(path, AzurePattern);
                    }
                    if (!match.Success)
                    {
                        match = Regex.Match(path, ReSharperShadowCopyPattern);
                    }
                    if (!match.Success)
                    {
#pragma warning disable CA1065
                        throw new(
                            "Unexpected value for CEF_RootPath: Expecting values similar to"
                            + $"\r\n'C:\\Data\\Projects\\CEF\\07.Portals\\Portal\\Project\\bin\\Debug' matching this RegEx pattern /{Pattern}/g."
                            + $"\r\nOR 'D:\\Home\\Site\\api' matching this RegEx pattern /{AzurePattern}/g."
                            + $"\r\nOR 'C:\\Users\\Username\\AppData\\Local\\Temp\\ReSharperTestRunner_something\\something' matching this RegEx pattern /{ReSharperShadowCopyPattern}/g."
                            + $"\r\nValue was '{path}'");
#pragma warning restore CA1065
                    }
                }
                var root = match.Groups[1].Value;
                if (root.EndsWith("\\ThirdParty"))
                {
                    root = root.Replace("\\ThirdParty", string.Empty);
                }
                if (root.EndsWith("\\Providers"))
                {
                    root = root.Replace("\\Providers", string.Empty);
                }
                if (root.EndsWith("\\04.Providers"))
                {
                    root = root.Replace("\\04.Providers", string.Empty);
                }
                if (root.EndsWith("\\07.Portals"))
                {
                    root = root.Replace("\\07.Portals", string.Empty);
                }
                if (root.EndsWith("\\11.T4"))
                {
                    root = root.Replace("\\11.T4", string.Empty);
                }
                return cefRootPath = $@"{root}\";
            }
        }

        private static string ExtractDirectoryFromPath(string path)
        {
            if (!Contract.CheckValidKey(path))
            {
                return string.Empty;
            }
            var uri = new UriBuilder(path);
            path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path) ?? string.Empty;
        }
    }
}
