// <copyright file="JSConfigsService.VendorAdmin.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the JS Configs service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using JetBrains.Annotations;
    using JSConfigs;
    using ServiceStack;

    [PublicAPI, UsedInVendorAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/VendorAdmin", "GET", Summary = "")]
    public class GetVendorAdminCEFConfig : IReturnVoid
    {
    }

    [PublicAPI, UsedInVendorAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/VendorAdminAlt", "GET", Summary = "")]
    public class GetVendorAdminCEFConfigAlt : IReturn<CEFConfig>
    {
    }

    public partial class JSConfigsService
    {
        public object Get(GetVendorAdminCEFConfig _)
        {
            return new HttpResult(
                GetVendorAdminConfig(false),
                "text/javascript")
            {
                LastModified = System.Diagnostics.Process.GetCurrentProcess().StartTime,
                MaxAge = TimeSpan.FromDays(1),
            };
        }

        public object Get(GetVendorAdminCEFConfigAlt _)
        {
            return GetVendorAdminConfigAlt();
        }

        private string GetVendorAdminConfig(bool minify)
        {
            EnsureLoaded();
            var result = CEFConfigDictionary.GetVendorAdminCEFConfig();
            if (!result.ActionSucceeded)
            {
                throw new System.Configuration.ConfigurationErrorsException();
            }
            var content = result.Result;
            if (minify)
            {
                content = content!
                    .Replace("\t", string.Empty)
                    .Replace("\r", string.Empty)
                    .Replace("\n", string.Empty);
            }
            return content!;
        }

        private CEFConfig GetVendorAdminConfigAlt()
        {
            var result = CEFConfigDictionary.GetVendorAdminCEFConfigAlt();
            if (!result.ActionSucceeded)
            {
                throw new System.Configuration.ConfigurationErrorsException();
            }
            var content = result.Result;
            return content!;
        }
    }
}
