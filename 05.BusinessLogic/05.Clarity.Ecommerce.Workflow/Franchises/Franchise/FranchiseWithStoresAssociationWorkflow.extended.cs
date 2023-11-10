// <copyright file="FranchiseWithStoresAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate Franchise Stores workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FranchiseWithStoresAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IFranchiseStore newEntity,
            IFranchiseStoreModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.Stores.ResolveWithAutoGenerateToIDAsync(
                    model.StoreID,
                    model.StoreKey,
                    model.StoreName,
                    model.Store,
                    context)
                .ConfigureAwait(false);
        }
    }
}
