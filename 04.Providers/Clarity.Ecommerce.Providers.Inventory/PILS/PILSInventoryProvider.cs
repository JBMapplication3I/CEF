// <copyright file="PILSInventoryProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PILS inventory provider class</summary>
namespace Clarity.Ecommerce.Providers.Inventory.PILS
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>The PILS inventory provider.</summary>
    /// <seealso cref="InventoryProviderBase"/>
    public class PILSInventoryProvider : InventoryProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PILSInventoryProviderConfig.IsValid(IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ResetAllocatedInventoryForAllProductsAsync(
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var pils in context.ProductInventoryLocationSections.FilterByActive(true))
            {
                if (pils.QuantityAllocated is null or 0m)
                {
                    continue;
                }
                pils.QuantityAllocated = 0m;
                pils.UpdatedDate = timestamp;
            }
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ResetAllocatedInventoryForProductIDsAsync(
            List<int> productIDs,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var pils in context.ProductInventoryLocationSections
                .FilterByActive(true)
                .FilterIAmARelationshipTableByMasterIDs<ProductInventoryLocationSection, Product, InventoryLocationSection>(
                    productIDs))
            {
                if (pils.QuantityAllocated is null or 0m)
                {
                    continue;
                }
                pils.QuantityAllocated = 0m;
                pils.UpdatedDate = timestamp;
            }
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> BulkUpdateInventoryForProductsAsync(
            List<ICalculatedInventory> inventoryToPush,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            IClarityEcommerceEntities? context = null;
            var counter = 0;
            var toAdd = new List<ProductInventoryLocationSection>();
            foreach (var group in inventoryToPush.GroupBy(x => x.ProductID))
            {
                if (context == null)
                {
                    context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                }
                var productID = group.Key;
                if (Contract.CheckInvalidID(productID))
                {
                    // TODO: Add message stating why skipped
                    continue;
                }
                if (!Contract.CheckValidID(
                    await Workflows.Products.CheckExistsAsync(productID, context).ConfigureAwait(false)))
                {
                    // TODO: Add message stating why skipped
                    continue;
                }
                foreach (var pilsGroup in group.SelectMany(x => x.RelevantLocations!).GroupBy(x => x.SlaveID))
                {
                    var ilsID = pilsGroup.Key;
                    if (Contract.CheckInvalidID(ilsID))
                    {
                        // TODO: Add message stating why skipped
                        continue;
                    }
                    if (!Contract.CheckValidID(
                        await Workflows.InventoryLocationSections.CheckExistsAsync(ilsID, context!).ConfigureAwait(false)))
                    {
                        // TODO: Add message stating why skipped
                        continue;
                    }
                    var inventory = pilsGroup.First();
                    var pilsSingle = await context!.ProductInventoryLocationSections
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableByMasterID<ProductInventoryLocationSection, Product, InventoryLocationSection>(productID)
                        .FilterIAmARelationshipTableBySlaveID<ProductInventoryLocationSection, Product, InventoryLocationSection>(ilsID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                    if (pilsSingle == null)
                    {
                        toAdd.Add(new()
                        {
                            // Base Properties
                            Active = true,
                            CustomKey = inventory.CustomKey,
                            CreatedDate = DateExtensions.GenDateTime,
                            // Hash = relevantHash,
                            // Related Objects
                            MasterID = productID,
                            SlaveID = ilsID,
                            // Quantities
                            Quantity = inventory.Quantity,
                            QuantityAllocated = inventory.QuantityAllocated,
                            QuantityPreSold = inventory.QuantityPreSold,
                        });
                        continue;
                    }
                    if (pilsSingle.Quantity == inventory.Quantity
                        && pilsSingle.QuantityAllocated == inventory.QuantityAllocated
                        && pilsSingle.QuantityPreSold == inventory.QuantityPreSold
                        && pilsSingle.CustomKey == inventory.CustomKey
                        && pilsSingle.Hash == inventory.Hash)
                    {
                        // All data matches already
                        continue;
                    }
                    pilsSingle.CustomKey = inventory.CustomKey;
                    pilsSingle.Quantity = inventory.Quantity;
                    pilsSingle.QuantityAllocated = inventory.QuantityAllocated;
                    pilsSingle.QuantityPreSold = inventory.QuantityPreSold;
                    pilsSingle.Hash = inventory.Hash;
                    pilsSingle.UpdatedDate = timestamp;
                    if (++counter % 250 != 0)
                    {
                        continue;
                    }
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                    context.Dispose();
                    context = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            if (context != null)
            {
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                context.Dispose();
                context = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            counter = 0;
            foreach (var pils in toAdd)
            {
                if (context == null)
                {
                    context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                }
                context.ProductInventoryLocationSections.Add(pils);
                if (++counter % 250 != 0)
                {
                    continue;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                context.Dispose();
                context = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            if (context == null)
            {
                return CEFAR.PassingCEFAR();
            }
            context.Dispose();
            context = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> UpdateInventoryForProductAsync(
            int productID,
            decimal? quantity,
            decimal? quantityAllocated,
            decimal? quantityPreSold,
            int? relevantLocationID,
            long? relevantHash,
            string? contextProfileName)
        {
            Contract.RequiresValidID(productID);
            Contract.RequiresValidID(relevantLocationID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var pilsSingle = await context.ProductInventoryLocationSections
                .FilterByActive(true)
                .FilterIAmARelationshipTableByMasterID<ProductInventoryLocationSection, Product, InventoryLocationSection>(productID)
                .FilterIAmARelationshipTableBySlaveID<ProductInventoryLocationSection, Product, InventoryLocationSection>(relevantLocationID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (pilsSingle == null)
            {
                context.ProductInventoryLocationSections.Add(new()
                {
                    // Base Properties
                    Active = true,
                    CreatedDate = DateExtensions.GenDateTime,
                    Hash = relevantHash,
                    // Related Objects
                    MasterID = productID,
                    SlaveID = relevantLocationID!.Value,
                    // Quantities
                    Quantity = quantity,
                    QuantityAllocated = quantityAllocated,
                    QuantityPreSold = quantityPreSold,
                });
                return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                    .BoolToCEFAR($"ERROR! Something about updating the inventory for product {productID} failed");
            }
            if (pilsSingle.Quantity == quantity
                && pilsSingle.QuantityAllocated == quantityAllocated
                && pilsSingle.QuantityPreSold == quantityPreSold
                && pilsSingle.Hash == relevantHash)
            {
                // All data matches already
                return CEFAR.PassingCEFAR();
            }
            pilsSingle.Quantity = quantity;
            pilsSingle.QuantityAllocated = quantityAllocated;
            pilsSingle.QuantityPreSold = quantityPreSold;
            pilsSingle.Hash = relevantHash;
            pilsSingle.UpdatedDate = DateExtensions.GenDateTime;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR($"ERROR! Something about updating the inventory for product {productID} failed");
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<ICalculatedInventory>> CalculateInventoryAsync(
            int productID,
            string productKey,
            string? accountKey,
            string? contextProfileName)
        {
            return (await CalculateInventoryInnerAsync(
                        Contract.RequiresValidID(productID),
                        contextProfileName)
                    .ConfigureAwait(false))
                .Value
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<Dictionary<int, ICalculatedInventory>>> BulkCalculateInventoryAsync(
            List<(int productID, string productKey)>? productIDsAndKeys,
            string? accountKey,
            string? contextProfileName)
        {
            return (await Task.WhenAll(
                    Contract.RequiresNotEmpty(productIDsAndKeys)
                        .Distinct()
                        .Where(x => Contract.CheckValidID(x.productID))
                        .Select(x => CalculateInventoryInnerAsync(x.productID, contextProfileName)))
                    .ConfigureAwait(false))
                .ToDictionary(x => x.Key, x => x.Value)
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<Dictionary<int, decimal>>> GetBulkAvailableInventoryCountAsync(
            List<int> productIDs,
            string? contextProfileName)
        {
            return (await Task.WhenAll(
                    Contract.RequiresNotEmpty(productIDs)
                        .Distinct()
                        .Where(x => Contract.CheckValidID(x))
                        .Select(x => GetAvailableInventoryCountInnerAsync(x, contextProfileName)))
                    .ConfigureAwait(false))
                .ToDictionary(x => x.Key, x => x.Value)
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<decimal>> GetAvailableInventoryCountAsync(
            int productID,
            string? contextProfileName)
        {
            return (await GetAvailableInventoryCountInnerAsync(
                        Contract.RequiresValidID(productID),
                        contextProfileName)
                    .ConfigureAwait(false))
                .Value
                .WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<Dictionary<int, bool>>> BulkCheckHasAnyAvailableInventoryAsync(
            List<int> productIDs,
            string? contextProfileName)
        {
            return (await Task.WhenAll(
                    Contract.RequiresNotEmpty(productIDs)
                        .Distinct()
                        .Where(x => Contract.CheckValidID(x))
                        .Select(x => CheckHasAnyAvailableInventoryInnerAsync(x, contextProfileName)))
                    .ConfigureAwait(false))
                .ToDictionary(x => x.Key, x => x.Value)
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<bool>> CheckHasAnyAvailableInventoryAsync(
            int productID,
            string? contextProfileName)
        {
            return (await CheckHasAnyAvailableInventoryInnerAsync(
                        Contract.RequiresValidID(productID),
                        contextProfileName)
                    .ConfigureAwait(false))
                .Value
                .WrapInPassingCEFAR();
        }

        public override Task<CEFActionResponse<ICalculatedInventory[]?>> CalculateInventoryForMultipleUOMsAsync(int productID, string productKey, string? accountKey, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Calculates the inventory inner.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{KeyValuePair{int,ICalculatedInventory}}.</returns>
        private static async Task<KeyValuePair<int, ICalculatedInventory>> CalculateInventoryInnerAsync(
            int productID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(productID);
            var calculated = RegistryLoaderWrapper.GetInstance<ICalculatedInventory>(contextProfileName);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // TODO@JTG: Should this product call be cached in PILS as it's the non-changing data?
            var product = await context.Products
                .AsNoTracking()
                .FilterByID(productID)
                .Select(x => new
                {
                    x.ID,
                    x.IsDiscontinued,
                    x.IsUnlimitedStock,
                    x.AllowBackOrder,
                    x.MaximumBackOrderPurchaseQuantity,
                    x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    x.MaximumBackOrderPurchaseQuantityGlobal,
                    x.AllowPreSale,
                    x.PreSellEndDate,
                    x.MaximumPrePurchaseQuantity,
                    x.MaximumPrePurchaseQuantityIfPastPurchased,
                    x.MaximumPrePurchaseQuantityGlobal,
                    x.MaximumPurchaseQuantity,
                    x.MaximumPurchaseQuantityIfPastPurchased,
                })
                .SingleAsync()
                .ConfigureAwait(false);
            calculated.ProductID = product.ID;
            calculated.IsDiscontinued = product.IsDiscontinued;
            if (calculated.IsDiscontinued)
            {
                // Don't set anything else, all other data is irrelevant when discontinued
                calculated.IsOutOfStock = true;
                return new(productID, calculated);
            }
            calculated.IsUnlimitedStock = product.IsUnlimitedStock;
            var allowPreSale = product.AllowPreSale;
            if (allowPreSale && !CEFConfigDictionary.InventoryPreSaleEnabled
                || calculated.IsUnlimitedStock)
            {
                allowPreSale = false;
            }
            if (allowPreSale)
            {
                // Only pre-sales values are relevant
                calculated.AllowPreSale = true;
                calculated.PreSellEndDate = product.PreSellEndDate;
                // TODO@JTG: Use Settings for kits that have their own inventory vs count of components
                // TODO@JTG: Read User/Account's purchase history and record values against the limits;
                calculated.MaximumPrePurchaseQuantity = product.MaximumPrePurchaseQuantity;
                calculated.MaximumPrePurchaseQuantityIfPastPurchased = product.MaximumPrePurchaseQuantityIfPastPurchased;
                calculated.MaximumPrePurchaseQuantityGlobal = product.MaximumPrePurchaseQuantityGlobal;
                var pilsPreSales = await context.ProductInventoryLocationSections
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableByMasterID<ProductInventoryLocationSection, Product, InventoryLocationSection>(productID)
                    .Where(x => x.Slave!.Active && x.Slave.InventoryLocation!.Active)
                    .Select(x => new
                    {
                        // Base Properties
                        x.ID,
                        x.CustomKey,
                        x.CreatedDate,
                        x.UpdatedDate,
                        // Related Objects
                        x.SlaveID, // ILS ID
                        x.Slave!.InventoryLocationID,
                        // Quantities
                        x.Quantity,
                        x.QuantityAllocated,
                        x.QuantityPreSold,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false);
                if (pilsPreSales.Any())
                {
                    calculated.QuantityPreSold = pilsPreSales.Sum(x => x.QuantityPreSold);
                    calculated.RelevantLocations = pilsPreSales
                        .Select(x => new ProductInventoryLocationSectionModel
                        {
                            // Base Properties
                            ID = x.ID,
                            Active = true,
                            CreatedDate = x.CreatedDate,
                            UpdatedDate = x.UpdatedDate,
                            // Related Objects
                            MasterID = productID,
                            SlaveID = x.SlaveID,
                            InventoryLocationSectionInventoryLocationID = x.InventoryLocationID,
                            // Quantities
                            QuantityPreSold = x.QuantityPreSold,
                        })
                        .ToList<IProductInventoryLocationSectionModel>();
                }
                calculated.IsOutOfStock = false;
                return new(productID, calculated);
            }
            var allowBackOrder = product.AllowBackOrder;
            if (allowBackOrder && !CEFConfigDictionary.InventoryBackOrderEnabled
                || calculated.IsUnlimitedStock)
            {
                allowBackOrder = false;
            }
            if (allowBackOrder)
            {
                calculated.AllowBackOrder = true;
                // TODO@JTG: Use Settings for kits that have their own inventory vs count of components
                // TODO@JTG: Read User/Account's purchase history and record values against the limits
                calculated.MaximumBackOrderPurchaseQuantity = product.MaximumBackOrderPurchaseQuantity;
                calculated.MaximumBackOrderPurchaseQuantityIfPastPurchased = product.MaximumBackOrderPurchaseQuantityIfPastPurchased;
                calculated.MaximumBackOrderPurchaseQuantityGlobal = product.MaximumBackOrderPurchaseQuantityGlobal;
            }
            var pils = await context.ProductInventoryLocationSections
                .AsNoTracking()
                .FilterByActive(true)
                .FilterIAmARelationshipTableByMasterID<ProductInventoryLocationSection, Product, InventoryLocationSection>(productID)
                .Where(x => x.Slave!.Active && x.Slave.InventoryLocation!.Active)
                .Select(x => new
                {
                    // Base Properties
                    x.ID,
                    x.CustomKey,
                    x.CreatedDate,
                    x.UpdatedDate,
                    // Related Objects
                    x.SlaveID, // ILS ID
                    x.Slave!.InventoryLocationID,
                    // Quantities
                    x.Quantity,
                    x.QuantityAllocated,
                })
                .ToListAsync()
                .ConfigureAwait(false);
            if (pils.Any())
            {
                // TODO@JTG: Use Settings for kits that have their own inventory vs count of components
                // TODO@JTG: Read User/Account's purchase history and record values against the limits
                calculated.QuantityPresent = pils.Sum(x => x.Quantity);
                calculated.QuantityAllocated = pils.Sum(x => x.QuantityAllocated);
                calculated.QuantityOnHand = Math.Max(
                    0m,
                    calculated.QuantityPresent!.Value - calculated.QuantityAllocated!.Value);
                calculated.RelevantLocations = pils
                    .Select(x => new ProductInventoryLocationSectionModel
                    {
                        // Base Properties
                        ID = x.ID,
                        Active = true,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        // Related Objects
                        MasterID = productID,
                        SlaveID = x.SlaveID,
                        InventoryLocationSectionInventoryLocationID = x.InventoryLocationID,
                        // Quantities
                        Quantity = x.Quantity,
                        QuantityAllocated = x.QuantityAllocated,
                    })
                    .ToList<IProductInventoryLocationSectionModel>();
            }
            calculated.MaximumPurchaseQuantity = product.MaximumPurchaseQuantity;
            calculated.MaximumPurchaseQuantityIfPastPurchased = product.MaximumPurchaseQuantityIfPastPurchased;
            calculated.IsOutOfStock = !calculated.IsUnlimitedStock
                && calculated.QuantityOnHand is null or <= 0;
            // TODO: Read User/Account's purchase history and record values against the limits;
            return new(productID, calculated);
        }

        /// <summary>Gets available inventory count inner.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{KeyValuePair{int,decimal}}.</returns>
        private static async Task<KeyValuePair<int, decimal>> GetAvailableInventoryCountInnerAsync(
            int productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return new(
                Contract.RequiresValidID(productID),
                await context.ProductInventoryLocationSections
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableByMasterID<ProductInventoryLocationSection, Product, InventoryLocationSection>(productID)
                    .Where(x => x.Quantity.HasValue && x.Quantity - (x.QuantityAllocated ?? 0m) > 0m)
                    .SumAsync(x => x.Quantity!.Value - (x.QuantityAllocated ?? 0m))
                    .ConfigureAwait(false));
        }

        /// <summary>Check has any available inventory inner.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{KeyValuePair{int,bool}}.</returns>
        private static async Task<KeyValuePair<int, bool>> CheckHasAnyAvailableInventoryInnerAsync(
            int productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return new(
                Contract.RequiresValidID(productID),
                await context.ProductInventoryLocationSections
                    .AsNoTracking()
                    .FilterIAmARelationshipTableByMasterID<ProductInventoryLocationSection, Product, InventoryLocationSection>(
                        productID)
                    .AnyAsync(x => (x.Quantity ?? 0m) - (x.QuantityAllocated ?? 0m) > 0m)
                    .ConfigureAwait(false));
        }
    }
#if FALSE
// TODO@JTG: This is the old mapping code for kit/variant kits to get inventory, use as a reference when updating this provider
// with Kit settings and their inventory management
StockQuantity = x.IsUnlimitedStock || x.Type?.Name == "Variant Master"
    ? null
    : x.Type?.Name == "Kit" || x.Type?.Name == "Variant Kit"
        ? ProductSQLExtensions.GetKitCompositionStockQuantity<ProductAssociation>().Compile()(x.ProductAssociations.AsQueryable())
        : x.StockQuantity,
StockQuantityAllocated = x.IsUnlimitedStock || x.Type?.Name == "Variant Master"
    ? null
    : x.Type?.Name == "Kit" || x.Type?.Name == "Variant Kit"
        ? ProductSQLExtensions.GetKitCompositionStockQuantityAllocated<ProductAssociation>().Compile()(x.ProductAssociations.AsQueryable())
        : x.StockQuantityAllocated,
StockQuantityPreSold = x.IsUnlimitedStock || x.Type?.Name == "Variant Master"
    ? null
    : x.Type?.Name == "Kit" || x.Type?.Name == "Variant Kit"
        ? ProductSQLExtensions.GetKitCompositionStockQuantityPreSold<ProductAssociation>().Compile()(x.ProductAssociations.AsQueryable())
        : x.StockQuantityPreSold,
StockQuantityBroken = x.IsUnlimitedStock || x.Type?.Name == "Variant Master"
    ? null
    : x.Type?.Name == "Kit" || x.Type?.Name == "Variant Kit"
        ? ProductSQLExtensions.GetKitCompositionStockQuantityBroken<ProductAssociation>().Compile()(x.ProductAssociations.AsQueryable())
        : null,

newModel.StockQuantity = x.StockQuantity; // Pre-calculated
newModel.StockQuantityAllocated = x.StockQuantityAllocated; // Pre-calculated
newModel.StockQuantityPreSold = x.StockQuantityPreSold; // Pre-calculated
newModel.StockQuantityBroken = x.IsUnlimitedStock || x.Type != null && x.Type.Name == "Variant Master"
    ? null
    : x.Type != null && (x.Type.Name == "Kit" || x.Type.Name == "Variant Kit")
        ? GetKitCompositionStockQuantityBroken.Invoke(x.ProductAssociations.AsQueryable())
        : null;

item.StockQuantity = x.IsUnlimitedStock || x.Type?.Name == "Variant Master"
    ? null
    : x.Type?.Name == "Kit" || x.Type?.Name == "Variant Kit"
        ? ProductSQLExtensions.GetKitCompositionStockQuantity<ProductAssociation>().Compile().Invoke(x.ProductAssociations.AsQueryable())
        : x.StockQuantity;
item.StockQuantityAllocated = x.IsUnlimitedStock || x.Type?.Name == "Variant Master"
    ? null
    : x.Type?.Name == "Kit" || x.Type?.Name == "Variant Kit"
        ? ProductSQLExtensions.GetKitCompositionStockQuantityAllocated<ProductAssociation>().Compile().Invoke(x.ProductAssociations.AsQueryable())
        : x.StockQuantityAllocated;
item.StockQuantityPreSold = x.IsUnlimitedStock || x.Type?.Name == "Variant Master"
    ? null
    : x.Type?.Name == "Kit" || x.Type?.Name == "Variant Kit"
        ? ProductSQLExtensions.GetKitCompositionStockQuantityPreSold<ProductAssociation>().Compile().Invoke(x.ProductAssociations.AsQueryable())
        : x.StockQuantityPreSold;
item.StockQuantityBroken = x.IsUnlimitedStock || x.Type?.Name == "Variant Master"
    ? null
    : x.Type?.Name == "Kit" || x.Type?.Name == "Variant Kit"
        ? ProductSQLExtensions.GetKitCompositionStockQuantityBroken<ProductAssociation>().Compile().Invoke(x.ProductAssociations.AsQueryable())
        : null;
#endif
}
