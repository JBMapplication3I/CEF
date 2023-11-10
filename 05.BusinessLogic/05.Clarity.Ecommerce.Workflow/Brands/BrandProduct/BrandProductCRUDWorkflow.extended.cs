// <copyright file="BrandProductCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand product workflow class</summary>
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

    public partial class BrandProductWorkflow
    {
        /// <inheritdoc/>
        protected override bool OverrideDuplicateCheck => true;

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CreateManyAsync(
            List<IBrandProductModel> models,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using (var context = new ClarityEcommerceEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                foreach (var model in models)
                {
                    if (!OverrideDuplicateCheck)
                    {
                        await DuplicateCheckAsync(model, context).ConfigureAwait(false);
                    }
                    int? productID = model.SlaveID;
                    if (!Contract.CheckValidID(productID) && Contract.CheckValidKey(model.SlaveKey))
                    {
                        productID = await Workflows.Products.CheckExistsAsync(model.SlaveKey!, context).ConfigureAwait(false);
                        if (Contract.CheckInvalidID(productID))
                        {
                            continue;
                        }
                        productID = productID!.Value;
                    }
                    int? brandID = model.BrandID;
                    if (!Contract.CheckValidID(brandID) && Contract.CheckValidKey(model.BrandKey))
                    {
                        brandID = await Workflows.Brands.CheckExistsAsync(model.BrandKey!, context).ConfigureAwait(false);
                        if (Contract.CheckInvalidID(brandID))
                        {
                            continue;
                        }
                        brandID = brandID!.Value;
                    }
                    context.BrandProducts.Add(new()
                    {
                        // Base Properties
                        CustomKey = model.CustomKey,
                        Active = true,
                        CreatedDate = timestamp,
                        Hash = model.Hash,
                        JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary(),
                        // Relationship Table Properties
                        SlaveID = productID.Value,
                        MasterID = brandID.Value,
                        // BrandProduct Properties
                        IsVisibleIn = model.IsVisibleIn,
                        PriceBase = model.PriceBase,
                        PriceMsrp = model.PriceMsrp,
                        PriceReduction = model.PriceReduction,
                        PriceSale = model.PriceSale,
                    });
                }
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateManyAsync(
            List<IBrandProductModel> models,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = new ClarityEcommerceEntities();
            ////context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            foreach (var model in models)
            {
                // TODO: Internally check each model to make sure it's actually different. If not, don't try to change it
                var entity = await context.BrandProducts.FilterByID(model.ID).SingleOrDefaultAsync().ConfigureAwait(false)
                    ?? (!string.IsNullOrWhiteSpace(model.CustomKey)
                        ? await context.BrandProducts.FilterByCustomKey(model.CustomKey, true)
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
                entity.MasterID = model.BrandID;
                // BrandProduct Properties
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
            IBrandProduct entity,
            IBrandProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateBrandProductFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
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
        protected override async Task<IQueryable<BrandProduct>> FilterQueryByModelCustomAsync(
            IQueryable<BrandProduct> query,
            IBrandProductSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterBrandProductsByProductKey(search.ProductKey)
                .FilterBrandProductsByProductName(search.ProductName);
        }
    }
}
