// <copyright file="ProductKitWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product kit workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using MoreLinq.Extensions;
    using Utilities;

    /// <summary>A product kit workflow.</summary>
    /// <seealso cref="IProductKitWorkflow"/>
    public class ProductKitWorkflow : IProductKitWorkflow
    {
        /// <summary>Identifier for the kit component product association type.</summary>
        private int kitComponentProductAssociationTypeID;

        /// <inheritdoc/>
        public Task<IEnumerable<IProductModel>> KitComponentBOMFullAsync(int id, string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByID(id).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(Array.Empty<IProductModel>().AsEnumerable());
            }
            var upResults = DoKitComponentBOM(thisProduct, true, contextProfileName).ToList();
            var downResults = DoKitComponentBOM(thisProduct, false, contextProfileName);
            upResults.AddRange(downResults);
            return Task.FromResult<IEnumerable<IProductModel>>(upResults);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<IProductModel>> KitComponentBOMFullAsync(string key, string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(key));
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByCustomKey(key, true).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(Array.Empty<IProductModel>().AsEnumerable());
            }
            var upResults = DoKitComponentBOM(thisProduct, true, contextProfileName).ToList();
            var downResults = DoKitComponentBOM(thisProduct, false, contextProfileName);
            upResults.AddRange(downResults);
            return Task.FromResult<IEnumerable<IProductModel>>(upResults);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<IProductModel>> KitComponentBOMUpAsync(int id, string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByID(id).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(Array.Empty<IProductModel>().AsEnumerable());
            }
            return Task.FromResult(DoKitComponentBOM(thisProduct, true, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IEnumerable<IProductModel>> KitComponentBOMUpAsync(string key, string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(key));
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByCustomKey(key, true).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(Array.Empty<IProductModel>().AsEnumerable());
            }
            return Task.FromResult(DoKitComponentBOM(thisProduct, true, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IEnumerable<IProductModel>> KitComponentBOMDownAsync(int id, string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByID(id).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(Array.Empty<IProductModel>().AsEnumerable());
            }
            return Task.FromResult(DoKitComponentBOM(thisProduct, false, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IEnumerable<IProductModel>> KitComponentBOMDownAsync(string key, string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(key));
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByCustomKey(key, true).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(Array.Empty<IProductModel>().AsEnumerable());
            }
            return Task.FromResult(DoKitComponentBOM(thisProduct, false, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<bool> BreakKitInventoryApartAsync(
            int id,
            decimal? quantity,
            int? locationSectionID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByID(id).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(
                ModifyInventory(thisProduct, quantity, locationSectionID, true, true, context, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<bool> BreakKitInventoryApartAsync(
            string key,
            decimal? quantity,
            int? locationSectionID,
            string? contextProfileName)
        {
            Contract.RequiresValidKey(key);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByCustomKey(key, true).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(
                ModifyInventory(thisProduct, quantity, locationSectionID, true, true, context, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<bool> ReassembleBrokenKitInventoryAsync(
            int id,
            decimal? quantity,
            int? locationSectionID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByID(id).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(
                ModifyInventory(thisProduct, quantity, locationSectionID, false, true, context, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<bool> ReassembleBrokenKitInventoryAsync(
            string key,
            decimal? quantity,
            int? locationSectionID,
            string? contextProfileName)
        {
            Contract.RequiresValidKey(key);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByCustomKey(key, true).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(
                ModifyInventory(thisProduct, quantity, locationSectionID, false, true, context, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<bool> AssembleKitInventoryAsync(
            int id,
            decimal? quantity,
            int? locationSectionID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByID(id).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(
                ModifyInventory(thisProduct, quantity, locationSectionID, false, false, context, contextProfileName));
        }

        /// <inheritdoc/>
        public Task<bool> AssembleKitInventoryAsync(
            string key,
            decimal? quantity,
            int? locationSectionID,
            string? contextProfileName)
        {
            Contract.RequiresValidKey(key);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var thisProduct = context.Products.FilterByCustomKey(key, true).FirstOrDefault();
            if (thisProduct is null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(
                ModifyInventory(thisProduct, quantity, locationSectionID, false, false, context, contextProfileName));
        }

        /// <summary>Validates the stock quantity.</summary>
        /// <param name="quantity">         The quantity.</param>
        /// <param name="locationSectionID">Identifier for the location section.</param>
        /// <param name="thisProduct">      this product.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local, UnusedMethodReturnValue.Local
        private static bool ValidateStockQuantity(decimal quantity, int locationSectionID, IProduct thisProduct)
        {
            var stockQuantity = thisProduct.ProductInventoryLocationSections!
                .Where(pils => pils.Quantity.HasValue && pils.SlaveID == locationSectionID)
                .Select(pils => pils.Quantity!.Value)
                .DefaultIfEmpty(0)
                .Sum();
            if (stockQuantity < quantity)
            {
                // Business Rule: Cannot break apart kits when there aren't enough
                throw new InvalidOperationException("There is not enough inventory to handle this request");
            }
            return true;
        }

        /// <summary>Validates the location section.</summary>
        /// <param name="locationSectionID">Identifier for the location section.</param>
        /// <param name="thisProduct">      this product.</param>
        /// <returns>An int?.</returns>
        private static int? ValidateLocationSection(int? locationSectionID, IProduct thisProduct)
        {
            if (locationSectionID is null or <= 0 or int.MaxValue)
            {
                var firstLocation = thisProduct.ProductInventoryLocationSections!.FirstOrDefault();
                if (firstLocation == null)
                {
                    // Business Rule: Must have inventory
                    throw new InvalidOperationException("There is no inventory set up for this product");
                }
                locationSectionID = firstLocation.ID;
            }
            else
            {
                var specifiedLocation = thisProduct.ProductInventoryLocationSections!.FirstOrDefault(pils => pils.SlaveID == locationSectionID.Value);
                if (specifiedLocation == null)
                {
                    // Business Rule: Must have inventory
                    throw new InvalidOperationException("Selected inventory location isn't set up for this product");
                }
            }
            return locationSectionID;
        }

        /// <summary>Updates the component quantity.</summary>
        /// <param name="quantity">         The quantity.</param>
        /// <param name="addOrSubtract">    True to add or subtract.</param>
        /// <param name="locationSectionID">Identifier for the location section.</param>
        /// <param name="kitComponent">     The kit component.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        private static void UpdateComponentQuantity(
            decimal quantity,
            bool addOrSubtract,
            int locationSectionID,
            IProductAssociation kitComponent,
            DateTime timestamp)
        {
            var componentQuantity = kitComponent.Quantity ?? 1; // NOTE: Proven to have a value already, so will never coalesce to 1
            var componentProduct = kitComponent.Slave;
            var componentSection = componentProduct!.ProductInventoryLocationSections!.FirstOrDefault(pils => pils.SlaveID == locationSectionID);
            if (componentSection == null)
            {
                // Generate a bin for this product when it doesn't have one
                componentSection = RegistryLoaderWrapper.GetInstance<DataModel.ProductInventoryLocationSection>();
                componentSection.Active = true;
                componentSection.CreatedDate = timestamp;
                componentSection.UpdatedDate = null;
                componentSection.SlaveID = locationSectionID;
                componentSection.MasterID = componentProduct.ID;
                componentSection.Quantity = 0;
                componentSection.QuantityBroken = 0;
                componentProduct.ProductInventoryLocationSections!.Add(componentSection);
            }
            // Add the quantity from the kit (times number of kits to break apart)
            if (addOrSubtract)
            {
                componentSection.Quantity += componentQuantity * quantity;
            }
            else
            {
                componentSection.Quantity -= componentQuantity * quantity;
            }
        }

        /// <summary>Enumerates do kit component bom in this collection.</summary>
        /// <param name="thisProduct">       this product.</param>
        /// <param name="upOrDown">          True to up or down.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process do kit component bom in this collection.</returns>
        private IEnumerable<IProductModel> DoKitComponentBOM(
            IProduct thisProduct, bool upOrDown, string? contextProfileName)
        {
            var processedIDs = new List<int>();
            return KitComponentBOM(thisProduct, upOrDown, processedIDs, contextProfileName)
                .Select(ModelMapperForProduct.MapLiteProductOld);
        }

        /// <summary>Enumerates kit component bom in this collection.</summary>
        /// <param name="thisProduct">       this product.</param>
        /// <param name="upOrDown">          True to up or down.</param>
        /// <param name="processedIDs">      The processed IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process kit component bom in this collection.</returns>
        private IEnumerable<DataModel.Product> KitComponentBOM(
            IProduct? thisProduct,
            bool upOrDown,
            List<int> processedIDs,
            string? contextProfileName)
        {
            if (thisProduct is null)
            {
                return new List<DataModel.Product>();
            }
            var typeID = KitComponentProductAssociationTypeID(contextProfileName);
            var collection = upOrDown ? thisProduct.ProductsAssociatedWith : thisProduct.ProductAssociations;
            if (collection!.All(p => p.TypeID != typeID))
            {
                return new List<DataModel.Product>();
            }
            var kitMasterIDs = collection!
                .Where(p => p.TypeID == typeID)
                .Select(p => upOrDown ? p.MasterID : p.SlaveID)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
            for (var i = 0; i < kitMasterIDs.Count;)
            {
                if (processedIDs.Contains(kitMasterIDs[i]))
                {
                    kitMasterIDs.RemoveAt(i);
                    continue;
                }
                i++;
            }
            if (kitMasterIDs.Count == 0)
            {
                return new List<DataModel.Product>();
            }
            processedIDs.AddRange(kitMasterIDs);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var possibles = context.Products.FilterByIDs(kitMasterIDs).ToList();
            var anotherLevelOutPossibles = new List<DataModel.Product>();
            foreach (var result in possibles)
            {
                anotherLevelOutPossibles.AddRange(KitComponentBOM(
                    result, upOrDown, processedIDs, contextProfileName));
                // Force ourselves to break off so we don't blow up
                if (anotherLevelOutPossibles.Count >= 500)
                {
                    break;
                }
            }
            possibles.AddRange(anotherLevelOutPossibles);
            return possibles.DistinctBy(x => x.ID);
        }

        /// <summary>Validates the product.</summary>
        /// <param name="thisProduct">       this product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private void ValidateProduct(IProduct thisProduct, string? contextProfileName)
        {
            Contract.RequiresNotNull<ArgumentException, IProduct>(thisProduct, "ID must match a record in the database");
            var typeID = KitComponentProductAssociationTypeID(contextProfileName);
            // Business Rule: Cannot break apart a non-kit
            Contract.Requires<InvalidOperationException>(thisProduct.TypeID != typeID, "This product is not a kit");
            // Business Rule: Must have components to update
            Contract.Requires<InvalidOperationException>(
                thisProduct.ProductAssociations!.All(pa => pa.TypeID != typeID),
                "This product kit does not have any components assigned");
            // Business Rule: Components must have Quantity
            Contract.Requires<InvalidOperationException>(
                thisProduct.ProductAssociations!.Any(pa => pa.TypeID == typeID && pa.Quantity is { } or <= 0),
                "This product kit has at least one component that doesn't specify a quantity");
        }

        /// <summary>Kit component product association type identifier.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int.</returns>
        private int KitComponentProductAssociationTypeID(string? contextProfileName)
        {
            if (Contract.CheckValidID(kitComponentProductAssociationTypeID))
            {
                return kitComponentProductAssociationTypeID;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return kitComponentProductAssociationTypeID = context.ProductAssociationTypes
                .AsNoTracking()
                .FilterByName("Kit Component")
                .Single()
                .ID;
        }

        /// <summary>Modify inventory.</summary>
        /// <param name="thisProduct">                   this product.</param>
        /// <param name="quantity">                      The quantity.</param>
        /// <param name="locationSectionID">             Identifier for the location section.</param>
        /// <param name="addOrSubtractComponentQuantity">True to add or subtract component quantity.</param>
        /// <param name="affectBroken">                  True to affect broken.</param>
        /// <param name="context">                       The context.</param>
        /// <param name="contextProfileName">            Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool ModifyInventory(
            IProduct thisProduct,
            decimal? quantity,
            int? locationSectionID,
            bool addOrSubtractComponentQuantity,
            bool affectBroken,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            ValidateProduct(thisProduct, contextProfileName);
            if (quantity is null or < 1)
            {
                quantity = 1;
            }
            locationSectionID = ValidateLocationSection(locationSectionID, thisProduct);
            // ReSharper disable once PossibleInvalidOperationException
            ValidateStockQuantity(quantity.Value, locationSectionID!.Value, thisProduct);
            // Everything has passed validation so far
            var timestamp = DateExtensions.GenDateTime;
            // Run each component
            foreach (var kitComponent in thisProduct.ProductAssociations!.Where(pa => pa.TypeID == KitComponentProductAssociationTypeID(contextProfileName)).ToList())
            {
                UpdateComponentQuantity(quantity.Value, addOrSubtractComponentQuantity, locationSectionID.Value, kitComponent, timestamp);
            }
            // Reduce the normal quantity and increase the broken quantity
            var kitSection = thisProduct.ProductInventoryLocationSections!.First(pils => pils.SlaveID == locationSectionID);
            if (addOrSubtractComponentQuantity)
            {
                kitSection.Quantity -= quantity.Value;
                if (affectBroken)
                {
                    kitSection.QuantityBroken += quantity.Value;
                }
            }
            else
            {
                kitSection.Quantity += quantity.Value;
                if (affectBroken)
                {
                    kitSection.QuantityBroken -= quantity.Value;
                }
            }
            // We're finished!
            return context.SaveUnitOfWork();
        }
    }
}
