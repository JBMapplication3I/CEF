// <copyright file="JSConfigsService.StoreAdmin.cs" company="clarity-ventures.com">
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

    [PublicAPI, UsedInStoreAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/StoreAdmin", "GET")]
    public class GetStoreAdminCEFConfig : IReturnVoid
    {
    }

    [PublicAPI, UsedInStoreAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/StoreAdminAlt", "GET")]
    public class GetStoreAdminCEFConfigAlt : IReturn<CEFConfig>
    {
    }

    public partial class JSConfigsService
    {
        public object Get(GetStoreAdminCEFConfig _)
        {
            return new HttpResult(
                GetStoreAdminConfig(false),
                "text/javascript")
            {
                LastModified = System.Diagnostics.Process.GetCurrentProcess().StartTime,
                MaxAge = TimeSpan.FromDays(1),
            };
        }

        public object Get(GetStoreAdminCEFConfigAlt _)
        {
            return GetStoreAdminConfigAlt();
        }

        private string GetStoreAdminConfig(bool minify)
        {
            EnsureLoaded();
            var result = CEFConfigDictionary.GetStoreAdminCEFConfig();
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

        private CEFConfig GetStoreAdminConfigAlt()
        {
            var result = CEFConfigDictionary.GetStoreAdminCEFConfigAlt();
            if (!result.ActionSucceeded)
            {
                throw new System.Configuration.ConfigurationErrorsException();
            }
            var content = result.Result;
            return content!;
        }
    }
}
