// <copyright file="InventoryLocationRegionService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the InventoryLocation Region service class</summary>
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
        Route("/Inventory/InventoryLocation/Regions/ByInventoryLocationID", "POST",
            Summary = "Get InventoryLocationRegions by InventoryLocation ID")]
    public partial class GetInventoryLocationRegionsByInventoryLocationID : InventoryLocationRegionSearchModel, IReturn<InventoryLocationRegionPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetInventoryLocationRegionsByInventoryLocationID request)
        {
            return await GetPagedResultsAsync<IInventoryLocationRegionModel, InventoryLocationRegionModel, IInventoryLocationRegionSearchModel, InventoryLocationRegionPagedResults>(
                    request,
                    false,
                    Workflows.InventoryLocationRegions)
                .ConfigureAwait(false);
        }
    }
}
