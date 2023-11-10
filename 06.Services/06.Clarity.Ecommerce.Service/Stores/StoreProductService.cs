// <copyright file="StoreProductService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store product service class</summary>
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
    [Route("/Stores/StoreProducts/ByProductID", "POST")]
    public class GetStoreProductsByProductID : StoreProductSearchModel, IReturn<StoreProductPagedResults>
    {
    }

    [Authenticate]
    [Route("/Stores/StoreProducts/CreateMany", "POST")]
    public class CreateStoreProducts : List<StoreProductModel>, IReturn<CEFActionResponse>
    {
    }

    [Authenticate]
    [Route("/Stores/StoreProducts/UpdateMany", "POST")]
    public class UpdateStoreProducts : List<StoreProductModel>, IReturn<CEFActionResponse>
    {
    }

    [Route("/Stores/StoreProducts/GetHash", "GET")]
    public class GetStoreProductDigest : IReturn<List<DigestModel>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetStoreProductsByProductID request)
        {
            return await GetPagedResultsAsync<IStoreProductModel, StoreProductModel, IStoreProductSearchModel, StoreProductPagedResults>(
                    request,
                    false,
                    Workflows.StoreProducts)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(CreateStoreProducts request)
        {
            try
            {
                return await Workflows.StoreProducts.CreateManyAsync(request.ToList<IStoreProductModel>(), null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        public async Task<object?> Post(UpdateStoreProducts request)
        {
            try
            {
                return await Workflows.StoreProducts.UpdateManyAsync(request.ToList<IStoreProductModel>(), null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        public async Task<object?> Get(GetStoreProductDigest _)
        {
            return await Workflows.StoreProducts.GetDigestAsync(null).ConfigureAwait(false);
        }
    }
}
