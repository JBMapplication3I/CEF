// <copyright file="AdWithStoresAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate Ad Stores workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class AdWithStoresAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IAdStore newEntity,
            IAdStoreModel model,
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
