// <copyright file="StoreProductCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store product workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class StoreProductWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> CreateManyAsync(
            List<IStoreProductModel> models,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            foreach (var model in models)
            {
                ////if (!OverrideDuplicateCheck) { DuplicateCheck(model); }
                int? productID = model.SlaveID;
                if (Contract.CheckInvalidID(productID))
                {
                    productID = await Workflows.Products.CheckExistsAsync(model.SlaveKey!, contextProfileName).ConfigureAwait(false);
                    if (Contract.CheckInvalidID(productID))
                    {
                        continue;
                    }
                    productID = productID!.Value;
                }
                int? storeID = model.StoreID;
                if (Contract.CheckInvalidID(storeID))
                {
                    storeID = await Workflows.Stores.CheckExistsAsync(model.StoreKey!, contextProfileName).ConfigureAwait(false);
                    if (Contract.CheckInvalidID(storeID))
                    {
                        continue;
                    }
                }
                var entity = new StoreProduct
                {
                    Active = true,
                    CreatedDate = timestamp,
                    CustomKey = model.CustomKey,
                    SlaveID = productID.Value,
                    IsVisibleIn = model.IsVisibleIn,
                    PriceBase = model.PriceBase,
                    PriceMsrp = model.PriceMsrp,
                    PriceReduction = model.PriceReduction,
                    PriceSale = model.PriceSale,
                    Hash = model.Hash,
                    MasterID = storeID!.Value,
                };
                context.StoreProducts.Add(entity);
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateManyAsync(
            List<IStoreProductModel> models,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.Configuration.ValidateOnSaveEnabled = false;
            foreach (var model in models)
            {
                var entity = context.StoreProducts.FilterByID(model.ID).SingleOrDefault()
                    ?? (!string.IsNullOrWhiteSpace(model.CustomKey)
                        ? context.StoreProducts.FilterByCustomKey(model.CustomKey, true)
                            .OrderByDescending(x => x.Active)
                            .SingleOrDefault()
                        : throw new ArgumentException("Must supply an ID or CustomKey that matches an existing record"));
                entity!.Active = model.Active;
                entity.UpdatedDate = timestamp;
                entity.CustomKey = model.CustomKey;
                entity.SlaveID = model.SlaveID;
                entity.IsVisibleIn = model.IsVisibleIn;
                entity.PriceBase = model.PriceBase;
                entity.PriceMsrp = model.PriceMsrp;
                entity.PriceReduction = model.PriceReduction;
                entity.PriceSale = model.PriceSale;
                entity.Hash = model.Hash;
                entity.MasterID = model.StoreID;
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
            IStoreProduct entity,
            IStoreProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateStoreProductFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            // Secondary Workflows
            if (Contract.CheckValidKey(context.ContextProfileName))
            {
                return;
            }
            // Don't run during tests
            await Workflows.PricingFactory.RemoveAllCachedPricesByProductIDAsync(entity.SlaveID, null).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<StoreProduct>> FilterQueryByModelCustomAsync(
            IQueryable<StoreProduct> query,
            IStoreProductSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterStoreProductsByProductKey(search.ProductKey)
                .FilterStoreProductsByProductName(search.ProductName);
        }
    }
}
