// <copyright file="FranchiseCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class FranchiseWorkflow
    {
        /// <inheritdoc/>
        public Task<IFranchiseModel?> GetFullAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                GetAsEntityFull(Contract.RequiresValidID(id), context)
                    .CreateFranchiseModelFromEntityFull(contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IFranchiseModel?> GetFullAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                GetAsEntityFull(Contract.RequiresValidKey(key), context)
                    .CreateFranchiseModelFromEntityFull(contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IFranchiseModel?> GetByHostUrlAsync(string hostUrl, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.Franchises
                    // .AsNoTracking() // TODO@JTG: Research fix
                    .FilterByActive(true)
                    .FilterFranchisesByHostUrl(Contract.RequiresValidKey(hostUrl))
                    .SelectFirstFullFranchiseAndMapToFranchiseModel(contextProfileName));
        }

        /// <inheritdoc/>
        public async Task<int?> CheckExistsByHostUrlAsync(string hostUrl, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Franchises
                .FilterByActive(true)
                .FilterFranchisesByHostUrl(Contract.RequiresValidKey(hostUrl))
                .Select(x => (int?)x.ID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Franchise>> FilterQueryByModelCustomAsync(
            IQueryable<Franchise> query,
            IFranchiseSearchModel search,
            IClarityEcommerceEntities context)
        {
            var zipCode = await Workflows.ZipCodes.GetByZipCodeValueAsync(
                    search.StoreAnyZipCode,
                    context.ContextProfileName)
                .ConfigureAwait(false);
            return query
                .FilterByHaveATypeSearchModel<Franchise, FranchiseType>(search)
                .OrderBy(v => v.Name)
                // Filter within ZipCode Radius
                .FilterFranchisesByStoreZipCodeRadius(zipCode?.Latitude, zipCode?.Longitude, search.StoreAnyRadius, search.StoreAnyUnits)
                // Filter within Lat/Long Radius
                .FilterFranchisesByStoreLatitudeLongitudeRadius(search.StoreAnyLatitude, search.StoreAnyLongitude, search.StoreAnyRadius, search.StoreAnyUnits)
                .FilterFranchisesByStoreRegionID(search.StoreRegionID)
                .FilterFranchisesByStoreAnyContactAddressMatchingRegionID(search.StoreAnyRegionID)
                .FilterFranchisesByStoreAnyContactAddressMatchingCountryID(search.StoreCountryID)
                .FilterFranchisesByStoreAnyContactAddressMatchingCity(search.StoreAnyCity)
                .FilterFranchisesByStoreAnyDistrictID(search.StoreAnyDistrictID);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IFranchise entity,
            IFranchiseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Store Properties
            entity.UpdateFranchiseFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Related Objects
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunLimitedAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Franchise? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            await DeleteAssociatedImagesAsync<FranchiseImage>(entity.ID, context).ConfigureAwait(false);
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Gets as entity full base.</summary>
        /// <param name="context">The context.</param>
        /// <returns>as entity full base.</returns>
        private static IQueryable<IFranchise> GetAsEntityFullBase(IClarityEcommerceEntities context)
        {
            return context.Franchises
                .Include(x => x.Stores)
                .Include(x => x.Notes)
                .Include(x => x.Accounts)
                .Include(x => x.Categories)
                .Include(x => x.FranchiseInventoryLocations)
                .Include(x => x.FranchiseInventoryLocations!.Select(y => y.Slave))
                .Include(x => x.Products)
                .Include(x => x.Products!.Select(y => y.Slave))
                .Include(x => x.FranchiseSiteDomains)
                .Include(x => x.Users);
        }

        /// <summary>Gets as entity full.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>as entity full.</returns>
        private static IFranchise? GetAsEntityFull(int id, IClarityEcommerceEntities context)
        {
            return GetAsEntityFullBase(context).FilterByID(id).FirstOrDefault();
        }

        /// <summary>Gets as entity full.</summary>
        /// <param name="key">    The key to get.</param>
        /// <param name="context">The context.</param>
        /// <returns>as entity full.</returns>
        private static IFranchise? GetAsEntityFull(string key, IClarityEcommerceEntities context)
        {
            return GetAsEntityFullBase(context).FilterByActive(true).FilterByCustomKey(key, true).FirstOrDefault();
        }

        /// <summary>Executes the limited associate workflows operation.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RunLimitedAssociateWorkflowsAsync(
            IFranchise entity,
            IFranchiseModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            if (model.Notes != null)
            {
                if (Contract.CheckValidID(entity.ID))
                {
                    foreach (var note in model.Notes)
                    {
                        note.FranchiseID = entity.ID;
                    }
                }
                await Workflows.FranchiseWithNotesAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            }
#pragma warning disable SA1501,format // Statement should not be on a single line
            if (model.Images != null) { await Workflows.FranchiseWithImagesAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (CEFConfigDictionary.ImportProductsAllowSaveFranchiseProductsWithFranchise && model.Products != null) { await Workflows.FranchiseWithProductsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.Accounts != null) { await Workflows.FranchiseWithAccountsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.Users != null) { await Workflows.FranchiseWithUsersAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            // Skipped: Not supposed to map this property in via this manner: Categories
            if (model.Stores != null) { await Workflows.FranchiseWithStoresAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.FranchiseInventoryLocations != null) { await Workflows.FranchiseWithFranchiseInventoryLocationsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.FranchiseSiteDomains != null) { await Workflows.FranchiseWithFranchiseSiteDomainsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.FranchiseCurrencies != null) { await Workflows.FranchiseWithFranchiseCurrenciesAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.FranchiseLanguages != null) { await Workflows.FranchiseWithFranchiseLanguagesAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
#pragma warning restore SA1501,format // Statement should not be on a single line
        }
    }
}
