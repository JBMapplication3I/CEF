// <copyright file="ProductInventoryLocationService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product inventory location service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [Obsolete("Deprecated in 2020.3, use PILSInventoryProvider instead. Will be removed in 2020.1")]
    [PublicAPI,
     Route("/Inventory/ProductInventoryLocationSections/ResetProductInventoryAllocated", "POST",
         Summary = "Reset Allocated Inventory to 0")]
    public class ResetProductInventoryAllocated : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(StoreKey), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string StoreKey { get; set; } = null!;
    }

    [Obsolete("Deprecated in 2020.3, use PILSInventoryProvider instead. Will be removed in 2020.1")]
    [PublicAPI,
     Route("/Inventory/ProductInventoryLocationSections/CreateMany", "POST",
         Summary = "GET InventoryLocations By ProductID")]
    public class CreateProductInventoryLocationSections : List<ProductInventoryLocationSectionModel>, IReturn<CEFActionResponse>
    {
    }

    [Obsolete("Deprecated in 2020.3, use PILSInventoryProvider instead. Will be removed in 2020.1")]
    [PublicAPI,
     Route("/Inventory/ProductInventoryLocationSections/UpdateMany", "POST",
         Summary = "GET InventoryLocations By ProductID")]
    public class UpdateProductInventoryLocationSections : List<ProductInventoryLocationSectionModel>, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI]
    public partial class ProductInventoryLocationSectionService
    {
        [Obsolete("Deprecated in 2020.3, use PILSInventoryProvider instead. Will be removed in 2020.1")]
        public async Task<object?> Post(CreateProductInventoryLocationSections request)
        {
            try
            {
                return (await Workflows.ProductInventoryLocationSections.CreateManyAsync(
                            request.ToList<IProductInventoryLocationSectionModel>(),
                            null)
                        .ConfigureAwait(false))
                    .BoolToCEFAR();
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        [Obsolete("Deprecated in 2020.3, use PILSInventoryProvider instead. Will be removed in 2020.1")]
        public async Task<object?> Post(UpdateProductInventoryLocationSections request)
        {
            try
            {
                return (await Workflows.ProductInventoryLocationSections.UpdateManyAsync(
                            request.ToList<IProductInventoryLocationSectionModel>(),
                            null)
                        .ConfigureAwait(false))
                    .BoolToCEFAR();
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        [Obsolete("Deprecated in 2020.3, use PILSInventoryProvider instead. Will be removed in 2020.1")]
        public async Task<object?> Post(ResetProductInventoryAllocated request)
        {
            return await Workflows.ProductInventoryLocationSections.ResetInventoryByStoreAsync(
                    request.StoreKey,
                    null)
                .ConfigureAwait(false);
        }
    }
}
