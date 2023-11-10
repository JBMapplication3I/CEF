// <copyright file="BrandWithBrandInventoryLocationsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate brand inventory locations workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class BrandWithBrandInventoryLocationsAssociationWorkflow
    {
        protected override List<IBrandInventoryLocationModel>? GetModelObjectsList(IBrandModel model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IBrandInventoryLocationModel model,
            IBrandInventoryLocation entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(model.TypeID == entity.TypeID);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IBrandInventoryLocation newEntity,
            IBrandInventoryLocationModel model,
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
            newEntity.TypeID = await Workflows.BrandInventoryLocationTypes.ResolveWithAutoGenerateToIDAsync(
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
