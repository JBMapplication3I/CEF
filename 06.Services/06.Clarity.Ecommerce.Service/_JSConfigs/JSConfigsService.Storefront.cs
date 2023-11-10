// <copyright file="JSConfigsService.Storefront.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the JS Configs service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using ServiceStack;

    [PublicAPI, UsedInStorefront,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/StoreFront", "GET", Summary = "")]
    public class GetStoreFrontCEFConfig : IReturnVoid
    {
    }

    [PublicAPI, UsedInStorefront,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/StoreFrontAlt", "GET", Summary = "")]
    public class GetStoreFrontCEFConfigAlt : IReturn<string>
    {
    }

    public partial class JSConfigsService : ClarityEcommerceServiceBase
    {
        public object Get(GetStoreFrontCEFConfig _)
        {
            return new HttpResult(GetStoreFrontConfig(false), "text/javascript")
            {
                LastModified = System.Diagnostics.Process.GetCurrentProcess().StartTime,
                MaxAge = TimeSpan.FromDays(1),
            };
        }

        public object Get(GetStoreFrontCEFConfigAlt _)
        {
            return GetStoreFrontConfigAlt();
        }

        private string GetStoreFrontConfig(bool minify)
        {
            EnsureLoaded();
            var result = CEFConfigDictionary.GetStoreFrontCEFConfig(null);
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

        private string GetStoreFrontConfigAlt()
        {
            var result = CEFConfigDictionary.GetStoreFrontCEFConfigAlt(null);
            if (!result.ActionSucceeded)
            {
                throw new System.Configuration.ConfigurationErrorsException();
            }
            var content = result.Result;
            return content!;
        }
    }

    [PublicAPI]
    public partial class JSConfigsService
    {
        private List<string>? coreUrnIDs;

        private List<string> CoreUrnIDs
        {
            get
            {
                if (coreUrnIDs != null)
                {
                    return coreUrnIDs;
                }
                return coreUrnIDs = new()
                {
                    UrnId.Create<GetAdminCEFConfig>(string.Empty),
                    UrnId.Create<GetStoreAdminCEFConfig>(string.Empty),
                    UrnId.Create<GetBrandAdminCEFConfig>(string.Empty),
                    UrnId.Create<GetVendorAdminCEFConfig>(string.Empty),
                    UrnId.Create<GetStoreFrontCEFConfig>(string.Empty),
                };
            }
        }

        public async Task<object?> Delete(ClearJSConfigsCaches _)
        {
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName: null).ConfigureAwait(false);
            await client!.RemoveByPatternAsync("HardSoftStops:*").ConfigureAwait(false);
            var urn = string.Empty;
            /*
            if (CEFConfigDictionary.BrandsEnabled)
            {
                urn += ":" + new Uri(Request.AbsoluteUri).Host.Replace(":", "{colon}");
            }
            */
            foreach (var key in CoreUrnIDs)
            {
                await ClearCachePrefixedAsync($"{key}{urn}").ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        private void EnsureLoaded()
        {
            CEFConfigDictionary.Load();
        }
    }
}
