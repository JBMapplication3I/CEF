// <copyright file="DiscountWithUsersAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate discount users workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class DiscountWithUsersAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IDiscountUserModel model)
        {
            // Must have something to recognize the User by
            return Contract.CheckAnyValidID(model.SlaveID, model.Slave?.ID)
                || Contract.CheckAnyValidKey(model.SlaveKey, model.Slave?.CustomKey);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IDiscountUserModel model,
            IDiscountUser entity,
            IClarityEcommerceEntities context)
        {
            return (entity.SlaveID == model.SlaveID
                    || entity.Slave!.CustomKey == (model.SlaveKey ?? model.Slave?.CustomKey))
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IDiscountUser newEntity,
            IDiscountUserModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model),
                "Must pass either the User ID, Key or Name to match against");
            newEntity.SlaveID = await Workflows.Users.ResolveToIDAsync(
                    byID: model.SlaveID,
                    byKey: model.SlaveKey,
                    model: model.Slave,
                    context: context)
                .ConfigureAwait(false);
        }
    }
}
