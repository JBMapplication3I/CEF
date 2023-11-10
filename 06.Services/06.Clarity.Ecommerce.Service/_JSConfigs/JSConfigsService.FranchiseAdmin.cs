// <copyright file="JSConfigsService.FranchiseAdmin.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the JS Configs service class</summary>
// ReSharper disable MissingXmlDoc
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using JetBrains.Annotations;
    using JSConfigs;
    using ServiceStack;

    [PublicAPI, UsedInFranchiseAdmin,
        Core.CEFCacheResponse(Duration = 60 * 60, MaxAge = 30 * 60),
        Route("/JSConfigs/FranchiseAdminAlt", "GET", Summary = "")]
    public class GetFranchiseAdminCEFConfigAlt : IReturn<CEFConfig>
    {
    }

    public partial class JSConfigsService
    {
        public object Get(GetFranchiseAdminCEFConfigAlt _)
        {
            return GetFranchiseAdminConfigAlt();
        }

        private CEFConfig GetFranchiseAdminConfigAlt()
        {
            var result = CEFConfigDictionary.GetFranchiseAdminCEFConfigAlt();
            if (!result.ActionSucceeded)
            {
                throw new System.Configuration.ConfigurationErrorsException();
            }
            var content = result.Result;
            return content!;
        }
    }
}
