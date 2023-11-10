// <copyright file="AccountWithStoresAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate Account Stores workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class AccountWithStoresAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IStoreAccountModel model,
            IStoreAccount entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.SlaveID == model.StoreID
                && entity.HasAccessToStore == model.HasAccessToStore);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IStoreAccount newEntity,
            IStoreAccountModel model,
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
