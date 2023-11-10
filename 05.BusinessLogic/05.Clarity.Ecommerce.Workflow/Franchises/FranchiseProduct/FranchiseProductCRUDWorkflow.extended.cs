// <copyright file="FranchiseProductCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise product workflow class</summary>
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

    public partial class FranchiseProductWorkflow
    {
        /// <inheritdoc/>
        protected override bool OverrideDuplicateCheck => true;

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CreateManyAsync(
            List<IFranchiseProductModel> models,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            foreach (var model in models)
            {
                if (!OverrideDuplicateCheck)
                {
                    await DuplicateCheckAsync(model, context).ConfigureAwait(false);
                }
                int? productID = model.SlaveID;
                if (Contract.CheckInvalidID(productID))
                {
                    productID = await Workflows.Products.CheckExistsAsync(model.SlaveKey!, context).ConfigureAwait(false);
                    if (Contract.CheckInvalidID(productID))
                    {
                        continue;
                    }
                    productID = productID!.Value;
                }
                int? franchiseID = model.FranchiseID;
                if (Contract.CheckInvalidID(franchiseID))
                {
                    franchiseID = await Workflows.Franchises.CheckExistsAsync(model.FranchiseKey!, context).ConfigureAwait(false);
                    if (Contract.CheckInvalidID(franchiseID))
                    {
                        continue;
                    }
                    franchiseID = franchiseID!.Value;
                }
                context.FranchiseProducts.Add(new()
                {
                    // Base Properties
                    CustomKey = model.CustomKey,
                    Active = true,
                    CreatedDate = timestamp,
                    Hash = model.Hash,
                    JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary(),
                    // Relationship Table Properties
                    SlaveID = productID.Value,
                    MasterID = franchiseID.Value,
                    // FranchiseProduct Properties
                    IsVisibleIn = model.IsVisibleIn,
                    PriceBase = model.PriceBase,
                    PriceMsrp = model.PriceMsrp,
                    PriceReduction = model.PriceReduction,
                    PriceSale = model.PriceSale,
                });
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateManyAsync(
            List<IFranchiseProductModel> models,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            ////context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            foreach (var model in models)
            {
                // TODO: Internally check each model to make sure it's actually different. If not, don't try to change it
                var entity = await context.FranchiseProducts.FilterByID(model.ID).SingleOrDefaultAsync().ConfigureAwait(false)
                    ?? (!string.IsNullOrWhiteSpace(model.CustomKey)
                        ? await context.FranchiseProducts.FilterByCustomKey(model.CustomKey, true)
                            .OrderByDescending(x => x.Active)
                            .SingleOrDefaultAsync()
                            .ConfigureAwait(false)
                        : throw new ArgumentException("Must supply an ID or CustomKey that matches an existing record"));
                // ReSharper disable once PossibleNullReferenceException
                if (entity.CustomKey != model.CustomKey)
                {
                    await DuplicateCheckAsync(model, context).ConfigureAwait(false); // This will throw if it finds another entity with this model's key
                }
                // Base Properties
                entity.CustomKey = model.CustomKey;
                entity.Active = model.Active;
                entity.UpdatedDate = timestamp;
                entity.Hash = model.Hash;
                entity.JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary();
                // Relationship Table Properties
                entity.SlaveID = model.SlaveID;
                entity.MasterID = model.FranchiseID;
                // FranchiseProduct Properties
                entity.IsVisibleIn = model.IsVisibleIn;
                entity.PriceBase = model.PriceBase;
                entity.PriceMsrp = model.PriceMsrp;
                entity.PriceReduction = model.PriceReduction;
                entity.PriceSale = model.PriceSale;
                // Secondary Workflows
                if (context.ContextProfileName == null)
                {
                    await Workflows.PricingFactory.RemoveAllCachedPricesByProductIDAsync(
                            entity.SlaveID,
                            null)
                        .ConfigureAwait(false);
                }
            }
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IFranchiseProduct entity,
            IFranchiseProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateFranchiseProductFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            // Secondary Workflows
            if (Contract.CheckValidKey(context.ContextProfileName))
            {
                // Don't run during tests
                return;
            }
            if (context.ContextProfileName == null)
            {
                await Workflows.PricingFactory.RemoveAllCachedPricesByProductIDAsync(
                        entity.SlaveID,
                        null)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<FranchiseProduct>> FilterQueryByModelCustomAsync(
            IQueryable<FranchiseProduct> query,
            IFranchiseProductSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterFranchiseProductsByProductKey(search.ProductKey)
                .FilterFranchiseProductsByProductName(search.ProductName);
        }
    }
}
