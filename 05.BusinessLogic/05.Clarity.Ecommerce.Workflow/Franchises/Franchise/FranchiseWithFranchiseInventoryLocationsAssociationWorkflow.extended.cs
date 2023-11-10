// <copyright file="FranchiseWithFranchiseInventoryLocationsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate franchise inventory locations workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FranchiseWithFranchiseInventoryLocationsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IFranchiseInventoryLocationModel model,
            IFranchiseInventoryLocation entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(model.TypeID == entity.TypeID);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IFranchiseInventoryLocation newEntity,
            IFranchiseInventoryLocationModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.InventoryLocations.ResolveToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
            newEntity.TypeID = await Workflows.FranchiseInventoryLocationTypes.ResolveWithAutoGenerateToIDAsync(
                    model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.TypeDisplayName,
                    model.Type,
                    context)
                .ConfigureAwait(false);
        }
    }
}
