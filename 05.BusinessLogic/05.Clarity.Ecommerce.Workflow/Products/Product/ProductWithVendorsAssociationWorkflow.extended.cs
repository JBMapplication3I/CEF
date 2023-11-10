// <copyright file="ProductWithVendorsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate product vendors workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ProductWithVendorsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IVendorProductModel model)
        {
            // Must have something to recognize the vendor by
            return Contract.CheckAnyValidID(model.MasterID)
                || Contract.CheckAnyValidKey(model.MasterKey, model.MasterName);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IVendorProductModel model,
            IVendorProduct entity,
            IClarityEcommerceEntities context)
        {
            return entity.ActualCost == model.ActualCost
                && entity.Bin == model.Bin
                && entity.CostMultiplier == model.CostMultiplier
                && entity.InventoryCount == model.InventoryCount
                && entity.ListedPrice == model.ListedPrice
                && entity.MaximumInventory == model.MaximumInventory
                && entity.MinimumInventory == model.MinimumInventory;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IVendorProduct newEntity,
            IVendorProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.Vendors.ResolveWithAutoGenerateToIDAsync(
                    model.MasterID,
                    model.MasterKey,
                    model.MasterKey,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.ActualCost = model.ActualCost;
            newEntity.Bin = model.Bin;
            newEntity.CostMultiplier = model.CostMultiplier;
            newEntity.InventoryCount = model.InventoryCount;
            newEntity.MaximumInventory = model.MaximumInventory;
            newEntity.MinimumInventory = model.MinimumInventory;
            newEntity.ListedPrice = model.ListedPrice;
        }
    }
}
