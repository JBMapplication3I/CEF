// <copyright file="ManufacturerCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer workflow class</summary>
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
    using Models;
    using Utilities;

    public partial class ManufacturerWorkflow
    {
        /// <inheritdoc/>
        public Task<List<IManufacturerProductModel>> GetManufacturersByProductAsync(int productID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.ManufacturerProducts
                    .FilterByActive(true)
                    .FilterManufacturerProductsByActiveManufacturers()
                    .FilterManufacturerProductsByActiveProducts()
                    .FilterManufacturerProductsByProductID(productID)
                    .SelectListManufacturerProductAndMapToManufacturerProductModel(contextProfileName)
                    .ToList());
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int?>> GetIDByAssignedUserIDAsync(int userID, string? contextProfileName)
        {
            throw new NotImplementedException();
            // TODO: Requires schema to add accounts to manufacturers
            /*
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var accountID = await context.Users
                .AsNoTracking()
                .FilterByID(Contract.RequiresValidID(userID))
                .Select(x => x.AccountID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(accountID))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR: User or User's Account not found.");
            }
            var manufacturerID = await context.Manufacturers
                .AsNoTracking()
                .Where(x => x.Accounts!.Any(y => y.Active && y.SlaveID == accountID!.Value))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            return manufacturerID.WrapInPassingCEFARIfNotNull("ERROR: No Manufacturer associated to this Account.");
            */
        }

        /// <inheritdoc/>
        public Task<(IEnumerable<IManufacturerProductModel> results, int totalPages, int totalCount)> GetProductsByManufacturerAsync(
            IManufacturerProductSearchModel search,
            bool asListing,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.ManufacturerProducts
                .AsNoTracking()
                .FilterByActive(search.Active)
                .FilterManufacturerProductsByActiveManufacturers()
                .FilterManufacturerProductsByActiveProducts()
                .FilterManufacturerProductsByManufacturerID(search.ManufacturerID)
                .FilterManufacturerProductsByProductID(search.ProductID)
                .FilterManufacturerProductsByProductKey(search.ProductKey)
                .FilterManufacturerProductsByProductName(search.ProductName)
                // TODO: .FilterManufacturerProductsByMinInventoryCount(search.MinInventoryCount)
                // TODO: .FilterManufacturerProductsByMaxInventoryCount(search.MaxInventoryCount)
                .FilterByModifiedSince(search.ModifiedSince)
                .OrderBy(x => x.Slave!.Name);
            return Task.FromResult(asListing
                ? query.SelectListManufacturerProductAndMapToManufacturerProductModel(search.Paging, search.Sorts, search.Groupings, contextProfileName)
                : query.SelectLiteManufacturerProductAndMapToManufacturerProductModel(search.Paging, search.Sorts, search.Groupings, contextProfileName));
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IManufacturer entity,
            IManufacturerModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateManufacturerFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeactivateAsync(
            Manufacturer? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Deactivate a null record");
            }
            if (!entity.Active)
            {
                // Already inactive
                return CEFAR.PassingCEFAR();
            }
            var timestamp = DateExtensions.GenDateTime;
            await DeactivateAssociatedImagesAsync<ManufacturerImage>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ManufacturerProduct>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<DiscountManufacturer>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<FavoriteManufacturer>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<StoreManufacturer>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<VendorManufacturer>(entity.ID, timestamp, context).ConfigureAwait(false);
            var e = context.Set<Manufacturer>().FilterByID(entity.ID).Single();
            e.UpdatedDate = timestamp;
            e.Active = false;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Failed to save Deactivate");
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Manufacturer? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            await DeleteAssociatedImagesAsync<ManufacturerImage>(entity.ID, context).ConfigureAwait(false);
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }
    }
}
