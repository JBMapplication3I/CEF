// <copyright file="AccountWithAccountAssociationsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate accounts workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class AccountWithAccountAssociationsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IAccountAssociationModel model,
            IAccountAssociation entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(entity.Slave!.CustomKey == (model.SlaveKey ?? model.Slave?.CustomKey)
                && (entity.TypeID == model.TypeID || entity.Type!.CustomKey == (model.TypeKey ?? model.Type?.CustomKey))
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary());
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IAccountAssociation newEntity,
            IAccountAssociationModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.TypeID = await Workflows.AccountAssociationTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: model.TypeID,
                    byKey: model.TypeKey,
                    byName: model.TypeName,
                    model: model.Type,
                    context: context)
                .ConfigureAwait(false);
            newEntity.SlaveID = await Workflows.Accounts.ResolveWithAutoGenerateToIDAsync(
                    byID: model.SlaveID,
                    byKey: model.SlaveKey,
                    byName: model.SlaveName,
                    model: model.Slave,
                    context: context)
                .ConfigureAwait(false);
        }
    }
}
