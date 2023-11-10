// <copyright file="FranchiseWithUsersAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate franchise users workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FranchiseWithUsersAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IFranchiseUser newEntity,
            IFranchiseUserModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.Users.ResolveToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
        }
    }
}
