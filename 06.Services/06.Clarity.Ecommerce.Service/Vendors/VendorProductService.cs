// <copyright file="VendorProductService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using ServiceStack;

    [Authenticate,
        Route("/Vendors/VendorProducts/ByProductID/{ID}", "GET",
            Summary = "Get Vendors By ProductID")]
    public class GetVendorProductsByProductID : ImplementsIDBase, IReturn<VendorProductPagedResults>
    {
    }

    public partial class VendorProductService
    {
        public async Task<object?> Get(GetVendorProductsByProductID request)
        {
            return await GetPagedResultsAsync<IVendorProductModel, VendorProductModel, IVendorProductSearchModel, VendorProductPagedResults>(
                    new VendorProductSearchModel { ProductID = request.ID },
                    true,
                    Workflows.VendorProducts)
                .ConfigureAwait(false);
        }
    }
}
