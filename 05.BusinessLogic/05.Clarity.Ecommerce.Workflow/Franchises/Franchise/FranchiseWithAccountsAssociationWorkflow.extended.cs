// <copyright file="FranchiseWithAccountsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate franchise accounts workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FranchiseWithAccountsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IFranchiseAccountModel model,
            IFranchiseAccount entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.HasAccessToFranchise == model.HasAccessToFranchise
                && entity.PricePointID == model.PricePointID);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IFranchiseAccount newEntity,
            IFranchiseAccountModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.Accounts.ResolveToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
            newEntity.HasAccessToFranchise = model.HasAccessToFranchise;
            newEntity.PricePointID = await Workflows.PricePoints.ResolveToIDOptionalAsync(
                    model.PricePointID,
                    model.PricePointKey,
                    model.PricePointName,
                    model.PricePoint,
                    context)
                .ConfigureAwait(false);
        }
    }
}
