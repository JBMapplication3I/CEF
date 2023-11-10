// <copyright file="ProductInventoryLocationSectionCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product inventory location section workflow class</summary>
namespace Clarity.Ecommerce.Workflow
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
    using Mapper;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    public partial class ProductInventoryLocationSectionWorkflow
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<IProductInventoryLocationSectionModel>> SearchForCatalogAsync(
            IProductInventoryLocationSectionSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.ProductInventoryLocationSections
                    .AsNoTracking()
                    .FilterByActive(search.Active)
                    .FilterPILSByProductIDs(search.ProductIDs)
                    .Select(x => new
                    {
                        // Base Properties
                        x.ID,
                        x.CustomKey,
                        // Inventory Quantities
                        x.Quantity,
                        x.QuantityAllocated,
                        x.QuantityBroken,
                        // IL
                        x.Slave!.InventoryLocationID,
                        InventoryLocationKey = x.Slave.InventoryLocation!.CustomKey,
                        // ILS
                        x.SlaveID,
                        InventoryLocationSectionKey = x.Slave.CustomKey,
                        // Product
                        x.MasterID,
                        FlatQuantity = x.Master!.StockQuantity,
                        FlatQuantityAllocated = x.Master.StockQuantityAllocated,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new ProductInventoryLocationSectionModel
                {
                    // Base Properties
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    // Inventory Quantities
                    Quantity = x.Quantity,
                    QuantityAllocated = x.QuantityAllocated,
                    QuantityBroken = x.QuantityBroken,
                    // Product Quantities
                    FlatQuantity = x.FlatQuantity,
                    FlatQuantityAllocated = x.FlatQuantityAllocated,
                    // IL
                    InventoryLocationSectionInventoryLocationID = x.InventoryLocationID,
                    InventoryLocationSectionInventoryLocationKey = x.InventoryLocationKey,
                    // ILS
                    SlaveID = x.SlaveID,
                    SlaveKey = x.InventoryLocationSectionKey,
                    // Product
                    MasterID = x.MasterID,
                });
        }

        /// <inheritdoc/>
        public async Task<bool> CheckKitComponentInventoryAsync(int? kitProductID, string? contextProfileName)
        {
            Contract.RequiresValidID(kitProductID);
            var inventory = await GetKitComponentInventoryAsync(kitProductID, contextProfileName).ConfigureAwait(false);
            return inventory.allowBackOrder || inventory.inventory > 0m;
        }

        /// <inheritdoc/>
        public async Task<(decimal? inventory, bool allowBackOrder, bool allowPreSale, DateTime? preSaleEnd)> GetKitComponentInventoryAsync(
            int? kitProductID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(kitProductID);
            (decimal? inventory, bool allowBackOrder, bool allowPreSale, DateTime? preSaleEnd) retVal = (null, false, false, null);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var product = await context.Products
                // Kit Product must not be soft-deleted
                .FilterByActive(true)
                .FilterByID(kitProductID)
                // Must be of Type 'Kit'
                .FilterByTypeKey<Product, ProductType>("KIT", true)
                // Must have Associated products of type 'Kit Component'
                .Where(x => x.ProductAssociations!
                    .Any(y => y.Active
                        && y.Slave != null
                        && y.Slave.Active
                        && y.Type != null
                        && y.Type.CustomKey == "KIT-COMPONENT"))
                .Select(x => new { x.ID, x.AllowBackOrder, x.AllowPreSale, x.PreSellEndDate })
                // If we somehow found more than one, that's a problem
                .SingleOrDefaultAsync();
            if (product == null)
            {
                // We couldn't find the product or it didn't meet the conditions for this function
                return retVal;
            }
            var kitComponents = await context.ProductAssociations
                .FilterByActive(true)
                .FilterByTypeKey<ProductAssociation, ProductAssociationType>("KIT-COMPONENT", true)
                .Where(x => x.MasterID == kitProductID!.Value
                    && x.Slave != null
                    && x.Slave.Active
                    && x.Quantity > 0m)
                .Select(x => new { ProductID = x.SlaveID, QuantityRequired = x.Quantity ?? 0m })
                .ToListAsync()
                .ConfigureAwait(false);
            var results = (await context.ProductInventoryLocationSections
                    .FilterByActive(true)
                    .FilterPILSByProductIDs(kitComponents.Select(x => x.ProductID).ToList())
                    .Select(x => new
                    {
                        x.MasterID,
                        QuantityOnHand = (x.Quantity ?? 0m) - (x.QuantityAllocated ?? 0m) - (x.QuantityBroken ?? 0m),
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .GroupBy(x => x.MasterID);
            retVal.allowBackOrder = product.AllowBackOrder;
            retVal.allowPreSale = product.AllowPreSale;
            retVal.preSaleEnd = product.PreSellEndDate;
            retVal.inventory = Math.Max(
                0m,
                kitComponents
                    .Min(x => Math.Floor(
                        (results.SingleOrDefault(y => y.Key == x.ProductID)
                            ?.AsEnumerable()
                            ?.Select(z => z.QuantityOnHand)
                            .DefaultIfEmpty(0)
                            .Sum()
                            ?? int.MaxValue)
                        / x.QuantityRequired)));
            return retVal;
        }

        /// <inheritdoc/>
        public async Task<bool> CheckProductInventoryAsync(int? productID, string? contextProfileName)
        {
            Contract.RequiresValidID(productID);
            var (inventory, allowBackOrder, allowPreSale, preSaleEnd) = await GetProductInventoryAsync(productID, contextProfileName).ConfigureAwait(false);
            return allowBackOrder || allowPreSale && (!preSaleEnd.HasValue || preSaleEnd.Value > DateTime.Now) || inventory > 0m;
        }

        /// <inheritdoc/>
        public async Task<(decimal? inventory, bool allowBackOrder, bool allowPreSale, DateTime? preSaleEnd)> GetProductInventoryAsync(
            int? productID, string? contextProfileName)
        {
            Contract.RequiresValidID(productID);
            (decimal? inventory, bool allowBackOrder, bool allowPreSale, DateTime? preSaleEnd) retVal = (null, false, false, null);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var product = await context.Products
                .AsNoTracking()
                .FilterByActive(true) // Kit Product must not be soft-deleted
                .FilterByID(productID)
                .Select(x => new { x.AllowBackOrder, x.AllowPreSale, x.PreSellEndDate, x.StockQuantity, x.StockQuantityAllocated, x.StockQuantityPreSold })
                .SingleOrDefaultAsync() // If we somehow found more than one, that's a problem
                .ConfigureAwait(false);
            if (product == null)
            {
                // We couldn't find the product or it didn't meet the conditions for this function
                return retVal;
            }
            var results = await context.ProductInventoryLocationSections
                .FilterByActive(true)
                .FilterPILSByProductID(productID)
                .Select(x => (x.Quantity ?? 0m) - (x.QuantityAllocated ?? 0m) - (x.QuantityBroken ?? 0m))
                .ToListAsync()
                .ConfigureAwait(false);
            retVal.allowBackOrder = product.AllowBackOrder;
            retVal.allowPreSale = product.AllowPreSale;
            retVal.preSaleEnd = product.PreSellEndDate;
            retVal.inventory = results.Count == 0 && product.StockQuantity.HasValue
                ? product.StockQuantity - (product.StockQuantityAllocated ?? 0m)
                : results.DefaultIfEmpty(0m).Sum();
            return retVal;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateManyAsync(
            List<IProductInventoryLocationSectionModel> models, string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            foreach (var model in models)
            {
                // if (!OverrideDuplicateCheck) { DuplicateCheck(model); } // Don't need to do this here
                int? productID = model.MasterID;
                if (Contract.CheckInvalidID(productID))
                {
                    productID = await Workflows.Products.CheckExistsAsync(model.MasterKey!, contextProfileName).ConfigureAwait(false);
                    if (Contract.CheckInvalidID(productID))
                    {
                        continue;
                    }
                }
                var entity = new ProductInventoryLocationSection
                {
                    Active = true,
                    CreatedDate = timestamp,
                    CustomKey = model.CustomKey,
                    MasterID = productID!.Value,
                    Quantity = model.Quantity,
                    QuantityAllocated = model.QuantityAllocated,
                    QuantityBroken = model.QuantityBroken,
                    SlaveID = model.SlaveID,
                    Hash = model.Hash,
                };
                context.ProductInventoryLocationSections.Add(entity);
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return true;
        }

        /// <inheritdoc/>
        public virtual async Task<bool> UpdateManyAsync(List<IProductInventoryLocationSectionModel> models, string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.Configuration.ValidateOnSaveEnabled = false;
            foreach (var model in models)
            {
                var entity = await context.ProductInventoryLocationSections.FilterByID(model.ID).SingleOrDefaultAsync()
                    ?? (!string.IsNullOrWhiteSpace(model.CustomKey)
                        ? await context.ProductInventoryLocationSections
                            .FilterByCustomKey(model.CustomKey, true)
                            .OrderByDescending(x => x.Active)
                            .SingleAsync()
                        : throw new ArgumentException("Must supply an ID or CustomKey that matches an existing record"));
                ////if (entity.CustomKey != model.CustomKey)
                ////{
                ////    DuplicateCheck(model); // This will throw if it finds another entity with this model's key
                ////}
                entity.Active = model.Active;
                entity.UpdatedDate = timestamp;
                entity.CustomKey = model.CustomKey;
                entity.Quantity = model.Quantity;
                entity.QuantityAllocated = model.QuantityAllocated;
                entity.QuantityBroken = model.QuantityBroken;
                entity.Hash = model.Hash;
                entity.MasterID = model.MasterID;
                entity.SlaveID = model.SlaveID;
            }
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return true;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ResetInventoryByStoreAsync(
            string storeKey, string? contextProfileName)
        {
            var response = new CEFActionResponse();
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var pils = context.ProductInventoryLocationSections
                .AsNoTracking()
                .FilterByActive(true)
                .FilterPILSByILSInventoryLocationKey(storeKey);
            ////var store = context.StoreInventoryLocations.FilterByStoreKey(storeKey).FirstOrDefault();
            ////var loc = store?.InventoryLocation?.Sections?.FirstOrDefault();
            ////if (loc == null)
            ////{
            ////    response.ActionSucceeded = false;
            ////    return response;
            ////}
            ////var inv = context.ProductInventoryLocationSections.Where(x => x.InventoryLocationSectionID == loc.ID);
            foreach (var locID in pils)
            {
                locID.QuantityAllocated = 0;
            }
            try
            {
                response.ActionSucceeded = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            catch
            {
                response.ActionSucceeded = false;
                return response;
            }
            return response;
        }

        /// <inheritdoc/>
        public IContactModel? GetWarehouseContact(string regionCode, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var contactID = context.InventoryLocations
                .AsNoTracking()
                .FilterByActive(true)
                .FilterILByRegionCode(Contract.RequiresValidKey(regionCode))
                .Select(x => x.ContactID)
                .FirstOrDefault();
            if (Contract.CheckInvalidID(contactID))
            {
                // TODO: Widen search by radius of the region to find nearest out of region?
                return null;
            }
            var contact = context.Contacts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(contactID!.Value)
                .SelectFirstFullContactAndMapToContactModel(contextProfileName);
            return contact;
        }

        /// <inheritdoc/>
        public IProductInventoryLocationSectionModel? GetClosestWarehouseByRegionCode(
            string regionCode,
            int productID,
            string? contextProfileName)
        {
            var json1 = CEFConfigDictionary.WarehousePriorityListByRegionMatrixJSON;
            if (!Contract.CheckValidKey(json1))
            {
                return null;
            }
            var warehouseServesMatrix = JsonConvert.DeserializeObject<SerializableDictionary<List<string>>>(json1!);
            if (warehouseServesMatrix == null || !warehouseServesMatrix.Any())
            {
                return null;
            }
            var warehouse = string.Empty;
            foreach (var kvp in warehouseServesMatrix.Where(kvp => kvp.Value != null && kvp.Value.Contains(regionCode)))
            {
                warehouse = kvp.Key;
                break;
            }
            if (!Contract.CheckValidKey(warehouse))
            {
                return null;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var section = context.ProductInventoryLocationSections
                .AsNoTracking()
                .FilterByActive(true)
                .FilterPILSByHasQuantity(true)
                .FilterPILSByProductID(productID)
                .FilterPILSByILSInventoryLocationKey(warehouse)
                .SelectFirstFullProductInventoryLocationSectionAndMapToProductInventoryLocationSectionModel(contextProfileName);
            if (section != null)
            {
                return section;
            }
            var json2 = CEFConfigDictionary.WarehousePriorityListByRegionMatrixJSON;
            if (!Contract.CheckValidKey(json2))
            {
                return context.ProductInventoryLocationSections
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterPILSByHasQuantity(true)
                    .FilterPILSByProductID(productID)
                    .SelectFirstFullProductInventoryLocationSectionAndMapToProductInventoryLocationSectionModel(contextProfileName);
            }
            var warehouseMatrix = JsonConvert.DeserializeObject<SerializableDictionary<List<string>>>(json2!);
            if (!warehouseMatrix!.TryGetValue(warehouse, out var warehouseList))
            {
                return null;
            }
            var pils = warehouseList
                .Select(x => context.ProductInventoryLocationSections
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterPILSByHasQuantity(true)
                    .FilterPILSByProductID(productID)
                    .FilterPILSByILSInventoryLocationKey(x)
                    .SelectFirstFullProductInventoryLocationSectionAndMapToProductInventoryLocationSectionModel(contextProfileName))
                .FirstOrDefault(x => x != null);
            return pils;
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<ProductInventoryLocationSection>> FilterQueryByModelCustomAsync(
            IQueryable<ProductInventoryLocationSection> query,
            IProductInventoryLocationSectionSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterPILSByILSInventoryLocationID(search.InventoryLocationID)
                .FilterPILSByILSInventoryLocationKey(search.InventoryLocationKey)
                .FilterPILSByILSInventoryLocationName(search.InventoryLocationName)
                .FilterPILSByHasQuantityBroken(search.HasBrokenQuantity)
                .FilterPILSByStoreID(search.StoreID)
                .FilterPILSByStoreIDs(search.StoreIDs)
                .FilterPILSByProductIDs(search.ProductIDs);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IProductInventoryLocationSection entity,
            IProductInventoryLocationSectionModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // ProductInventoryLocationSection Properties
            entity.Quantity = model.Quantity;
            entity.QuantityAllocated = model.QuantityAllocated;
            entity.QuantityBroken = model.QuantityBroken;
            // Related Objects
            model.Slave ??= new InventoryLocationSectionModel
            {
                ID = model.SlaveID,
                CustomKey = model.SlaveKey,
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                Name = model.SlaveName,
                InventoryLocationKey = model.Slave?.InventoryLocationKey
                    ?? "UNASSIGNED",
                InventoryLocationName = model.Slave?.InventoryLocationName
                    ?? model.Slave?.InventoryLocationKey
                    ?? "Unassigned Warehouse",
            };
            entity.SlaveID = await Workflows.InventoryLocationSections.ResolveWithAutoGenerateToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
            entity.MasterID = await Workflows.Products.ResolveWithAutoGenerateToIDAsync(
                    model.MasterID,
                    model.MasterKey,
                    model.MasterName,
                    null,
                    context)
                .ConfigureAwait(false);
            entity.Hash = model.Hash;
            // Secondary Workflows
            if (context.ContextProfileName == null)
            {
                await Workflows.PricingFactory.RemoveAllCachedPricesByProductIDAsync(
                        entity.MasterID,
                        null)
                    .ConfigureAwait(false);
            }
        }
    }
}
