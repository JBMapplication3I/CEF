// <copyright file="UserWithStoresAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate User Stores workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class UserWithStoresAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IStoreUser newEntity,
            IStoreUserModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.Stores.ResolveWithAutoGenerateToIDAsync(
                    model.StoreID,
                    model.StoreKey,
                    model.StoreName,
                    model.Store,
                    context)
                .ConfigureAwait(false);
        }
    }
}
