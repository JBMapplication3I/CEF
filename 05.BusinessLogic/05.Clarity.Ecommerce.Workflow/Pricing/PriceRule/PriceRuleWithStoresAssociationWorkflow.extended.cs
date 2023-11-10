// <copyright file="PriceRuleWithStoresAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate price rule stores workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class PriceRuleWithStoresAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IPriceRuleStoreModel model)
        {
            // Must have something to recognize the Store by
            return Contract.CheckAnyValidID(model.StoreID, model.Store?.ID)
                || Contract.CheckAnyValidKey(model.StoreKey, model.StoreName, model.Store?.CustomKey, model.Store?.Name);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IPriceRuleStoreModel model,
            IPriceRuleStore entity,
            IClarityEcommerceEntities context)
        {
            return (entity.SlaveID == model.SlaveID
                    || entity.Slave!.CustomKey == (model.SlaveKey ?? model.Slave?.CustomKey)
                    || entity.Slave.Name == (model.SlaveName ?? model.Slave?.Name))
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IPriceRuleStore newEntity,
            IPriceRuleStoreModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model),
                "Must pass either the Store ID, Key or Name to match against");
            newEntity.SlaveID = await Workflows.Stores.ResolveToIDAsync(
                    byID: model.StoreID,
                    byKey: model.StoreKey,
                    byName: model.StoreName,
                    model: model.Store,
                    context: context)
                .ConfigureAwait(false);
        }
    }
}
