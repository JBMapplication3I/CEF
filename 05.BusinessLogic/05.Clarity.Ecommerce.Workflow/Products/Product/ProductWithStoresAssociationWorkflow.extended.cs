// <copyright file="ProductWithStoresAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate Product Stores workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ProductWithStoresAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IStoreProductModel model)
        {
            // Must have something to recognize the store by
            return Contract.CheckAnyValidID(model.StoreID, model.Store?.ID)
                || Contract.CheckAnyValidKey(model.StoreKey, model.StoreName, model.Store?.CustomKey, model.Store?.Name);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IStoreProductModel model,
            IStoreProduct entity,
            IClarityEcommerceEntities context)
        {
            return entity.IsVisibleIn == model.IsVisibleIn
                && entity.PriceBase == model.PriceBase
                && entity.PriceSale == model.PriceSale
                && entity.PriceReduction == model.PriceReduction
                && entity.PriceMsrp == model.PriceMsrp;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IStoreProduct newEntity,
            IStoreProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.Stores.ResolveWithAutoGenerateToIDAsync(
                    model.StoreID,
                    model.StoreKey,
                    model.StoreName,
                    model.Store,
                    context)
                .ConfigureAwait(false);
            newEntity.IsVisibleIn = model.IsVisibleIn;
            newEntity.PriceBase = model.PriceBase;
            newEntity.PriceSale = model.PriceSale;
            newEntity.PriceReduction = model.PriceReduction;
            newEntity.PriceMsrp = model.PriceMsrp;
        }
    }
}
