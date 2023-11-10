// <copyright file="ProductPricePointsService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product price points service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate,
        Route("/Products/ProductPricePoints/ByAccountPricePoints", "POST",
            Summary = "Use to get a list of product price points", Priority = 1)]
    public class GetProductPricePointsByAccountPricePoints
        : ProductPricePointSearchModel, IReturn<ProductPricePointPagedResults>
    {
    }

    [PublicAPI,
        Route("/Products/ProductPricePoints/ByKeys", "POST",
            Summary = "Use to get a specific Product Price Point by Custom Key")]
    public class GetProductPricePointByKeys : ProductPricePointSearchModel, IReturn<ProductPricePointModel>
    {
    }

    [PublicAPI,
        Route("/Products/ProductPricePoints/ByKeys/Exists", "POST",
            Summary = "Use to get the id of a specific Product Price Point by Custom Key")]
    public class CheckProductPricePointExistsByKeys : ProductPricePointSearchModel, IReturn<int?>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Pricing/ClearCache", "DELETE",
            Summary = "Clears the Price Cache")]
    public partial class ClearPriceCache : IReturn<CEFActionResponse>
    {
    }

    [PublicAPI,
        Route("/Products/ProductPricePoints/GetHash", "GET",
            Summary = "Get a digest of ProductPricePoints")]
    public class GetProductPricePointDigest : IReturn<List<DigestModel>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetProductPricePointsByAccountPricePoints request)
        {
            request.PricePointIDs = (await CurrentAccountOrThrow401Async().ConfigureAwait(false))
                .AccountPricePoints!
                .Select(app => (int?)app.SlaveID)
                .ToList();
            return await GetPagedResultsAsync<IProductPricePointModel, ProductPricePointModel, IProductPricePointSearchModel, ProductPricePointPagedResults>(
                    request,
                    false,
                    Workflows.ProductPricePoints)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(GetProductPricePointByKeys request)
        {
            return await Workflows.ProductPricePoints.GetAsync(request, null).ConfigureAwait(false);
        }

        public async Task<object?> Post(CheckProductPricePointExistsByKeys request)
        {
            return await Workflows.ProductPricePoints.CheckExistsAsync(request, null).ConfigureAwait(false);
        }

        public async Task<object?> Delete(ClearPriceCache _)
        {
            await RegistryLoaderWrapper.GetInstance<IPricingFactory>(ServiceContextProfileName)
                .RemoveAllCachedPricesAsync(ServiceContextProfileName)
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        public async Task<object?> Get(GetProductPricePointDigest _)
        {
            return await Workflows.ProductPricePoints.GetDigestAsync(null).ConfigureAwait(false);
        }
    }
}
