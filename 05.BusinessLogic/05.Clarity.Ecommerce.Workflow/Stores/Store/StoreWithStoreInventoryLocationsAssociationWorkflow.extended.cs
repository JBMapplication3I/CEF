// <copyright file="StoreWithStoreInventoryLocationsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate store inventory locations workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class StoreWithStoreInventoryLocationsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IStoreInventoryLocationModel model,
            IStoreInventoryLocation entity,
            IClarityEcommerceEntities context)
        {
            return model.TypeID == entity.TypeID;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IStoreInventoryLocation newEntity,
            IStoreInventoryLocationModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.InventoryLocations.ResolveWithAutoGenerateToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
            newEntity.TypeID = await Workflows.StoreInventoryLocationTypes.ResolveWithAutoGenerateToIDAsync(
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
