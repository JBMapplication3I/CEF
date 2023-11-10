// <copyright file="FlatInventoryProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the flat inventory provider class</summary>
namespace Clarity.Ecommerce.Providers.Inventory.Flat
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A flat inventory provider.</summary>
    /// <seealso cref="InventoryProviderBase"/>
    public class FlatInventoryProvider : InventoryProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => FlatInventoryProviderConfig.IsValid(IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ResetAllocatedInventoryForAllProductsAsync(
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var product in context.Products)
            {
                if (product.StockQuantityAllocated is null or 0m)
                {
                    continue;
                }
                product.StockQuantityAllocated = 0m;
                product.UpdatedDate = timestamp;
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
            foreach (var product in context.Products.FilterByIDs(productIDs))
            {
                if (product.StockQuantityAllocated is null or 0m)
                {
                    continue;
                }
                product.StockQuantityAllocated = 0m;
                product.UpdatedDate = timestamp;
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
            var groups = inventoryToPush.GroupBy(x => x.ProductID);
            // ReSharper disable once PossibleMultipleEnumeration
            var groupCount = groups.Count();
            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var group in groups)
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
                var inventory = group.First();
                var product = await context.Products
                    .FilterByID(productID)
                    .SingleAsync()
                    .ConfigureAwait(false);
                if (product.StockQuantity == inventory.QuantityPresent
                    && product.StockQuantityAllocated == inventory.QuantityAllocated
                    && product.StockQuantityPreSold == inventory.QuantityPreSold)
                {
                    // All data matches already
                    continue;
                }
                product.StockQuantity = inventory.QuantityPresent;
                product.StockQuantityAllocated = inventory.QuantityAllocated;
                product.StockQuantityPreSold = inventory.QuantityPreSold;
                product.UpdatedDate = timestamp;
                context.Products.AddOrUpdate(product);
                ++counter;
                if (counter % 250 != 0 && counter != groupCount)
                {
                    continue;
                }
                if (!context.Configuration.AutoDetectChangesEnabled)
                {
                    context.ChangeTracker.DetectChanges();
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
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var product = await context.Products
                .FilterByID(productID)
                .SingleAsync()
                .ConfigureAwait(false);
            if (product.StockQuantity == quantity
                && product.StockQuantityAllocated == quantityAllocated
                && product.StockQuantityPreSold == quantityPreSold)
            {
                // All data matches already
                return CEFAR.PassingCEFAR();
            }
            product.StockQuantity = quantity;
            product.StockQuantityAllocated = quantityAllocated;
            product.StockQuantityPreSold = quantityPreSold;
            product.UpdatedDate = DateExtensions.GenDateTime;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
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
        public override async Task<CEFActionResponse<ICalculatedInventory[]?>> CalculateInventoryForMultipleUOMsAsync(
            int productID,
            string productKey,
            string? accountKey,
            string? contextProfileName)
        {
            List<ICalculatedInventory>? inventory = new List<ICalculatedInventory>();
            var baseInventory = (await CalculateInventoryInnerAsync(
                       Contract.RequiresValidID(productID),
                       contextProfileName)
                   .ConfigureAwait(false))
               .Value;
            if (!Contract.CheckNotNull(baseInventory))
            {
                return CEFAR.WrapInFailingCEFAR(inventory.ToArray(), $"Could not calculate the inventory for product: {productID}");
            }
            inventory.Add(baseInventory);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var rawProductAttrs = await context.Products.FilterByCustomKey(productKey, true).Select(sa => sa.JsonAttributes).SingleOrDefaultAsync();
            var productAttrs = rawProductAttrs.DeserializeAttributesDictionary();
            List<string>? productUOMs = new List<string>();
            if (productAttrs.TryGetValue("AvailableUOMs", out var uoms))
            {
                productUOMs = uoms.Value.Split(',').ToList();
            }
            foreach (var unit in productUOMs)
            {
                if (productAttrs.TryGetValue(unit, out var conversion))
                {
                    inventory.Add(new CalculatedInventory
                    {
                        QuantityOnHand = baseInventory.QuantityOnHand > 0 && (baseInventory.QuantityOnHand / decimal.Parse(conversion.Value) >= 1)
                            ? baseInventory.QuantityOnHand / decimal.Parse(conversion.Value)
                            : 0,
                        ProductUOM = unit,
                        ProductID = productID,
                    });
                }
            }
            return CEFAR.WrapInPassingCEFAR(inventory.ToArray());
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

        /// <summary>Calculates the inventory inner.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{KeyValuePair{int,ICalculatedInventory}}.</returns>
        private static async Task<KeyValuePair<int, ICalculatedInventory>> CalculateInventoryInnerAsync(
            int productID,
            string? contextProfileName)
        {
            var calculated = RegistryLoaderWrapper.GetInstance<ICalculatedInventory>(contextProfileName);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var product = await context.Products
                .AsNoTracking()
                .FilterByID(productID)
                .Select(x => new
                {
                    x.ID,
                    x.IsDiscontinued,
                    x.IsUnlimitedStock,
                    x.StockQuantity,
                    x.StockQuantityAllocated,
                    x.AllowBackOrder,
                    x.MaximumBackOrderPurchaseQuantity,
                    x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    x.MaximumBackOrderPurchaseQuantityGlobal,
                    x.AllowPreSale,
                    x.PreSellEndDate,
                    x.MaximumPrePurchaseQuantity,
                    x.MaximumPrePurchaseQuantityIfPastPurchased,
                    x.MaximumPrePurchaseQuantityGlobal,
                    x.StockQuantityPreSold,
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
                // TODO@JTG: Read User/Account's purchase history and record values against the limits
                calculated.MaximumPrePurchaseQuantity = product.MaximumPrePurchaseQuantity;
                calculated.MaximumPrePurchaseQuantityIfPastPurchased = product.MaximumPrePurchaseQuantityIfPastPurchased;
                calculated.MaximumPrePurchaseQuantityGlobal = product.MaximumPrePurchaseQuantityGlobal;
                calculated.QuantityPreSold = product.StockQuantityPreSold;
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
            // TODO@JTG: Use Settings for kits that have their own inventory vs count of components
            // TODO@JTG: Read User/Account's purchase history and record values against the limits
            calculated.QuantityPresent = product.StockQuantity;
            calculated.QuantityAllocated = product.StockQuantityAllocated;
            if (calculated.QuantityPresent != null || calculated.QuantityAllocated != null)
            {
                calculated.QuantityOnHand = Math.Max(
                    0m,
                    (product.StockQuantity ?? 0m) - (product.StockQuantityAllocated ?? 0m));
            }
            calculated.MaximumPurchaseQuantity = product.MaximumPurchaseQuantity;
            calculated.MaximumPurchaseQuantityIfPastPurchased = product.MaximumPurchaseQuantityIfPastPurchased;
            calculated.IsOutOfStock = !calculated.IsUnlimitedStock
                && calculated.QuantityOnHand is null or <= 0;
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
            // TODO@JTG: Use Settings for kits that have their own inventory vs count of components
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return new(
                Contract.RequiresValidID(productID),
                await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(productID)
                    .Select(x => (x.StockQuantity ?? 0m) - (x.StockQuantityAllocated ?? 0m))
                    .SingleAsync()
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
            // TODO@JTG: Use Settings for kits that have their own inventory vs count of components
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return new(
                Contract.RequiresValidID(productID),
                await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(productID)
                    .Select(x => (x.StockQuantity ?? 0m) - (x.StockQuantityAllocated ?? 0m) > 0m)
                    .SingleAsync()
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
