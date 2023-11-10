// <copyright file="BrandCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand workflow class</summary>
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

    public partial class BrandWorkflow
    {
        /// <inheritdoc/>
        public Task<IBrandModel?> GetFullAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                GetAsEntityFull(Contract.RequiresValidID(id), context)
                    .CreateBrandModelFromEntityFull(contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IBrandModel?> GetFullAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                GetAsEntityFull(Contract.RequiresValidKey(key), context)
                    .CreateBrandModelFromEntityFull(contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IBrandModel?> GetByHostUrlAsync(string hostUrl, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.Brands
                    // .AsNoTracking() // TODO@JTG: Research fix
                    .FilterByActive(true)
                    .FilterBrandsByHostUrl(Contract.RequiresValidKey(hostUrl))
                    .SelectFirstFullBrandAndMapToBrandModel(contextProfileName));
        }

        /// <inheritdoc/>
        public async Task<int?> CheckExistsByHostUrlAsync(string hostUrl, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Brands
                .FilterByActive(true)
                .FilterBrandsByHostUrl(Contract.RequiresValidKey(hostUrl))
                .Select(x => (int?)x.ID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IBrand entity,
            IBrandModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Store Properties
            entity.UpdateBrandFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Related Objects
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunLimitedAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Brand? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            await DeleteAssociatedImagesAsync<BrandImage>(entity.ID, context).ConfigureAwait(false);
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Gets as entity full base.</summary>
        /// <param name="context">The context.</param>
        /// <returns>as entity full base.</returns>
        private static IQueryable<IBrand> GetAsEntityFullBase(IClarityEcommerceEntities context)
        {
            return context.Brands
                .Include(x => x.Stores)
                .Include(x => x.Notes)
                .Include(x => x.Accounts)
                .Include(x => x.Categories)
                .Include(x => x.BrandInventoryLocations)
                .Include(x => x.BrandInventoryLocations!.Select(y => y.Slave))
                .Include(x => x.Products)
                .Include(x => x.Products!.Select(y => y.Slave))
                .Include(x => x.BrandSiteDomains)
                .Include(x => x.Users);
        }

        /// <summary>Gets as entity full.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>as entity full.</returns>
        private static IBrand? GetAsEntityFull(int id, IClarityEcommerceEntities context)
        {
            return GetAsEntityFullBase(context).FilterByID(id).FirstOrDefault();
        }

        /// <summary>Gets as entity full.</summary>
        /// <param name="key">    The key to get.</param>
        /// <param name="context">The context.</param>
        /// <returns>as entity full.</returns>
        private static IBrand? GetAsEntityFull(string key, IClarityEcommerceEntities context)
        {
            return GetAsEntityFullBase(context)
                .FilterByActive(true)
                .FilterByCustomKey(key, true)
                .FirstOrDefault();
        }

        /// <summary>Executes the limited associate workflows operation.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RunLimitedAssociateWorkflowsAsync(
            IBrand entity,
            IBrandModel model,
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
                        note.BrandID = entity.ID;
                    }
                }
                await Workflows.BrandWithNotesAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            }
#pragma warning disable SA1501,format // Statement should not be on a single line
            if (model.Images != null) { await Workflows.BrandWithImagesAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (CEFConfigDictionary.ImportProductsAllowSaveBrandProductsWithBrand && model.Products != null) { await Workflows.BrandWithProductsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.Accounts != null) { await Workflows.BrandWithAccountsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            // Skipped: Not supposed to map this property in via this manner: Users
            // Skipped: Not supposed to map this property in via this manner: Categories
            if (model.Stores != null) { await Workflows.BrandWithStoresAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.BrandSiteDomains != null) { await Workflows.BrandWithBrandSiteDomainsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            if (model.BrandCurrencies != null) { await Workflows.BrandWithBrandCurrenciesAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
#pragma warning restore SA1501,format // Statement should not be on a single line
        }
    }
}
