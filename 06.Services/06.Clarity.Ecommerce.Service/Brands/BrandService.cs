// <copyright file="BrandService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate,
        Route("/Brands/Brand/Full/ID/{ID}", "GET", Summary = "Use to get a specific brand")]
    public class GetBrandFull : ImplementsIDBase, IReturn<BrandModel>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Brands/Brand/Full/Key/{Key}", "GET", Summary = "Use to get a specific brand")]
    public class GetBrandFullByKey : ImplementsKeyBase, IReturn<BrandModel>
    {
    }

    [PublicAPI,
        Route("/Brands/Brand/Current", "GET", Priority = 1,
            Summary = "Use to get the current brand based on url, sub-domain or sub-folder")]
    public partial class GetCurrentBrand : IReturn<CEFActionResponse<BrandModel>>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Brands/Brand/Current/Administration", "GET",
            Summary = "Use to get the brand that the current user has administrative rights to (limited to brand admins)")]
    public partial class GetCurrentBrandAdministration : IReturn<CEFActionResponse<BrandModel>>
    {
    }

    [PublicAPI]
    public partial class BrandService
    {
        public async Task<object?> Get(GetCurrentBrand request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    ParseRequestUrlReferrerToBrandLastModifiedAsync,
                    async () => (await ParseRequestUrlReferrerToBrandAsync().ConfigureAwait(false) as BrandModel)
                        .WrapInPassingCEFARIfNotNull())
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetBrandFull request)
        {
            // NOTE: Never cached, for admins only
            return await Workflows.Brands.GetFullAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetBrandFullByKey request)
        {
            // NOTE: Never cached, for admins only
            return await Workflows.Brands.GetFullAsync(request.Key, null).ConfigureAwait(false);
        }

        // Brand Administration
        public async Task<object?> Get(GetCurrentBrandAdministration _)
        {
            // NOTE: Never cached, for admins only
            try
            {
                var result = (BrandModel?)await Workflows.Brands.GetAsync(
                        await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false),
                        contextProfileName: null)
                    .ConfigureAwait(false);
                return result.WrapInPassingCEFARIfNotNull();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: "GetCurrentBrandAdministration Error",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: null)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<BrandModel>(
                    "Unable to locate current brand the user would be administrator of");
            }
        }
    }
}
