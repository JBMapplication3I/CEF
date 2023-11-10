// <copyright file="BrandWithAccountsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate brand accounts workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class BrandWithAccountsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IBrandAccountModel model,
            IBrandAccount entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.IsVisibleIn == model.IsVisibleIn
                && entity.PricePointID == model.PricePointID);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IBrandAccount newEntity,
            IBrandAccountModel model,
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
            newEntity.IsVisibleIn = model.IsVisibleIn;
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
