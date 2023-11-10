// <copyright file="BrandProductService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand product service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using ServiceStack;

    [Authenticate]
    [Route("/Brands/BrandProducts/ByProductID", "POST")]
    public class GetBrandProductsByProductID : BrandProductSearchModel, IReturn<BrandProductPagedResults>
    {
    }

    [Authenticate]
    [Route("/Brands/BrandProducts/CreateMany", "POST")]
    public class CreateBrandProducts : List<BrandProductModel>, IReturn<CEFActionResponse>
    {
    }

    [Authenticate]
    [Route("/Brands/BrandProducts/UpdateMany", "POST")]
    public class UpdateBrandProducts : List<BrandProductModel>, IReturn<CEFActionResponse>
    {
    }

    [Route("/Brands/BrandProducts/GetHash", "GET")]
    public class GetBrandProductDigest : IReturn<List<DigestModel>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetBrandProductsByProductID request)
        {
            return await GetPagedResultsAsync<IBrandProductModel, BrandProductModel, IBrandProductSearchModel, BrandProductPagedResults>(
                    request,
                    false,
                    Workflows.BrandProducts)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(CreateBrandProducts request)
        {
            try
            {
                return await Workflows.BrandProducts.CreateManyAsync(request.ToList<IBrandProductModel>(), null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        public async Task<object?> Post(UpdateBrandProducts request)
        {
            try
            {
                return await Workflows.BrandProducts.UpdateManyAsync(request.ToList<IBrandProductModel>(), null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        public async Task<object?> Get(GetBrandProductDigest _)
        {
            return await Workflows.BrandProducts.GetDigestAsync(null).ConfigureAwait(false);
        }
    }
}
