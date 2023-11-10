// <copyright file="JSConfigsService.Admin.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the js configs service class</summary>
// ReSharper disable MissingXmlDoc
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using ServiceStack;

    [PublicAPI, UsedInAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/Admin", "GET")]
    public class GetAdminCEFConfig : IReturnVoid
    {
    }

    [PublicAPI, UsedInAdmin,
        Route("/JSConfigs/GetAppSettings", "POST",
            Summary = "Get the app settings values from the core config")]
    public class GetAppSettings : IReturn<CEFActionResponse<Dictionary<Type, Dictionary<string, object>>>>
    {
    }

    [PublicAPI, UsedInAdmin,
        Route("/JSConfigs/UpdateAppSettings", "POST",
            Summary = "Update the app settings values to the core config")]
    public class UpdateAppSettings : IReturn<CEFActionResponse>
    {
        public Dictionary<Type, Dictionary<string, object>> KeysToUpdate { get; set; } = null!;
    }

    [PublicAPI, UsedInAdmin,
        Route("/JSConfigs/ClearCaches", "DELETE",
            Summary = "Clears the caches for the JSConfigs endpoints")]
    public class ClearJSConfigsCaches : IReturn<CEFActionResponse>
    {
    }

    public partial class JSConfigsService
    {
        public object Post(GetAppSettings _)
        {
            return CEFConfigDictionary.Dict.WrapInPassingCEFAR();
        }

        public object Post(UpdateAppSettings request)
        {
            return CEFConfigDictionary.TrySetFromUI(request.KeysToUpdate);
        }

        public object Get(GetAdminCEFConfig _)
        {
            return new HttpResult(GetAdminConfig(false), "text/javascript")
            {
                LastModified = System.Diagnostics.Process.GetCurrentProcess().StartTime,
                MaxAge = TimeSpan.FromDays(1),
            };
        }

        private string GetAdminConfig(bool minify)
        {
            EnsureLoaded();
            var result = CEFConfigDictionary.GetAdminCEFConfig();
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
    }
}
