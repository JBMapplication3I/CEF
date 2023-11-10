// <copyright file="StoreRegionService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store region service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate,
        Route("/Stores/StoreRegions/ByStoreID", "POST",
            Summary = "Get store regions by store ID")]
    public partial class GetStoreRegionsByStoreID : StoreRegionSearchModel, IReturn<StoreRegionPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetStoreRegionsByStoreID request)
        {
            return await GetPagedResultsAsync<IStoreRegionModel, StoreRegionModel, IStoreRegionSearchModel, StoreRegionPagedResults>(
                    request,
                    false,
                    Workflows.StoreRegions)
                .ConfigureAwait(false);
        }
    }
}
