// <copyright file="InventoryLocationCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location workflow class</summary>
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
    using Mapper;
    using Utilities;

    public partial class InventoryLocationWorkflow
    {
        /// <inheritdoc/>
        public Task<IInventoryLocationSectionModel?> GetSectionAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.InventoryLocationSections
                    .AsNoTracking()
                    .FilterByID(id)
                    .SelectSingleFullInventoryLocationSectionAndMapToInventoryLocationSectionModel(contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IInventoryLocationSectionModel?> GetSectionAsync(string customKey, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.InventoryLocationSections
                    .AsNoTracking()
                    .FilterByCustomKey(customKey, true)
                    .SelectSingleFullInventoryLocationSectionAndMapToInventoryLocationSectionModel(contextProfileName));
        }

        /// <inheritdoc/>
        public Task<List<IInventoryLocationSectionModel>> SearchSectionsAsync(
            int? locationID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.InventoryLocationSections
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Where(x => x.InventoryLocation!.Active
                             && (!locationID.HasValue || x.InventoryLocationID == locationID.Value))
                    .ApplySorting(null, null, contextProfileName)
                    .SelectListInventoryLocationSectionAndMapToInventoryLocationSectionModel(contextProfileName)
                    .ToList());
        }

        /// <inheritdoc/>
        public Task<List<IProductInventoryLocationSectionModel>> GetInventoryLocationsByProductAsync(
            int productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.ProductInventoryLocationSections
                .AsNoTracking()
                .FilterByActive(true)
                .Where(pils => pils.Slave!.Active)
                .SelectListProductInventoryLocationSectionAndMapToProductInventoryLocationSectionModel(contextProfileName)
                .ToList());
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<IInventoryLocationModel>> SearchForConnectAsync(
            IInventoryLocationSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.InventoryLocations.AsNoTracking().AsQueryable();
            return (await FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .SelectFullInventoryLocationAndMapToInventoryLocationModel(
                    search.Paging,
                    search.Sorts,
                    search.Groupings,
                    contextProfileName)
                .results;
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IInventoryLocation entity,
            IInventoryLocationModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresValidKey(model.Name, "Name is required");
            // The Address requires special handling
            entity.UpdateInventoryLocationFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<InventoryLocation>> FilterQueryByModelCustomAsync(
            IQueryable<InventoryLocation> query,
            IInventoryLocationSearchModel search,
            IClarityEcommerceEntities context)
        {
            if (!string.IsNullOrWhiteSpace(search.CountryName))
            {
                var searchString = search.CountryName!.Trim().ToLower();
                query = query
                    .Where(il => il.Contact != null
                              && il.Contact.Address != null
                              && il.Contact.Address.Country != null
                              && il.Contact.Address.Country.Name != null
                              && il.Contact.Address.Country.Name.Contains(searchString));
            }
            if (!string.IsNullOrWhiteSpace(search.StateName))
            {
                var searchString = search.StateName!.Trim().ToLower();
                query = query
                    .Where(il => il.Contact != null
                              && il.Contact.Address != null
                              && il.Contact.Address.Region != null
                              && il.Contact.Address.Region.Name != null
                              && il.Contact.Address.Region.Name.Contains(searchString));
            }
            if (!string.IsNullOrWhiteSpace(search.PostalCode))
            {
                var searchString = search.StateName!.Trim().ToLower();
                query = query
                    .Where(il => il.Contact != null
                              && il.Contact.Address != null
                              && il.Contact.Address.PostalCode != null
                              && il.Contact.Address.PostalCode.Contains(searchString));
            }
            return query;
        }
    }
}
