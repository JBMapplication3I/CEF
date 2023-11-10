// <copyright file="PriceRuleWithManufacturersAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate price rule manufacturers workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class PriceRuleWithManufacturersAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IPriceRuleManufacturerModel model)
        {
            // Must have something to recognize the Manufacturer by
            return Contract.CheckAnyValidID(model.SlaveID, model.Slave?.ID)
                || Contract.CheckAnyValidKey(model.SlaveKey, model.SlaveName, model.Slave?.CustomKey, model.Slave?.Name);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IPriceRuleManufacturerModel model,
            IPriceRuleManufacturer entity,
            IClarityEcommerceEntities context)
        {
            return (entity.SlaveID == model.SlaveID
                    || entity.Slave!.CustomKey == (model.SlaveKey ?? model.Slave?.CustomKey)
                    || entity.Slave.Name == (model.SlaveName ?? model.Slave?.Name))
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IPriceRuleManufacturer newEntity,
            IPriceRuleManufacturerModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model),
                "Must pass either the Manufacturer ID, Key or Name to match against");
            newEntity.SlaveID = await Workflows.Manufacturers.ResolveToIDAsync(
                    byID: model.SlaveID,
                    byKey: model.SlaveKey,
                    byName: model.SlaveName,
                    model: model.Slave,
                    context: context)
                .ConfigureAwait(false);
        }
    }
}
