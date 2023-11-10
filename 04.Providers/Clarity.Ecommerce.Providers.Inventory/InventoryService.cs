// <copyright file="InventoryService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout service class</summary>
#pragma warning disable CA1822
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Inventory
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>A calculate inventory.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/CalculateInventory/{ID}", "GET",
            Summary = "Calculates inventory for the given product identifier. Returns a model containing relevant information about how the inventory was calculated, limits to purchasing, etc.")]
    public class CalculateInventory : ImplementsIDBase, IReturn<CEFActionResponse<CalculatedInventory>>
    {
    }

    /// <summary>A calculate inventory.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/CalculateInventoryForMultipleUOMs/{ID}", "GET",
            Summary = "Calculates inventory for the given product and existing units of measure. Returns a model containing relevant information about how the inventory was calculated, limits to purchasing, etc.")]
    public class CalculateInventoryForMultipleUOMs : ImplementsIDBase, IReturn<CEFActionResponse<CalculatedInventory[]>>
    {
    }

    /// <summary>A get available inventory count.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/CalculateInventory/{ID}", "GET",
            Summary = "Returns a total sum count of available inventory for the given product identifier.")]
    public class GetAvailableInventoryCount : ImplementsIDBase, IReturn<CEFActionResponse<decimal>>
    {
    }

    /// <summary>A check has any available inventory.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/CheckHasAnyAvailableInventory/{ID}", "GET",
            Summary = "Returns a boolean value indicating whether there is any available inventory for the given product identifier.")]
    public class CheckHasAnyAvailableInventory : ImplementsIDBase, IReturn<CEFActionResponse<bool>>
    {
    }

    /// <summary>An update inventory for product.</summary>
    /// <seealso cref="ImplementsIDOnBodyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/UpdateInventoryForProduct", "PATCH",
            Summary = "Calculates inventory for the given product identifier.")]
    public class UpdateInventoryForProduct : ImplementsIDOnBodyBase, IUpdateInventoryForProduct, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        public decimal? Quantity { get; set; }

        /// <summary>Gets or sets the quantity allocated.</summary>
        /// <value>The quantity allocated.</value>
        public decimal? QuantityAllocated { get; set; }

        /// <summary>Gets or sets the quantity pre-sold.</summary>
        /// <value>The quantity pre-sold.</value>
        public decimal? QuantityPreSold { get; set; }

        /// <summary>Gets or sets the identifier of the relevant location.</summary>
        /// <value>The identifier of the relevant location.</value>
        public int? RelevantLocationID { get; set; }

        /// <summary>Gets or sets the relevant hash.</summary>
        /// <value>The relevant hash.</value>
        public long? RelevantHash { get; set; }
    }

    /// <summary>An inventory service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public partial class InventoryService : ClarityEcommerceServiceBase
    {
        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(CalculateInventoryForMultipleUOMs request)
        {
            List<ICalculatedInventory> inventory = new();
            using var context = RegistryLoaderWrapper.GetContext(null);
            var productKey = await context.Products
                .AsNoTracking()
                .FilterByID(Contract.RequiresValidID(request.ID))
                .Select(p => p.CustomKey!)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            return await inventoryProvider.CalculateInventoryForMultipleUOMsAsync(
                        productID: request.ID,
                        productKey,
                        CurrentAccountKey,
                        contextProfileName: null)
                    .ConfigureAwait(false);
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(CalculateInventory request)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var productKey = await context.Products
                .AsNoTracking()
                .FilterByID(Contract.RequiresValidID(request.ID))
                .Select(p => p.CustomKey!)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            return (await inventoryProvider.CalculateInventoryAsync(
                        productID: request.ID,
                        productKey,
                        CurrentAccountKey,
                        contextProfileName: null)
                    .ConfigureAwait(false))
                .ChangeCEFARType<ICalculatedInventory, CalculatedInventory>();
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(GetAvailableInventoryCount request)
        {
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            return await inventoryProvider.GetAvailableInventoryCountAsync(
                    productID: request.ID,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(CheckHasAnyAvailableInventory request)
        {
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            return await inventoryProvider.CheckHasAnyAvailableInventoryAsync(
                    productID: request.ID,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Patches the given request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Patch(UpdateInventoryForProduct request)
        {
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            return await inventoryProvider.UpdateInventoryForProductAsync(
                    productID: request.ID,
                    quantity: request.Quantity,
                    quantityAllocated: request.QuantityAllocated,
                    quantityPreSold: request.QuantityPreSold,
                    relevantLocationID: request.RelevantLocationID,
                    relevantHash: request.RelevantHash,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }
    }

    /// <summary>A calculate inventory.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/BulkCalculateInventory", "POST",
            Summary = "Calculates inventory for the given product identifier. Returns a model containing relevant information about how the inventory was calculated, limits to purchasing, etc.")]
    public class BulkCalculateInventory : IReturn<CEFActionResponse<Dictionary<int, CalculatedInventory>>>
    {
        /// <summary>Gets or sets the product IDs.</summary>
        /// <value>The product IDs.</value>
        [ApiMember(Name = nameof(ProductIDs), DataType = "List<int>", ParameterType = "body", IsRequired = true)]
        public List<int> ProductIDs { get; set; } = null!;
    }

    /// <summary>A get available inventory count.</summary>
    /// <seealso cref="IReturn{CEFActionResponseOfDictionaryOfIntToDecimal}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/BulkCalculateInventoryCount", "POST",
            Summary = "Returns a total sum count of available inventory for the given product identifier.")]
    public class GetBulkAvailableInventoryCount : IReturn<CEFActionResponse<Dictionary<int, decimal>>>
    {
        /// <summary>Gets or sets the product IDs.</summary>
        /// <value>The product IDs.</value>
        [ApiMember(Name = nameof(ProductIDs), DataType = "List<int>", ParameterType = "body", IsRequired = true)]
        public List<int> ProductIDs { get; set; } = null!;
    }

    /// <summary>A bulk check has any available inventory.</summary>
    /// <seealso cref="IReturn{CEFActionResponseOfDictionaryOfIntToBool}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/BulkCheckHasAnyAvailableInventory", "POST",
            Summary = "Returns a boolean value indicating whether there is any available inventory for the given product identifier.")]
    public class BulkCheckHasAnyAvailableInventory : IReturn<CEFActionResponse<Dictionary<int, bool>>>
    {
        /// <summary>Gets or sets the product IDs.</summary>
        /// <value>The product IDs.</value>
        [ApiMember(Name = nameof(ProductIDs), DataType = "List<int>", ParameterType = "body", IsRequired = true)]
        public List<int> ProductIDs { get; set; } = null!;
    }

    /// <summary>A bulk update inventory for products.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Providers/Inventory/BulkUpdateInventoryForProducts", "PUT",
            Summary = "Takes in bulk inventory data for upserting.")]
    public class BulkUpdateInventoryForProducts : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the inventory to push.</summary>
        /// <value>The inventory to push.</value>
        public List<CalculatedInventory> InventoryToPush { get; set; } = null!;
    }

    public partial class InventoryService
    {
        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(BulkCalculateInventory request)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var productIDsAndKeys = (await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByIDs(Contract.RequiresNotEmpty(request.ProductIDs))
                    .Select(p => new { p.ID, p.CustomKey })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(p => (p.ID, p.CustomKey!))
                .ToList();
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            var result = await inventoryProvider.BulkCalculateInventoryAsync(
                    productIDsAndKeys: productIDsAndKeys,
                    accountKey: CurrentAccountKey,
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (!result.ActionSucceeded)
            {
                return result;
            }
            return result.Result!
                .ToDictionary(x => x.Key, x => (CalculatedInventory)x.Value)
                .WrapInPassingCEFAR();
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(GetBulkAvailableInventoryCount request)
        {
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            return await inventoryProvider.GetBulkAvailableInventoryCountAsync(
                    productIDs: request.ProductIDs,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(BulkCheckHasAnyAvailableInventory request)
        {
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            return await inventoryProvider.BulkCheckHasAnyAvailableInventoryAsync(
                    productIDs: request.ProductIDs,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Puts the given request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Put(BulkUpdateInventoryForProducts request)
        {
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName: null);
            if (inventoryProvider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No inventory provider detected.");
            }
            return await inventoryProvider.BulkUpdateInventoryForProductsAsync(
                    Contract.RequiresNotEmpty(request.InventoryToPush).ToList<ICalculatedInventory>(),
                    contextProfileName: null)
                .ConfigureAwait(false);
        }
    }

    /// <summary>An inventory feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class InventoryFeature : IPlugin
    {
        /// <summary>Registers this InventoryFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
