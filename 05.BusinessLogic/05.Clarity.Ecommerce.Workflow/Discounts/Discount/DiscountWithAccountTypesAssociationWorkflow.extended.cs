// <copyright file="DiscountWithAccountTypesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate discount account types workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class DiscountWithAccountTypesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IDiscountAccountTypeModel model)
        {
            // Must have something to recognize the Account Type by
            return Contract.CheckAnyValidID(model.SlaveID, model.Slave?.ID)
                || Contract.CheckAnyValidKey(model.SlaveKey, model.SlaveName, model.Slave?.CustomKey, model.Slave?.Name);
        }

        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IDiscountAccountTypeModel model,
            IDiscountAccountType entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                (entity.SlaveID == model.SlaveID
                    || entity.Slave!.CustomKey == (model.SlaveKey ?? model.Slave?.CustomKey)
                    || entity.Slave.Name == (model.SlaveName ?? model.Slave?.Name))
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary());
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IDiscountAccountType newEntity,
            IDiscountAccountTypeModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model),
                "Must pass either the Account Type ID, Key or Name to match against");
            newEntity.SlaveID = await Workflows.AccountTypes.ResolveToIDAsync(
                    byID: model.SlaveID,
                    byKey: model.SlaveKey,
                    byName: model.SlaveName,
                    model: model.Slave,
                    context: context)
                .ConfigureAwait(false);
        }
    }
}
