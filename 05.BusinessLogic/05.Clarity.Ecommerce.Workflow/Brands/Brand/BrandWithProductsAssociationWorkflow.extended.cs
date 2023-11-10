// <copyright file="BrandWithProductsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate brand products workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class BrandWithProductsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IBrandProductModel model,
            IBrandProduct entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.IsVisibleIn == model.IsVisibleIn
                && entity.PriceBase == model.PriceBase
                && entity.PriceSale == model.PriceSale
                && entity.PriceReduction == model.PriceReduction
                && entity.PriceMsrp == model.PriceMsrp);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IBrandProduct newEntity,
            IBrandProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.Products.ResolveToIDAsync(
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
