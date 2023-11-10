// <copyright file="ProductWithBrandsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate Product Brands workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ProductWithBrandsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IBrandProductModel model)
        {
            // Must have something to recognize the brand by
            return Contract.CheckAnyValidID(model.BrandID, model.Brand?.ID)
                || Contract.CheckAnyValidKey(model.BrandKey, model.BrandName, model.Brand?.CustomKey, model.Brand?.Name);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IBrandProductModel model,
            IBrandProduct entity,
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
            IBrandProduct newEntity,
            IBrandProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.Brands.ResolveWithAutoGenerateToIDAsync(
                    model.BrandID,
                    model.BrandKey,
                    model.BrandName,
                    model.Brand,
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
