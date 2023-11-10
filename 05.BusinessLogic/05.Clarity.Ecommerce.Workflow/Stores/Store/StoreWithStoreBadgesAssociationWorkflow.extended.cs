// <copyright file="StoreWithStoreBadgesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate store badges workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class StoreWithStoreBadgesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IStoreBadge newEntity,
            IStoreBadgeModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.Badges.ResolveWithAutoGenerateToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
        }
    }
}
