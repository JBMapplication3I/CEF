// <copyright file="VendorWithProductsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate vendor products workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class VendorWithProductsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(
            IVendorProductModel model)
        {
            // Must have something to recognize the vendor by
            return Contract.CheckAnyValidID(model.SlaveID, model.Slave?.ID)
                || Contract.CheckAnyValidKey(model.SlaveKey, model.SlaveName, model.Slave?.CustomKey, model.Slave?.Name);
        }

        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IVendorProductModel model,
            IVendorProduct entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.ActualCost == model.ActualCost
                && entity.Bin == model.Bin
                && entity.CostMultiplier == model.CostMultiplier
                && entity.InventoryCount == model.InventoryCount
                && entity.ListedPrice == model.ListedPrice
                && entity.MaximumInventory == model.MaximumInventory
                && entity.MinimumInventory == model.MinimumInventory);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IVendorProduct newEntity,
            IVendorProductModel model,
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
