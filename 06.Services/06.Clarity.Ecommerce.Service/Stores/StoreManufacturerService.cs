// <copyright file="StoreManufacturerService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store manufacturer service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, Authenticate]
    [Route("/Stores/StoreManufacturers/ByManufacturerID", "POST", Summary = "Get stores by ManufacturerID")]
    public class GetStoreManufacturersByManufacturerID : StoreManufacturerSearchModel, IReturn<StoreManufacturerPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetStoreManufacturersByManufacturerID request)
        {
            return await GetPagedResultsAsync<IStoreManufacturerModel, StoreManufacturerModel, IStoreManufacturerSearchModel, StoreManufacturerPagedResults>(
                request, false, Workflows.StoreManufacturers)
                .ConfigureAwait(false);
        }
    }
}
