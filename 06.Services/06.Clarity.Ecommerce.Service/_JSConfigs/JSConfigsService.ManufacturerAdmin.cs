// <copyright file="JSConfigsService.ManufacturerAdmin.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the JS Configs service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using JetBrains.Annotations;
    using JSConfigs;
    using ServiceStack;

    [PublicAPI, UsedInManufacturerAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/ManufacturerAdminAlt", "GET", Summary = "")]
    public class GetManufacturerAdminCEFConfigAlt : IReturn<CEFConfig>
    {
    }

    public partial class JSConfigsService
    {
        public object Get(GetManufacturerAdminCEFConfigAlt _)
        {
            return GetManufacturerAdminConfigAlt();
        }

        private CEFConfig GetManufacturerAdminConfigAlt()
        {
            var result = CEFConfigDictionary.GetManufacturerAdminCEFConfigAlt();
            if (!result.ActionSucceeded)
            {
                throw new System.Configuration.ConfigurationErrorsException();
            }
            var content = result.Result;
            return content!;
        }
    }
}
