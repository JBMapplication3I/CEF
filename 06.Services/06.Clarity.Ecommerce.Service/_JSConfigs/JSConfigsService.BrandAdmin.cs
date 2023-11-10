// <copyright file="JSConfigsService.BrandAdmin.cs" company="clarity-ventures.com">
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

    [PublicAPI, UsedInBrandAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/BrandAdmin", "GET")]
    public class GetBrandAdminCEFConfig : IReturnVoid
    {
    }

    [PublicAPI, UsedInBrandAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/BrandAdminAlt", "GET")]
    public class GetBrandAdminCEFConfigAlt : IReturn<CEFConfig>
    {
    }

    public partial class JSConfigsService
    {
        public object Get(GetBrandAdminCEFConfig _)
        {
            return new HttpResult(
                GetBrandAdminConfig(false),
                "text/javascript")
            {
                LastModified = System.Diagnostics.Process.GetCurrentProcess().StartTime,
                MaxAge = TimeSpan.FromDays(1),
            };
        }

        public object Get(GetBrandAdminCEFConfigAlt _)
        {
            return GetBrandAdminConfigAlt();
        }

        private string GetBrandAdminConfig(bool minify)
        {
            EnsureLoaded();
            var result = CEFConfigDictionary.GetBrandAdminCEFConfig();
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

        private CEFConfig GetBrandAdminConfigAlt()
        {
            var result = CEFConfigDictionary.GetBrandAdminCEFConfigAlt();
            if (!result.ActionSucceeded)
            {
                throw new System.Configuration.ConfigurationErrorsException();
            }
            var content = result.Result;
            return content!;
        }
    }
}
