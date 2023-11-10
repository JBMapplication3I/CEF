// <copyright file="StoreWithProductsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate store products workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class StoreWithProductsAssociationWorkflow
    {
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
            newEntity.SlaveID = await Workflows.Products.ResolveWithAutoGenerateToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
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
